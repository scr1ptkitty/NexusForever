using System.Threading.Tasks;
using NexusForever.WorldServer.Command.Attributes;
using NexusForever.WorldServer.Command.Contexts;
using NLog;
using NexusForever.WorldServer.Game.Entity;
using NexusForever.WorldServer.Game.Account.Static;
using NexusForever.Shared.GameTable.Model;
using NexusForever.Shared.GameTable;
using System.Linq;

namespace NexusForever.WorldServer.Command.Handler
{
    [Name("Morph", Permission.None)]
    public class MorphCommandHandler : CommandCategory
    {
        private static readonly ILogger log = LogManager.GetCurrentClassLogger();

        public MorphCommandHandler()
            : base(true, "morph")
        {
        }

        [SubCommandHandler("dagun", "variantName - 7 variants", Permission.CommandMorph)]
        public Task DagunSubCommandHandler(CommandContext context, string command, string[] parameters)
        {
            if (parameters.Length != 1)
                return Task.CompletedTask;

            uint creatureId = 0;
            string variant = parameters[0].ToLower();

            switch(variant)
            {
                case "black":
                    creatureId = 69407;
                    break;
                case "grim":
                    creatureId = 45867;
                    break;
                case "purple":
                    creatureId = 69411;
                    break;
                case "strain":
                    creatureId = 69408;
                    break;
                case "spacefaring":
                    creatureId = 69409;
                    break;
                case "silver":
                    creatureId = 69410;
                    break;
                case "white":
                    creatureId = 73296;
                    break;
            }

            // get the creature id from the creature2 table
            Creature2Entry creature2 = GameTableManager.Creature2.GetEntry(creatureId);
            
            if (creature2 == null || creatureId == 0)
            {
                log.Info($"Invalid morph variant");
                return Task.CompletedTask;
            }
            
            // get the creature's display information
            Creature2DisplayGroupEntryEntry displayGroupEntry = GameTableManager.Creature2DisplayGroupEntry.Entries.FirstOrDefault(d => d.Creature2DisplayGroupId == creature2.Creature2DisplayGroupId);
            if (displayGroupEntry == null)
                return Task.CompletedTask;

            log.Info($"Morphing player into dagun");
            // change the player's display information to the creature's display information
            context.Session.Player.SetDisplayInfo(displayGroupEntry.Creature2DisplayInfoId);
            return Task.CompletedTask;
        }

        [SubCommandHandler("dawngrazer", "variantName - 7 variants", Permission.CommandMorph)]
        public Task DawngrazerSubCommandHandler(CommandContext context, string command, string[] parameters)
        {
            if (parameters.Length != 1)
                return Task.CompletedTask;

            uint creatureId = 0;
            string variant = parameters[0].ToLower();

            switch(variant)
            {
                case "blue":
                    creatureId = 69404;
                    break;
                case "brown":
                    creatureId = 69401;
                    break;
                case "grey":
                    creatureId = 69402;
                    break;
                case "strain":
                    creatureId = 69405;
                    break;
                case "tan":
                    creatureId = 69403;
                    break;
                case "white":
                    creatureId = 69027;
                    break;
                case "zebra":
                    creatureId = 69406;
                    break;
            }

            // get the creature id from the creature2 table
            Creature2Entry creature2 = GameTableManager.Creature2.GetEntry(creatureId);
            if (creature2 == null || creatureId == 0)
            {
                log.Info($"Invalid morph variant");
                return Task.CompletedTask;
            }

            // get the creature's display information
            Creature2DisplayGroupEntryEntry displayGroupEntry = GameTableManager.Creature2DisplayGroupEntry.Entries.FirstOrDefault(d => d.Creature2DisplayGroupId == creature2.Creature2DisplayGroupId);
            if (displayGroupEntry == null)
                return Task.CompletedTask;
            
            log.Info($"Morphing player into dawngrazer");
            // change the player's display information to the creature's display information
            context.Session.Player.SetDisplayInfo(displayGroupEntry.Creature2DisplayInfoId);
            return Task.CompletedTask;
        }

