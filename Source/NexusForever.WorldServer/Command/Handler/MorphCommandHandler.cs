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

        [SubCommandHandler("dagun", "variantName - black, grim, purple, strain, spacefaring, silver, white", Permission.CommandMorph)]
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

        [SubCommandHandler("dawngrazer", "variantName - blue, brown, grey, strain, tan, white, zebra", Permission.CommandMorph)]
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

        [SubCommandHandler("jabbit", "variantName - augmented, blue, brown, grey, strain", Permission.CommandMorph)]
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

        [SubCommandHandler("kurg", "variantName - tan, white, caravanTan, caravanWhite", Permission.CommandMorph)]
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

        [SubCommandHandler("pumera", "variantName - chua, frosted, golden, grey, magenta, maroon, sabertooth, sienna, snowy, snowStripe, steely, strain, tawny, torine, whitevale", Permission.CommandMorph)]
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

        [SubCommandHandler("demorph", "removes the player's current morph", Permission.CommandMorph)]
        public Task DemorphSubCommandHandler(CommandContext context, string command, string[] parameters)
        {
            // remove current morph
            context.Session.Player.ResetAppearance();
            return Task.CompletedTask;
        }
    }
}
