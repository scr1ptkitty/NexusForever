﻿using System.IO;
using System.Numerics;
using System.Threading.Tasks;
using NexusForever.Shared.GameTable;
using NexusForever.Shared.GameTable.Model;
using NexusForever.WorldServer.Command.Attributes;
using NexusForever.WorldServer.Command.Contexts;
using NexusForever.WorldServer.Game;
using NexusForever.WorldServer.Game.Housing;
using NexusForever.WorldServer.Game.Map;
using NexusForever.WorldServer.Network.Message.Model;
using NexusForever.WorldServer.Game.Housing.Static;
using NexusForever.WorldServer.Game.Account.Static;

namespace NexusForever.WorldServer.Command.Handler
{
    [Name("Housing", Permission.None)]
    public class HousingCommandHandler : CommandCategory
    {
        public HousingCommandHandler()
            : base(true, "house")
        {
        }

        [SubCommandHandler("teleport", "[name] - Teleport to a residence, optionally specifying a character", Permission.CommandHouseTeleport)]
        public Task TeleportSubCommandHandler(CommandContext context, string command, string[] parameters)
        {
            string name;

            if (parameters.Length == 1)
            {
                name = parameters[0];
            }
            else
            {
                name = parameters.Length == 0 ? context.Session.Player.Name : string.Join(" ", parameters);
            }

            context.SendMessageAsync("Finding residence for " + name + "...");

            Residence residence = ResidenceManager.GetResidence(name).GetAwaiter().GetResult();
            if (residence == null)
            {
                if (parameters.Length == 0)
                    residence = ResidenceManager.CreateResidence(context.Session.Player);
                else
                {
                    context.SendMessageAsync("A residence for that character doesn't exist!");
                    return Task.CompletedTask;
                }
            }

            ResidenceEntrance entrance = ResidenceManager.GetResidenceEntrance(residence);
            context.Session.Player.TeleportTo(entrance.Entry, entrance.Position, 0u, residence.Id);

            return Task.CompletedTask;
        }


        
        [SubCommandHandler("teleportinside", "[name] - Teleport to a residence, optionally specifying a character", Permission.CommandHouseTeleportInside)]
        public Task TeleportInsideSubCommandHandler(CommandContext context, string command, string[] parameters)
        {
            WorldLocation2Entry entry = GameTableManager.WorldLocation2.GetEntry(uint.Parse(parameters[0]));
            if (entry == null)
                return Task.CompletedTask;

            WorldEntry worldEntry = GameTableManager.World.GetEntry(entry.WorldId);
            if (worldEntry == null)
                return Task.CompletedTask;

            context.Session.Player.TeleportTo(worldEntry, new Vector3(entry.Position0, entry.Position1, entry.Position2), 0u, 3u);

            return Task.CompletedTask;
        }
        

        [SubCommandHandler("decoradd", "decorId [quantity] - Add decor by id to your crate, optionally specifying quantity", Permission.CommandHouseDecorAdd)]
        public Task DecorAddSubCommandHandler(CommandContext context, string command, string[] parameters)
        {
            if (parameters.Length < 1 && parameters.Length > 2)
                return Task.CompletedTask;

            if (!(context.Session.Player.Map is ResidenceMap residenceMap))
            {
                context.SendMessageAsync("You need to be on a housing map to use this command!");
                return Task.CompletedTask;
            }

            uint decorInfoId = uint.Parse(parameters[0]);
            uint quantity    = parameters.Length == 2 ? uint.Parse(parameters[1]) : 1u;

            HousingDecorInfoEntry entry = GameTableManager.HousingDecorInfo.GetEntry(decorInfoId);
            if (entry == null)
            {
                context.SendMessageAsync($"Invalid decor info id {decorInfoId}!");
                return Task.CompletedTask;
            }

            residenceMap.DecorCreate(entry, quantity);
            return Task.CompletedTask;
        }

        [SubCommandHandler("decorlookup", "name - Returns a list of decor ids that match the supplied name", Permission.CommandHouseDecorLookup)]
        public Task DecorLookupSubCommandHandler(CommandContext context, string command, string[] parameters)
        {
            if (parameters.Length != 1)
                return Task.CompletedTask;

            var sw = new StringWriter();
            sw.WriteLine("Decor Lookup Results:");

            TextTable tt = GameTableManager.GetTextTable(context.Language);
            foreach (HousingDecorInfoEntry decorEntry in
                SearchManager.Search<HousingDecorInfoEntry>(parameters[0], context.Language, e => e.LocalizedTextIdName, true))
            {
                string text = tt.GetEntry(decorEntry.LocalizedTextIdName);
                sw.WriteLine($"({decorEntry.Id}) {text}");
            }

            context.SendMessageAsync(sw.ToString());
            return Task.CompletedTask;
        }

        [SubCommandHandler("remodel", "remodelType remodelId - Change ground/sky or toggle clutter", Permission.CommandHouseRemodel)]
        public Task RemodelSubCommandHandler(CommandContext context, string command, string[] parameters)
        {

            //remodel
            ClientHousingRemodel clientRemod = new ClientHousingRemodel();
            if (!(context.Session.Player.Map is ResidenceMap residenceMap))
            {
                context.SendMessageAsync("You need to be on a housing map to use this command!");
                return Task.CompletedTask;
            }

            Residence residence = ResidenceManager.GetResidence(context.Session.Player.Name).GetAwaiter().GetResult();


            if (parameters[0].ToLower() == "clutter")
            {
                if (parameters[1].ToLower() == "off")
                {
                    residence.Flags = ResidenceFlags.groundClutterOff;
                }
                else if (parameters[1].ToLower() == "on")
                {
                    residence.Flags = 0;
                }
            }
            else if (parameters[0].ToLower() == "ground")
            {
                residence.Ground = ushort.Parse(parameters[1]);
            }
            else if (parameters[0].ToLower() == "sky")
            {
                residence.Sky = ushort.Parse(parameters[1]);
            }

            residenceMap.Remodel(context.Session.Player, clientRemod);
            return Task.CompletedTask;
        }
    }
}