        [SubCommandHandler("jabbit", "variantName - 5 variants", Permission.CommandMorph)]
        public Task JabbitSubCommandHandler(CommandContext context, string command, string[] parameters)
        {
            if (parameters.Length != 1)
                return Task.CompletedTask;

            uint creatureId = 0;
            string variant = parameters[0].ToLower();

            switch(variant)
            {
                case "augmented":
                    creatureId = 69415;
                    break;
                case "blue":
                    creatureId = 69412;
                    break;
                case "brown":
                    creatureId = 69413;
                    break;
                case "grey":
                    creatureId = 69414;
                    break;
                case "strain":
                    creatureId = 69416;
                    break;
            }

            // get the creature id from the creature2 table
            Creature2Entry creature2 = GameTableManager.Creature2.GetEntry(creatureId);
            if (creature2 == null || creatureId == 0)
            {
                log.Info($"Invalid morph variant");
                return Task.CompletedTask;
            }

            // get the creature's display information
            Creature2DisplayGroupEntryEntry displayGroupEntry = GameTableManager.Creature2DisplayGroupEntry.Entries.FirstOrDefault(d => d.Creature2DisplayGroupId == creature2.Creature2DisplayGroupId);
            if (displayGroupEntry == null)
                return Task.CompletedTask;

            log.Info($"Morphing player into jabbit");
            // change the player's display information to the creature's display information
            context.Session.Player.SetDisplayInfo(displayGroupEntry.Creature2DisplayInfoId);
            return Task.CompletedTask;
        }

        [SubCommandHandler("kurg", "variantName - 4 variants", Permission.CommandMorph)]
        public Task KurgSubCommandHandler(CommandContext context, string command, string[] parameters)
        {
            if (parameters.Length != 1)
                return Task.CompletedTask;

            uint creatureId = 0;
            string variant = parameters[0].ToLower();

            switch(variant)
            {
                case "tan":
                    creatureId = 42293;
                    break;
                case "white":
                    creatureId = 73288;
                    break;
                case "caravantan":
                    creatureId = 41810;
                    break;
                case "caravanwhite":
                    creatureId = 24091;
                    break;
            }

            // get the creature id from the creature2 table
            Creature2Entry creature2 = GameTableManager.Creature2.GetEntry(creatureId);
            if (creature2 == null || creatureId == 0)
            {
                log.Info($"Invalid morph variant");
                return Task.CompletedTask;
            }

            // get the creature's display information
            Creature2DisplayGroupEntryEntry displayGroupEntry = GameTableManager.Creature2DisplayGroupEntry.Entries.FirstOrDefault(d => d.Creature2DisplayGroupId == creature2.Creature2DisplayGroupId);
            if (displayGroupEntry == null)
                return Task.CompletedTask;

            log.Info($"Morphing player into kurg");
            // change the player's display information to the creature's display information
            context.Session.Player.SetDisplayInfo(displayGroupEntry.Creature2DisplayInfoId);
            return Task.CompletedTask;
        }

        [SubCommandHandler("pumera", "variantName - 15 variants", Permission.CommandMorph)]
        public Task PumeraSubCommandHandler(CommandContext context, string command, string[] parameters)
        {
            if (parameters.Length != 1)
                return Task.CompletedTask;

            uint creatureId = 0;
            string variant = parameters[0].ToLower();

            switch(variant)
            {
                case "chua":
                    creatureId = 69430;
                    break;
                case "frosted":
                    creatureId = 69421;
                    break;
                case "golden":
                    creatureId = 69424;
                    break;
                case "grey":
                    creatureId = 69417;
                    break;
                case "magenta":
                    creatureId = 69423;
                    break;
                case "maroon":
                    creatureId = 69420;
                    break;
                case "sabertooth":
                    creatureId = 69427;
                    break;
                case "sienna":
                    creatureId = 69422;
                    break;
                case "snowy":
                    creatureId = 69418;
                    break;
                case "snowstripe":
                    creatureId = 69428;
                    break;
                case "steely":
                    creatureId = 69426;
                    break;
                case "strain":
                    creatureId = 69432;
                    break;
                case "tawny":
                    creatureId = 69419;
                    break;
                case "torine":
                    creatureId = 69429;
                    break;
                case "whitevale":
                    creatureId = 69425;
                    break;
            }

            // get the creature id from the creature2 table
            Creature2Entry creature2 = GameTableManager.Creature2.GetEntry(creatureId);
            if (creature2 == null || creatureId == 0)
            {
                log.Info($"Invalid morph variant");
                return Task.CompletedTask;
            }

            // get the creature's display information
            Creature2DisplayGroupEntryEntry displayGroupEntry = GameTableManager.Creature2DisplayGroupEntry.Entries.FirstOrDefault(d => d.Creature2DisplayGroupId == creature2.Creature2DisplayGroupId);
            if (displayGroupEntry == null)
                return Task.CompletedTask;

            log.Info($"Morphing player into pumera");
            // change the player's display information to the creature's display information
            context.Session.Player.SetDisplayInfo(displayGroupEntry.Creature2DisplayInfoId);
            return Task.CompletedTask;
        }

