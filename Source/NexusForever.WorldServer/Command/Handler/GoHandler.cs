using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NexusForever.Shared.GameTable;
using NexusForever.Shared.GameTable.Model;
using NexusForever.Shared.GameTable.Static;
using NexusForever.WorldServer.Command.Attributes;
using NexusForever.WorldServer.Command.Contexts;
using NexusForever.WorldServer.Game;
using NexusForever.WorldServer.Game.Housing;
using NexusForever.WorldServer.Game.Account.Static;

namespace NexusForever.WorldServer.Command.Handler
{
    [Name("Go", Permission.CommandGo)]
    public class GoHandler : NamedCommand
    {
        public GoHandler()
            : base(false, "go")
        {
        }

        private IEnumerable<uint> GetTextIds(WorldLocation2Entry entry)
        {
            WorldZoneEntry worldZone = GameTableManager.WorldZone.GetEntry(entry.WorldZoneId);
            if (worldZone != null && worldZone.LocalizedTextIdName != 0)
                yield return worldZone.LocalizedTextIdName;
            WorldEntry world = GameTableManager.World.GetEntry(entry.WorldId);
            if (world != null && world.LocalizedTextIdName != 0)
                yield return world.LocalizedTextIdName;
        }
        protected override async Task HandleCommandAsync(CommandContext context, string command, string[] parameters)
        {
            string zoneName = string.Join(" ", parameters).ToLower();

            WorldLocation2Entry zone = SearchManager.Search<WorldLocation2Entry>(zoneName, context.Language, GetTextIds).FirstOrDefault();

            if (zoneName.ToLower() == "home")
            {
                Residence residence = ResidenceManager.GetResidence(context.Session.Player.Name).GetAwaiter().GetResult();
                if (residence == null)
                {
                    //no residence has been created yet
                    await context.SendMessageAsync("No home to go to! Try using !house teleport");
                }

                ResidenceEntrance entrance = ResidenceManager.GetResidenceEntrance(residence);
                context.Session.Player.TeleportTo(entrance.Entry, entrance.Position, 0u, residence.Id);
                await context.SendMessageAsync("Going home...");
            }
            else
            {
                if (zone == null)
                    await context.SendErrorAsync($"Unknown zone: {zoneName}");
                else
                {
                    context.Session?.Player.TeleportTo((ushort)zone.WorldId, zone.Position0, zone.Position1, zone.Position2);
                    await context.SendMessageAsync($"{zoneName}: {zone.WorldId} {zone.Position0} {zone.Position1} {zone.Position2}");
                }
            }
            
        }
    }
}
