using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Numerics;
using System.Threading.Tasks;
using NexusForever.Shared.Configuration;
using NexusForever.Shared.IO.Map;
using NexusForever.WorldServer.Game.Entity;
using NexusForever.Shared;
using NLog;

namespace NexusForever.WorldServer.Game.Map
{
    public sealed class MapManager : Singleton<MapManager>, IUpdate
    {
        private static readonly Logger log = LogManager.GetCurrentClassLogger();

        private readonly Dictionary</*worldId*/ ushort, IMap> maps = new Dictionary<ushort, IMap>();

        private MapManager()
        {
        }

        public void Initialise()
        {
            ValidateMapFiles();
        }

        public int GetMapCount()
        {
            return maps.Keys.Count;
        }

        private void ValidateMapFiles()
        {
            log.Info("Validating map files...");

            string mapPath = ConfigurationManager<WorldServerConfiguration>.Instance.Config.Map.MapPath;
            if (mapPath == null || !Directory.Exists(mapPath))
                throw new DirectoryNotFoundException("Invalid path to base maps! Make sure you have set it in the configuration file.");

            foreach (string fileName in Directory.EnumerateFiles(mapPath, "*.nfmap"))
            {
                using (FileStream stream = File.OpenRead(fileName))
                using (BinaryReader reader = new BinaryReader(stream))
                {
                    var mapFile = new MapFile();
                    mapFile.ReadHeader(reader);
                }
            }
        }

        public void Update(double lastTick)
        {
            if (maps.Count == 0)
                return;

            var sw = Stopwatch.StartNew();

            var tasks = new List<Task>();
            foreach (IMap map in maps.Values)
                tasks.Add(Task.Run(() => { map.Update(lastTick); }));
            try
            {
                Task.WaitAll(tasks.ToArray());
            }
            catch
            {
                // ignored.
            }

            sw.Stop();
            if (sw.ElapsedMilliseconds > 10)
                log.Warn($"{maps.Count} map(s) took {sw.ElapsedMilliseconds}ms to update!");
        }

        /// <summary>
        /// Enqueue <see cref="Player"/> to be added to a map. 
        /// </summary>
        public void AddToMap(Player player, MapInfo info, Vector3 vector3)
        {
            if (info?.Entry == null)
                throw new ArgumentException();

            IMap map = CreateMap(info, player);
            map.EnqueueAdd(player, vector3);
        }

        /// <summary>
        /// Create base or instanced <see cref="IMap"/> of <see cref="MapInfo"/> for <see cref="Player"/>.
        /// </summary>
        private IMap CreateMap(MapInfo info, Player player)
        {
            IMap map = CreateBaseMap(info);
            if (map is IInstancedMap iMap)
                map = iMap.CreateInstance(info, player);

            return map;
        }

        /// <summary>
        /// Create and store base <see cref="IMap"/> of <see cref="MapInfo"/>.
        /// </summary>
        private IMap CreateBaseMap(MapInfo info)
        {
            if (maps.TryGetValue((ushort)info.Entry.Id, out IMap map))
                return map;

            switch (info.Entry.Type)
            {
                case 4:
                case 5:
                    map = new InstancedMap<ResidenceMap>();
                    break;
                default:
                    map = new BaseMap();
                    break;
            }

            map.Initialise(info, null);
            maps.Add((ushort)info.Entry.Id, map);
            return map;
        }

    }
}