        [SubCommandHandler("ekose", "variantName - 8 variants", Permission.CommandMorph)]
        public Task EkoseSubCommandHandler(CommandContext context, string command, string[] parameters)
        {
            if (parameters.Length != 1)
                return Task.CompletedTask;

            uint creatureId = 0;
            string variant = parameters[0].ToLower();

            switch(variant)
            {
                case "female":
                    creatureId = 32219;
                    break;
                case "spacefemale":
                    creatureId = 29797;
                    break;
                case "bluemale":
                    creatureId = 6520;
                    break;
                case "greenmale":
                    creatureId = 45195;
                    break;
                case "greenspacemale":
                    creatureId = 46921;
                    break;
                case "redmale":
                    creatureId = 1684;
                    break;
                case "yellowmale":
                    creatureId = 45009;
                    break;
                case "redSpacemale":
                    creatureId = 29798;
                    break;
            }

            // get the creature id from the creature2 table
            Creature2Entry creature2 = GameTableManager.Creature2.GetEntry(creatureId);
            if (creature2 == null || creatureId == 0)
            {
                log.Info($"Invalid morph variant");
                return Task.CompletedTask;
            }

            // get the creature's display information
            Creature2DisplayGroupEntryEntry displayGroupEntry = GameTableManager.Creature2DisplayGroupEntry.Entries.FirstOrDefault(d => d.Creature2DisplayGroupId == creature2.Creature2DisplayGroupId);
            if (displayGroupEntry == null)
                return Task.CompletedTask;

            log.Info($"Morphing player into ekose");
            // change the player's display information to the creature's display information
            context.Session.Player.SetDisplayInfo(displayGroupEntry.Creature2DisplayInfoId);
            return Task.CompletedTask;
        }

        [SubCommandHandler("grumpel", "variantName - 2 variants", Permission.CommandMorph)]
        public Task GrumpelSubCommandHandler(CommandContext context, string command, string[] parameters)
        {
            if (parameters.Length != 1)
                return Task.CompletedTask;

            uint creatureId = 0;
            string variant = parameters[0].ToLower();

            switch (variant)
            {
                case "base":
                    creatureId = 17381;
                    break;
                case "space":
                    creatureId = 75253;
                    break;
            }

            // get the creature id from the creature2 table
            Creature2Entry creature2 = GameTableManager.Creature2.GetEntry(creatureId);
            if (creature2 == null || creatureId == 0)
            {
                log.Info($"Invalid morph variant");
                return Task.CompletedTask;
            }

            // get the creature's display information
            Creature2DisplayGroupEntryEntry displayGroupEntry = GameTableManager.Creature2DisplayGroupEntry.Entries.FirstOrDefault(d => d.Creature2DisplayGroupId == creature2.Creature2DisplayGroupId);
            if (displayGroupEntry == null)
                return Task.CompletedTask;

            log.Info($"Morphing player into grumpel");
            // change the player's display information to the creature's display information
            context.Session.Player.SetDisplayInfo(displayGroupEntry.Creature2DisplayInfoId);
            return Task.CompletedTask;

        }

        [SubCommandHandler("ikthian", "variantName - 7 variants", Permission.CommandMorph)]
        public Task IkthianSubCommandHandler(CommandContext context, string command, string[] parameters)
        {
            if (parameters.Length != 1)
                return Task.CompletedTask;

            uint creatureId = 0;
            string variant = parameters[0].ToLower();
            switch (variant)
            {
                case "base":
                    creatureId = 21436;
                    break;
                case "base2":
                    creatureId = 26034;
                    break;
                case "claws":
                    creatureId = 21373;
                    break;
                case "armor1":
                    creatureId = 21439;
                    break;
                case "armor2":
                    creatureId = 27765;
                    break;
                case "armor3":
                    creatureId = 27769;
                    break;
                case "armor4":
                    creatureId = 28508;
                    break;
            }

            // get the creature id from the creature2 table
            Creature2Entry creature2 = GameTableManager.Creature2.GetEntry(creatureId);
            if (creature2 == null || creatureId == 0)
            {
                log.Info($"Invalid morph variant");
                return Task.CompletedTask;
            }

            // get the creature's display information
            Creature2DisplayGroupEntryEntry displayGroupEntry = GameTableManager.Creature2DisplayGroupEntry.Entries.FirstOrDefault(d => d.Creature2DisplayGroupId == creature2.Creature2DisplayGroupId);
            if (displayGroupEntry == null)
                return Task.CompletedTask;

            log.Info($"Morphing player into ikthian");
            // change the player's display information to the creature's display information
            context.Session.Player.SetDisplayInfo(displayGroupEntry.Creature2DisplayInfoId);
            return Task.CompletedTask;
        }

        [SubCommandHandler("krogg", "variantName - 2 variants", Permission.CommandMorph)]
        public Task KroggSubCommandHandler(CommandContext context, string command, string[] parameters)
        {
            if (parameters.Length != 1)
                return Task.CompletedTask;

            uint creatureId = 0;
            string variant = parameters[0].ToLower();
            switch (variant)
            {
                case "base":
                    creatureId = 19729;
                    break;
                case "highwayman":
                    creatureId = 23804;
                    break;
            }

            // get the creature id from the creature2 table
            Creature2Entry creature2 = GameTableManager.Creature2.GetEntry(creatureId);
            if (creature2 == null || creatureId == 0)
            {
                log.Info($"Invalid morph variant");
                return Task.CompletedTask;
            }

            // get the creature's display information
            Creature2DisplayGroupEntryEntry displayGroupEntry = GameTableManager.Creature2DisplayGroupEntry.Entries.FirstOrDefault(d => d.Creature2DisplayGroupId == creature2.Creature2DisplayGroupId);
            if (displayGroupEntry == null)
                return Task.CompletedTask;

            log.Info($"Morphing player into krogg");
            // change the player's display information to the creature's display information
            context.Session.Player.SetDisplayInfo(displayGroupEntry.Creature2DisplayInfoId);
            return Task.CompletedTask;
        }

        [SubCommandHandler("lopp", "variantName - 15 variants", Permission.CommandMorph)]
        public Task LoppSubCommandHandler(CommandContext context, string command, string[] parameters)
        {
            if (parameters.Length != 1)
                return Task.CompletedTask;

            uint creatureId = 0;
            string variant = parameters[0].ToLower();
            switch (variant)
            {
                case "bluefemale":
                    creatureId = 24142;
                    break;
                case "greenfemale":
                    creatureId = 25285;
                    break;
                case "greenspacefemale":
                    creatureId = 29906;
                    break;
                case "flower":
                    creatureId = 20810;
                    break;
                case "redfemale":
                    creatureId = 24353;
                    break;
                case "redspacefemale":
                    creatureId = 27915;
                    break;
                case "marshal":
                    creatureId = 20809;
                    break;
                case "greenmale":
                    creatureId = 24116;
                    break;
                case "greenspacemale":
                    creatureId = 28348;
                    break;
                case "redmale":
                    creatureId = 25283;
                    break;
                case "redspacemale":
                    creatureId = 28346;
                    break;
                case "yellowmale":
                    creatureId = 24118;
                    break;
                case "yellowspacemale":
                    creatureId = 28347;
                    break;
                case "snowfemale":
                    creatureId = 24361;
                    break;
                case "snowmale":
                    creatureId = 11010;
                    break;
            }

            // get the creature id from the creature2 table
            Creature2Entry creature2 = GameTableManager.Creature2.GetEntry(creatureId);
            if (creature2 == null || creatureId == 0)
            {
                log.Info($"Invalid morph variant");
                return Task.CompletedTask;
            }

            // get the creature's display information
            Creature2DisplayGroupEntryEntry displayGroupEntry = GameTableManager.Creature2DisplayGroupEntry.Entries.FirstOrDefault(d => d.Creature2DisplayGroupId == creature2.Creature2DisplayGroupId);
            if (displayGroupEntry == null)
                return Task.CompletedTask;

            log.Info($"Morphing player into lopp");
            // change the player's display information to the creature's display information
            context.Session.Player.SetDisplayInfo(displayGroupEntry.Creature2DisplayInfoId);
            return Task.CompletedTask;
        }

        [SubCommandHandler("nerid", "variantName - 2 variants", Permission.CommandMorph)]
        public Task NeridSubCommandHandler(CommandContext context, string command, string[] parameters)
        {
            if (parameters.Length != 1)
                return Task.CompletedTask;

            uint creatureId = 0;
            string variant = parameters[0].ToLower();
            switch (variant)
            {
                case "blueBase":
                    creatureId = 30602;
                    break;
                case "blue":
                    creatureId = 26044;
                    break;
            }

            // get the creature id from the creature2 table
            Creature2Entry creature2 = GameTableManager.Creature2.GetEntry(creatureId);
            if (creature2 == null || creatureId == 0)
            {
                log.Info($"Invalid morph variant");
                return Task.CompletedTask;
            }

            // get the creature's display information
            Creature2DisplayGroupEntryEntry displayGroupEntry = GameTableManager.Creature2DisplayGroupEntry.Entries.FirstOrDefault(d => d.Creature2DisplayGroupId == creature2.Creature2DisplayGroupId);
            if (displayGroupEntry == null)
                return Task.CompletedTask;

            log.Info($"Morphing player into nerid");
            // change the player's display information to the creature's display information
            context.Session.Player.SetDisplayInfo(displayGroupEntry.Creature2DisplayInfoId);
            return Task.CompletedTask;
        }

        [SubCommandHandler("pell", "variantName - 16 variants", Permission.CommandMorph)]
        public Task PellSubCommandHandler(CommandContext context, string command, string[] parameters)
        {
            if (parameters.Length != 1)
                return Task.CompletedTask;

            uint creatureId = 0;
            string variant = parameters[0].ToLower();
            switch (variant)
            {
                case "augmented":
                    creatureId = 8257;
                    break;
                case "brown":
                    creatureId = 14397;
                    break;
                case "brown2":
                    creatureId = 21615;
                    break;
                case "brown3":
                    creatureId = 43195;
                    break;
                case "brown4":
                    creatureId = 41703;
                    break;
                case "brownarmored":
                    creatureId = 70873;
                    break;
                case "brownarmored2":
                    creatureId = 20706;
                    break;
                case "brownarmored3":
                    creatureId = 20708;
                    break;
                case "greybase":
                    creatureId = 30464;
                    break;
                case "grey":
                    creatureId = 26603;
                    break;
                case "grey2":
                    creatureId = 30507;
                    break;
                case "grey3":
                    creatureId = 75458;
                    break;
                case "greyarmored":
                    creatureId = 30450;
                    break;
                case "greyarmored2":
                    creatureId = 30462;
                    break;
                case "greyarmored3":
                    creatureId = 49352;
                    break;
                case "greyarmored4":
                    creatureId = 49346;
                    break;
            }

            // get the creature id from the creature2 table
            Creature2Entry creature2 = GameTableManager.Creature2.GetEntry(creatureId);
            if (creature2 == null || creatureId == 0)
            {
                log.Info($"Invalid morph variant");
                return Task.CompletedTask;
            }

            // get the creature's display information
            Creature2DisplayGroupEntryEntry displayGroupEntry = GameTableManager.Creature2DisplayGroupEntry.Entries.FirstOrDefault(d => d.Creature2DisplayGroupId == creature2.Creature2DisplayGroupId);
            if (displayGroupEntry == null)
                return Task.CompletedTask;

            log.Info($"Morphing player into pell");
            // change the player's display information to the creature's display information
            context.Session.Player.SetDisplayInfo(displayGroupEntry.Creature2DisplayInfoId);
            return Task.CompletedTask;
        }

        [SubCommandHandler("demorph", "removes the player's current morph", Permission.CommandMorph)]
        public Task DemorphSubCommandHandler(CommandContext context, string command, string[] parameters)
        {
            // remove current morph
            context.Session.Player.ResetAppearance();
            return Task.CompletedTask;
        }
    }
}
