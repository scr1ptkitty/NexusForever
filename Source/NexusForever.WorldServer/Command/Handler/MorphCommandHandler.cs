﻿using System.Threading.Tasks;
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
                default:
                    creatureId = 69407;
                    context.SendMessageAsync($"Variant invalid - morphing into default variant...");
                    break;
            }

            // get the creature id from the creature2 table
            Creature2Entry creature2 = GameTableManager.Creature2.GetEntry(creatureId);
            
            if (creature2 == null || creatureId == 0)
            {
                log.Info($"{context.Session.Player.Name} : morph : invalid variant");
                return context.SendMessageAsync($"Invalid morph variant!");
            }
            
            // get the creature's display information
            Creature2DisplayGroupEntryEntry displayGroupEntry = GameTableManager.Creature2DisplayGroupEntry.Entries.FirstOrDefault(d => d.Creature2DisplayGroupId == creature2.Creature2DisplayGroupId);
            if (displayGroupEntry == null)
                return Task.CompletedTask;

            log.Info($"Morphing {context.Session.Player.Name} into dagun");
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
                default:
                    creatureId = 69403;
                    context.SendMessageAsync($"Variant invalid - morphing into default variant...");
                    break;
            }

            // get the creature id from the creature2 table
            Creature2Entry creature2 = GameTableManager.Creature2.GetEntry(creatureId);
            if (creature2 == null || creatureId == 0)
            {
                log.Info($"{context.Session.Player.Name} : morph : invalid variant");
                return context.SendMessageAsync($"Invalid morph variant!");
            }

            // get the creature's display information
            Creature2DisplayGroupEntryEntry displayGroupEntry = GameTableManager.Creature2DisplayGroupEntry.Entries.FirstOrDefault(d => d.Creature2DisplayGroupId == creature2.Creature2DisplayGroupId);
            if (displayGroupEntry == null)
                return Task.CompletedTask;
            
            log.Info($"Morphing {context.Session.Player.Name} into dawngrazer");
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
                default:
                    creatureId = 69414;
                    context.SendMessageAsync($"Variant invalid - morphing into default variant...");
                    break;
            }

            // get the creature id from the creature2 table
            Creature2Entry creature2 = GameTableManager.Creature2.GetEntry(creatureId);
            if (creature2 == null || creatureId == 0)
            {
                log.Info($"{context.Session.Player.Name} : morph : invalid variant");
                return context.SendMessageAsync($"Invalid morph variant!");
            }

            // get the creature's display information
            Creature2DisplayGroupEntryEntry displayGroupEntry = GameTableManager.Creature2DisplayGroupEntry.Entries.FirstOrDefault(d => d.Creature2DisplayGroupId == creature2.Creature2DisplayGroupId);
            if (displayGroupEntry == null)
                return Task.CompletedTask;

            log.Info($"Morphing {context.Session.Player.Name} into jabbit");
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
                default:
                    creatureId = 42293;
                    context.SendMessageAsync($"Variant invalid - morphing into default variant...");
                    break;
            }

            // get the creature id from the creature2 table
            Creature2Entry creature2 = GameTableManager.Creature2.GetEntry(creatureId);
            if (creature2 == null || creatureId == 0)
            {
                log.Info($"{context.Session.Player.Name} : morph : invalid variant");
                return context.SendMessageAsync($"Invalid morph variant!");
            }

            // get the creature's display information
            Creature2DisplayGroupEntryEntry displayGroupEntry = GameTableManager.Creature2DisplayGroupEntry.Entries.FirstOrDefault(d => d.Creature2DisplayGroupId == creature2.Creature2DisplayGroupId);
            if (displayGroupEntry == null)
                return Task.CompletedTask;

            log.Info($"Morphing {context.Session.Player.Name} into kurg");
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
                default:
                    creatureId = 69417;
                    context.SendMessageAsync($"Variant invalid - morphing into default variant...");
                    break;
            }

            // get the creature id from the creature2 table
            Creature2Entry creature2 = GameTableManager.Creature2.GetEntry(creatureId);
            if (creature2 == null || creatureId == 0)
            {
                log.Info($"{context.Session.Player.Name} : morph : invalid variant");
                return context.SendMessageAsync($"Invalid morph variant!");
            }

            // get the creature's display information
            Creature2DisplayGroupEntryEntry displayGroupEntry = GameTableManager.Creature2DisplayGroupEntry.Entries.FirstOrDefault(d => d.Creature2DisplayGroupId == creature2.Creature2DisplayGroupId);
            if (displayGroupEntry == null)
                return Task.CompletedTask;

            log.Info($"Morphing {context.Session.Player.Name} into pumera");
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
                default:
                    creatureId = 45009;
                    context.SendMessageAsync($"Variant invalid - morphing into default variant...");
                    break;
            }

            // get the creature id from the creature2 table
            Creature2Entry creature2 = GameTableManager.Creature2.GetEntry(creatureId);
            if (creature2 == null || creatureId == 0)
            {
                log.Info($"{context.Session.Player.Name} : morph : invalid variant");
                return context.SendMessageAsync($"Invalid morph variant!");
            }

            // get the creature's display information
            Creature2DisplayGroupEntryEntry displayGroupEntry = GameTableManager.Creature2DisplayGroupEntry.Entries.FirstOrDefault(d => d.Creature2DisplayGroupId == creature2.Creature2DisplayGroupId);
            if (displayGroupEntry == null)
                return Task.CompletedTask;

            log.Info($"Morphing {context.Session.Player.Name} into ekose");
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
                default:
                    creatureId = 17381;
                    context.SendMessageAsync($"Variant invalid - morphing into default variant...");
                    break;
            }

            // get the creature id from the creature2 table
            Creature2Entry creature2 = GameTableManager.Creature2.GetEntry(creatureId);
            if (creature2 == null || creatureId == 0)
            {
                log.Info($"{context.Session.Player.Name} : morph : invalid variant");
                return context.SendMessageAsync($"Invalid morph variant!");
            }

            // get the creature's display information
            Creature2DisplayGroupEntryEntry displayGroupEntry = GameTableManager.Creature2DisplayGroupEntry.Entries.FirstOrDefault(d => d.Creature2DisplayGroupId == creature2.Creature2DisplayGroupId);
            if (displayGroupEntry == null)
                return Task.CompletedTask;

            log.Info($"Morphing {context.Session.Player.Name} into grumpel");
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
                default:
                    creatureId = 21439;
                    context.SendMessageAsync($"Variant invalid - morphing into default variant...");
                    break;
            }

            // get the creature id from the creature2 table
            Creature2Entry creature2 = GameTableManager.Creature2.GetEntry(creatureId);
            if (creature2 == null || creatureId == 0)
            {
                log.Info($"{context.Session.Player.Name} : morph : invalid variant");
                return context.SendMessageAsync($"Invalid morph variant!");
            }

            // get the creature's display information
            Creature2DisplayGroupEntryEntry displayGroupEntry = GameTableManager.Creature2DisplayGroupEntry.Entries.FirstOrDefault(d => d.Creature2DisplayGroupId == creature2.Creature2DisplayGroupId);
            if (displayGroupEntry == null)
                return Task.CompletedTask;

            log.Info($"Morphing {context.Session.Player.Name} into ikthian");
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
                default:
                    creatureId = 23804;
                    context.SendMessageAsync($"Variant invalid - morphing into default variant...");
                    break;
            }

            // get the creature id from the creature2 table
            Creature2Entry creature2 = GameTableManager.Creature2.GetEntry(creatureId);
            if (creature2 == null || creatureId == 0)
            {
                log.Info($"{context.Session.Player.Name} : morph : invalid variant");
                return context.SendMessageAsync($"Invalid morph variant!");
            }

            // get the creature's display information
            Creature2DisplayGroupEntryEntry displayGroupEntry = GameTableManager.Creature2DisplayGroupEntry.Entries.FirstOrDefault(d => d.Creature2DisplayGroupId == creature2.Creature2DisplayGroupId);
            if (displayGroupEntry == null)
                return Task.CompletedTask;

            log.Info($"Morphing {context.Session.Player.Name} into krogg");
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
                default: 
                    creatureId = 20809;
                    context.SendMessageAsync($"Variant invalid - morphing into default variant...");
                    break;
            }

            // get the creature id from the creature2 table
            Creature2Entry creature2 = GameTableManager.Creature2.GetEntry(creatureId);
            if (creature2 == null || creatureId == 0)
            {
                log.Info($"{context.Session.Player.Name} : morph : invalid variant");
                return context.SendMessageAsync($"Invalid morph variant!");
            }

            // get the creature's display information
            Creature2DisplayGroupEntryEntry displayGroupEntry = GameTableManager.Creature2DisplayGroupEntry.Entries.FirstOrDefault(d => d.Creature2DisplayGroupId == creature2.Creature2DisplayGroupId);
            if (displayGroupEntry == null)
                return Task.CompletedTask;

            log.Info($"Morphing {context.Session.Player.Name} into lopp");
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
                case "bluebase":
                    creatureId = 30602;
                    break;
                case "blue":
                    creatureId = 26044;
                    break;
                default:
                    creatureId = 26044;
                    context.SendMessageAsync($"Variant invalid - morphing into default variant...");
                    break;
            }

            // get the creature id from the creature2 table
            Creature2Entry creature2 = GameTableManager.Creature2.GetEntry(creatureId);
            if (creature2 == null || creatureId == 0)
            {
                log.Info($"{context.Session.Player.Name} : morph : invalid variant");
                return context.SendMessageAsync($"Invalid morph variant!");
            }

            // get the creature's display information
            Creature2DisplayGroupEntryEntry displayGroupEntry = GameTableManager.Creature2DisplayGroupEntry.Entries.FirstOrDefault(d => d.Creature2DisplayGroupId == creature2.Creature2DisplayGroupId);
            if (displayGroupEntry == null)
                return Task.CompletedTask;

            log.Info($"Morphing {context.Session.Player.Name} into nerid");
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
                default:
                    creatureId = 70873;
                    context.SendMessageAsync($"Variant invalid - morphing into default variant...");
                    break;
            }

            // get the creature id from the creature2 table
            Creature2Entry creature2 = GameTableManager.Creature2.GetEntry(creatureId);
            if (creature2 == null || creatureId == 0)
            {
                log.Info($"{context.Session.Player.Name} : morph : invalid variant");
                return context.SendMessageAsync($"Invalid morph variant!");
            }

            // get the creature's display information
            Creature2DisplayGroupEntryEntry displayGroupEntry = GameTableManager.Creature2DisplayGroupEntry.Entries.FirstOrDefault(d => d.Creature2DisplayGroupId == creature2.Creature2DisplayGroupId);
            if (displayGroupEntry == null)
                return Task.CompletedTask;

            log.Info($"Morphing {context.Session.Player.Name} into pell");
            // change the player's display information to the creature's display information
            context.Session.Player.SetDisplayInfo(displayGroupEntry.Creature2DisplayInfoId);
            return Task.CompletedTask;
        }

        [SubCommandHandler("elemental", "variantName - 7 variants - storyteller only", Permission.CommandMorphStoryteller)]
        public Task ElementalSubCommandHandler(CommandContext context, string command, string[] parameters)
        {
            if (parameters.Length != 1)
                return Task.CompletedTask;

            uint creatureId = 0;
            string variant = parameters[0].ToLower();
            switch (variant)
            {
                case "air":
                    creatureId = 52514;
                    break;
                case "earth":
                    creatureId = 52519;
                    break;
                case "fire":
                    creatureId = 52516;
                    break;
                case "life":
                    creatureId = 52518;
                    break;
                case "logic":
                    creatureId = 52517;
                    break;
                case "soulfrost":
                    creatureId = 75622;
                    break;
                case "water":
                    creatureId = 52515;
                    break;
                default:
                    creatureId = 52514;
                    context.SendMessageAsync($"Variant invalid - morphing into default variant...");
                    break;
            }

            // get the creature id from the creature2 table
            Creature2Entry creature2 = GameTableManager.Creature2.GetEntry(creatureId);
            if (creature2 == null || creatureId == 0)
            {
                log.Info($"{context.Session.Player.Name} : morph : invalid variant");
                return context.SendMessageAsync($"Invalid morph variant!");
            }

            // get the creature's display information
            Creature2DisplayGroupEntryEntry displayGroupEntry = GameTableManager.Creature2DisplayGroupEntry.Entries.FirstOrDefault(d => d.Creature2DisplayGroupId == creature2.Creature2DisplayGroupId);
            if (displayGroupEntry == null)
                return Task.CompletedTask;

            log.Info($"Morphing {context.Session.Player.Name} into elemental");
            // change the player's display information to the creature's display information
            context.Session.Player.SetDisplayInfo(displayGroupEntry.Creature2DisplayInfoId);
            return Task.CompletedTask;
        }

        [SubCommandHandler("strain", "variantName - 10 variants - storyteller only", Permission.CommandMorphStoryteller)]
        public Task StrainSubCommandHandler(CommandContext context, string command, string[] parameters)
        {
            if (parameters.Length != 1)
                return Task.CompletedTask;

            uint creatureId = 0;
            string variant = parameters[0].ToLower();
            switch (variant)
            {
                case "brute":
                    creatureId = 55050;
                    break;
                case "technobrute":
                    creatureId = 52970;
                    break;
                case "corruptor":
                    creatureId = 30208;
                    break;
                case "technocorruptor":
                    creatureId = 52963;
                    break;
                case "crawler":
                    creatureId = 48146;
                    break;
                case "mauler":
                    creatureId = 55010;
                    break;
                case "technomauler":
                    creatureId = 52964;
                    break;
                case "peep":
                    creatureId = 37962;
                    break;
                case "ravager":
                    creatureId = 30210;
                    break;
                case "technoravager":
                    creatureId = 52968;
                    break;
                default:
                    creatureId = 48146;
                    context.SendMessageAsync($"Variant invalid - morphing into default variant...");
                    break;
            }

            // get the creature id from the creature2 table
            Creature2Entry creature2 = GameTableManager.Creature2.GetEntry(creatureId);
            if (creature2 == null || creatureId == 0)
            {
                log.Info($"{context.Session.Player.Name} : morph : invalid variant");
                return context.SendMessageAsync($"Invalid morph variant!");
            }

            // get the creature's display information
            Creature2DisplayGroupEntryEntry displayGroupEntry = GameTableManager.Creature2DisplayGroupEntry.Entries.FirstOrDefault(d => d.Creature2DisplayGroupId == creature2.Creature2DisplayGroupId);
            if (displayGroupEntry == null)
                return Task.CompletedTask;

            log.Info($"Morphing {context.Session.Player.Name} into strain");
            // change the player's display information to the creature's display information
            context.Session.Player.SetDisplayInfo(displayGroupEntry.Creature2DisplayInfoId);
            return Task.CompletedTask;
        }

        [SubCommandHandler("osun", "variantName - 20 variants - storyteller only", Permission.CommandMorphStoryteller)]
        public Task OsunSubCommandHandler(CommandContext context, string command, string[] parameters)
        {
            if (parameters.Length != 1)
                return Task.CompletedTask;

            uint creatureId = 0;
            string variant = parameters[0].ToLower();
            switch (variant)
            {
                case "warlord":
                    creatureId = 13021;
                    break;
                case "icewarlord":
                    creatureId = 75614;
                    break;
                case "icewarlord2":
                    creatureId = 75459;
                    break;
                case "redwarlord":
                    creatureId = 70444;
                    break;
                case "warrior":
                    creatureId = 13019;
                    break;
                case "icewarrior":
                    creatureId = 75615;
                    break;
                case "redwarrior":
                    creatureId = 73099;
                    break;
                case "warrior2":
                    creatureId = 14342;
                    break;
                case "icewarrior2":
                    creatureId = 71367;
                    break;
                case "warrior3":
                    creatureId = 14343;
                    break;
                case "redwarrior3":
                    creatureId = 48177;
                    break;
                case "strainwarrior":
                    creatureId = 55016;
                    break;
                case "witch":
                    creatureId = 15202;
                    break;
                case "witch2":
                    creatureId = 71767;
                    break;
                case "blueghostwitch":
                    creatureId = 75621;
                    break;
                case "redghostwitch":
                    creatureId = 75617;
                    break;
                case "redwitch2":
                    creatureId = 48295;
                    break;
                case "icewitch":
                    creatureId = 71250;
                    break;
                case "icewitch2":
                    creatureId = 70373;
                    break;
                case "strainwitch":
                    creatureId = 52969;
                    break;
                default:
                    creatureId = 13019;
                    context.SendMessageAsync($"Variant invalid - morphing into default variant...");
                    break;
            }

            // get the creature id from the creature2 table
            Creature2Entry creature2 = GameTableManager.Creature2.GetEntry(creatureId);
            if (creature2 == null || creatureId == 0)
            {
                log.Info($"{context.Session.Player.Name} : morph : invalid variant");
                return context.SendMessageAsync($"Invalid morph variant!");
            }

            // get the creature's display information
            Creature2DisplayGroupEntryEntry displayGroupEntry = GameTableManager.Creature2DisplayGroupEntry.Entries.FirstOrDefault(d => d.Creature2DisplayGroupId == creature2.Creature2DisplayGroupId);
            if (displayGroupEntry == null)
                return Task.CompletedTask;

            log.Info($"Morphing {context.Session.Player.Name} into osun");
            // change the player's display information to the creature's display information
            context.Session.Player.SetDisplayInfo(displayGroupEntry.Creature2DisplayInfoId);
            return Task.CompletedTask;
        }

        [SubCommandHandler("highpriest", "variantName - 6 variants - storyteller only", Permission.CommandMorphStoryteller)]
        public Task HighPriestSubCommandHandler(CommandContext context, string command, string[] parameters)
        {
            if (parameters.Length != 1)
                return Task.CompletedTask;

            uint creatureId = 0;
            string variant = parameters[0].ToLower();
            switch (variant)
            {
                case "armored":
                    creatureId = 48554;
                    break;
                case "armored2":
                    creatureId = 70445;
                    break;
                case "armoredwhite":
                    creatureId = 70893;
                    break;
                case "base":
                    creatureId = 42948;
                    break;
                case "dark":
                    creatureId = 75509;
                    break;
                case "strain":
                    creatureId = 55015;
                    break;
                default:
                    creatureId = 48554;
                    context.SendMessageAsync($"Variant invalid - morphing into default variant...");
                    break;
            }

            // get the creature id from the creature2 table
            Creature2Entry creature2 = GameTableManager.Creature2.GetEntry(creatureId);
            if (creature2 == null || creatureId == 0)
            {
                log.Info($"{context.Session.Player.Name} : morph : invalid variant");
                return context.SendMessageAsync($"Invalid morph variant!");
            }

            // get the creature's display information
            Creature2DisplayGroupEntryEntry displayGroupEntry = GameTableManager.Creature2DisplayGroupEntry.Entries.FirstOrDefault(d => d.Creature2DisplayGroupId == creature2.Creature2DisplayGroupId);
            if (displayGroupEntry == null)
                return Task.CompletedTask;

            log.Info($"Morphing {context.Session.Player.Name} into high priest");
            // change the player's display information to the creature's display information
            context.Session.Player.SetDisplayInfo(displayGroupEntry.Creature2DisplayInfoId);
            return Task.CompletedTask;
        }

        [SubCommandHandler("triton", "variantName - 5 variants - storyteller only", Permission.CommandMorphStoryteller)]
        public Task TritonSubCommandHandler(CommandContext context, string command, string[] parameters)
        {
            if (parameters.Length != 1)
                return Task.CompletedTask;

            uint creatureId = 0;
            string variant = parameters[0].ToLower();
            switch (variant)
            {
                case "armored":
                    creatureId = 34094;
                    break;
                case "armored2":
                    creatureId = 48592;
                    break;
                case "armored3":
                    creatureId = 61635;
                    break;
                case "base":
                    creatureId = 34093;
                    break;
                case "strain":
                    creatureId = 55018;
                    break;
                default:
                    creatureId = 34094;
                    context.SendMessageAsync($"Variant invalid - morphing into default variant...");
                    break;
            }

            // get the creature id from the creature2 table
            Creature2Entry creature2 = GameTableManager.Creature2.GetEntry(creatureId);
            if (creature2 == null || creatureId == 0)
            {
                log.Info($"{context.Session.Player.Name} : morph : invalid variant");
                return context.SendMessageAsync($"Invalid morph variant!");
            }

            // get the creature's display information
            Creature2DisplayGroupEntryEntry displayGroupEntry = GameTableManager.Creature2DisplayGroupEntry.Entries.FirstOrDefault(d => d.Creature2DisplayGroupId == creature2.Creature2DisplayGroupId);
            if (displayGroupEntry == null)
                return Task.CompletedTask;

            log.Info($"Morphing {context.Session.Player.Name} into triton");
            // change the player's display information to the creature's display information
            context.Session.Player.SetDisplayInfo(displayGroupEntry.Creature2DisplayInfoId);
            return Task.CompletedTask;
        }

        [SubCommandHandler("oghra", "variantName - 11 variants", Permission.CommandMorph)]
        public Task OghraSubCommandHandler(CommandContext context, string command, string[] parameters)
        {
            if (parameters.Length != 1)
                return Task.CompletedTask;

            uint creatureId = 0;
            string variant = parameters[0].ToLower();
            switch (variant)
            {
                case "skirt":
                    creatureId = 4089;
                    break;
                case "coat":
                    creatureId = 4091;
                    break;
                case "vest":
                    creatureId = 15756;
                    break;
                case "duster":
                    creatureId = 33426;
                    break;
                case "captain":
                    creatureId = 28079;
                    break;
                case "skin":
                    creatureId = 9633;
                    break;
                case "grimduster":
                    creatureId = 27875;
                    break;
                case "grimvest":
                    creatureId = 63557;
                    break;
                case "augmented":
                    creatureId = 65441;
                    break;
                case "zombievest":
                    creatureId = 75681;
                    break;
                case "zombieskin":
                    creatureId = 69110;
                    break;
                default:
                    creatureId = 4089;
                    context.SendMessageAsync($"Variant invalid - morphing into default variant...");
                    break;
            }

            // get the creature id from the creature2 table
            Creature2Entry creature2 = GameTableManager.Creature2.GetEntry(creatureId);
            if (creature2 == null || creatureId == 0)
            {
                return context.SendMessageAsync($"Invalid morph variant!");
            }

            // get the creature's display information
            Creature2DisplayGroupEntryEntry displayGroupEntry = GameTableManager.Creature2DisplayGroupEntry.Entries.FirstOrDefault(d => d.Creature2DisplayGroupId == creature2.Creature2DisplayGroupId);
            if (displayGroupEntry == null)
                return Task.CompletedTask;

            log.Info($"Morphing {context.Session.Player.Name} into oghra");
            // change the player's display information to the creature's display information
            context.Session.Player.SetDisplayInfo(displayGroupEntry.Creature2DisplayInfoId);
            return Task.CompletedTask;
        }

        [SubCommandHandler("grund", "variantName - 16 variants", Permission.CommandMorph)]
        public Task GrundSubCommandHandler(CommandContext context, string command, string[] parameters)
        {
            if (parameters.Length != 1)
                return Task.CompletedTask;

            uint creatureId = 0;
            string variant = parameters[0].ToLower();
            switch (variant)
            {
                case "red":
                    creatureId = 9634;
                    break;
                case "red2":
                    creatureId = 23588;
                    break;
                case "red3":
                    creatureId = 24951;
                    break;
                case "red4":
                    creatureId = 24986;
                    break;
                case "red5":
                    creatureId = 53395;
                    break;
                case "red6":
                    creatureId = 61901;
                    break;
                case "white":
                    creatureId = 18624;
                    break;
                case "white2":
                    creatureId = 68841;
                    break;
                case "white3":
                    creatureId = 8982;
                    break;
                case "white5":
                    creatureId = 63252;
                    break;
                case "augmented":
                    creatureId = 48649;
                    break;
                case "grim":
                    creatureId = 63556;
                    break;
                case "grim3":
                    creatureId = 11902;
                    break;
                case "grim6":
                    creatureId = 70196;
                    break;
                case "zombie":
                    creatureId = 68632;
                    break;
                case "zombie2":
                    creatureId = 68633;
                    break;
                default:
                    creatureId = 9634;
                    context.SendMessageAsync($"Variant invalid - morphing into default variant...");
                    break;
            }

            // get the creature id from the creature2 table
            Creature2Entry creature2 = GameTableManager.Creature2.GetEntry(creatureId);
            if (creature2 == null || creatureId == 0)
            {
                return context.SendMessageAsync($"Invalid morph variant!");
            }

            // get the creature's display information
            Creature2DisplayGroupEntryEntry displayGroupEntry = GameTableManager.Creature2DisplayGroupEntry.Entries.FirstOrDefault(d => d.Creature2DisplayGroupId == creature2.Creature2DisplayGroupId);
            if (displayGroupEntry == null)
                return Task.CompletedTask;

            log.Info($"Morphing {context.Session.Player.Name} into grund");
            // change the player's display information to the creature's display information
            context.Session.Player.SetDisplayInfo(displayGroupEntry.Creature2DisplayInfoId);
            return Task.CompletedTask;
        }

        [SubCommandHandler("eeklu", "variantName - 16 variants", Permission.CommandMorph)]
        public Task EekluSubCommandHandler(CommandContext context, string command, string[] parameters)
        {
            if (parameters.Length != 1)
                return Task.CompletedTask;

            uint creatureId = 0;
            string variant = parameters[0].ToLower();
            switch (variant)
            {
                case "belt":
                    creatureId = 4087;
                    break;
                case "belt2":
                    creatureId = 75355;
                    break;
                case "maskbelt":
                    creatureId = 72895;
                    break;
                case "jacket":
                    creatureId = 24950;
                    break;
                case "duster":
                    creatureId = 24981;
                    break;
                case "buccaneer":
                    creatureId = 24983;
                    break;
                case "corsair":
                    creatureId = 25007;
                    break;
                case "captain":
                    creatureId = 27609;
                    break;
                case "grimjacket":
                    creatureId = 45979;
                    break;
                case "grimduster":
                    creatureId = 27895;
                    break;
                case "grimbuccaneer":
                    creatureId = 27873;
                    break;
                case "pinkjacket":
                    creatureId = 27903;
                    break;
                case "pinkbuccaneer":
                    creatureId = 63269;
                    break;
                case "pinkcaptain":
                    creatureId = 28187;
                    break;
                case "zombie":
                    creatureId = 75432;
                    break;
                case "zombiebelt":
                    creatureId = 72760;
                    break;
                default:
                    creatureId = 4087;
                    context.SendMessageAsync($"Variant invalid - morphing into default variant...");
                    break;
            }

            // get the creature id from the creature2 table
            Creature2Entry creature2 = GameTableManager.Creature2.GetEntry(creatureId);
            if (creature2 == null || creatureId == 0)
            {
                return context.SendMessageAsync($"Invalid morph variant!");
            }

            // get the creature's display information
            Creature2DisplayGroupEntryEntry displayGroupEntry = GameTableManager.Creature2DisplayGroupEntry.Entries.FirstOrDefault(d => d.Creature2DisplayGroupId == creature2.Creature2DisplayGroupId);
            if (displayGroupEntry == null)
                return Task.CompletedTask;

            log.Info($"Morphing {context.Session.Player.Name} into eeklu");
            // change the player's display information to the creature's display information
            context.Session.Player.SetDisplayInfo(displayGroupEntry.Creature2DisplayInfoId);
            return Task.CompletedTask;
        }

        [SubCommandHandler("freebot", "variantName - 11", Permission.CommandMorph)]
        public Task FreebotSubCommandHandler(CommandContext context, string command, string[] parameters)
        {
            if (parameters.Length != 1)
                return Task.CompletedTask;

            uint creatureId = 0;
            string variant = parameters[0].ToLower();
            switch (variant)
            {
                case "blue":
                    creatureId = 26449;
                    break;
                case "bluedrills":
                    creatureId = 39034;
                    break;
                case "blueneedles":
                    creatureId = 24382;
                    break;
                case "bronze":
                    creatureId = 75865;
                    break;
                case "bronzeclubs":
                    creatureId = 24381;
                    break;
                case "green":
                    creatureId = 14271;
                    break;
                case "reddrills":
                    creatureId = 70341;
                    break;
                case "silverclubs":
                    creatureId = 19778;
                    break;
                case "heavyblue":
                    creatureId = 52112;
                    break;
                case "heavygold":
                    creatureId = 59184;
                    break;
                case "heavygrey":
                    creatureId = 61852;
                    break;
                case "heavyred":
                    creatureId = 61854;
                    break;
                case "pellprobe":
                    creatureId = 49366;
                    break;
                default:
                    creatureId = 26449;
                    context.SendMessageAsync($"Variant invalid - morphing into default variant...");
                    break;
            }

            // get the creature id from the creature2 table
            Creature2Entry creature2 = GameTableManager.Creature2.GetEntry(creatureId);
            if (creature2 == null || creatureId == 0)
            {
                return context.SendMessageAsync($"Invalid morph variant!");
            }

            // get the creature's display information
            Creature2DisplayGroupEntryEntry displayGroupEntry = GameTableManager.Creature2DisplayGroupEntry.Entries.FirstOrDefault(d => d.Creature2DisplayGroupId == creature2.Creature2DisplayGroupId);
            if (displayGroupEntry == null)
                return Task.CompletedTask;

            log.Info($"Morphing {context.Session.Player.Name} into freebot");
            // change the player's display information to the creature's display information
            context.Session.Player.SetDisplayInfo(displayGroupEntry.Creature2DisplayInfoId);
            return Task.CompletedTask;
        }

        [SubCommandHandler("malverine", "variantName - 6 variants", Permission.CommandMorph)]
        public Task MalverineSubCommandHandler(CommandContext context, string command, string[] parameters)
        {
            if (parameters.Length != 1)
                return Task.CompletedTask;

            uint creatureId = 0;
            string variant = parameters[0].ToLower();
            switch (variant)
            {
                case "golden":
                    creatureId = 25982;
                    break;
                case "purple":
                    creatureId = 23851;
                    break;
                case "white":
                    creatureId = 31755;
                    break;
                case "augmented":
                    creatureId = 32213;
                    break;
                case "strain":
                    creatureId = 38071;
                    break;
                case "black":
                    creatureId = 41659;
                    break;
                default:
                    creatureId = 25982;
                    context.SendMessageAsync($"Variant invalid - morphing into default variant...");
                    break;

            }

            // get the creature id from the creature2 table
            Creature2Entry creature2 = GameTableManager.Creature2.GetEntry(creatureId);
            if (creature2 == null || creatureId == 0)
            {
                return context.SendMessageAsync($"Invalid morph variant!");
            }

            // get the creature's display information
            Creature2DisplayGroupEntryEntry displayGroupEntry = GameTableManager.Creature2DisplayGroupEntry.Entries.FirstOrDefault(d => d.Creature2DisplayGroupId == creature2.Creature2DisplayGroupId);
            if (displayGroupEntry == null)
                return Task.CompletedTask;

            log.Info($"Morphing {context.Session.Player.Name} into malverine");
            // change the player's display information to the creature's display information
            context.Session.Player.SetDisplayInfo(displayGroupEntry.Creature2DisplayInfoId);
            return Task.CompletedTask;
        }

        [SubCommandHandler("vind", "base variant", Permission.CommandMorph)]
        public Task VindSubCommandHandler(CommandContext context, string command, string[] parameters)
        {
            // there's only one variant for vind!

            /*
            if (parameters.Length != 1)
                return Task.CompletedTask;
            */
            uint creatureId = 2410;
            /*
            string variant = parameters[0].ToLower();
            switch (variant)
            {
                case "":
                    creatureId = 0;
                    break;
            }
            */

            // get the creature id from the creature2 table
            Creature2Entry creature2 = GameTableManager.Creature2.GetEntry(creatureId);
            if (creature2 == null || creatureId == 0)
            {
                return context.SendMessageAsync($"Invalid morph variant!");
            }

            // get the creature's display information
            Creature2DisplayGroupEntryEntry displayGroupEntry = GameTableManager.Creature2DisplayGroupEntry.Entries.FirstOrDefault(d => d.Creature2DisplayGroupId == creature2.Creature2DisplayGroupId);
            if (displayGroupEntry == null)
                return Task.CompletedTask;

            log.Info($"Morphing {context.Session.Player.Name} into vind");
            // change the player's display information to the creature's display information
            context.Session.Player.SetDisplayInfo(displayGroupEntry.Creature2DisplayInfoId);
            return Task.CompletedTask;
        }

        [SubCommandHandler("girrok", "variantName - 10 variants", Permission.CommandMorph)]
        public Task GirrokSubCommandHandler(CommandContext context, string command, string[] parameters)
        {
            if (parameters.Length != 1)
                return Task.CompletedTask;

            uint creatureId = 0;
            string variant = parameters[0].ToLower();
            switch (variant)
            {
                case "black":
                    creatureId = 1753;
                    break;
                case "brown":
                    creatureId = 17646;
                    break;
                case "augmented":
                    creatureId = 19741;
                    break;
                case "white":
                    creatureId = 19742;
                    break;
                case "purple":
                    creatureId = 19969;
                    break;
                case "bone":
                    creatureId = 23775;
                    break;
                case "skeledroid":
                    creatureId = 26201;
                    break;
                case "scarred":
                    creatureId = 26846;
                    break;
                case "strain":
                    creatureId = 38289;
                    break;
                case "purplestripe":
                    creatureId = 45505;
                    break;
                default:
                    creatureId = 1753;
                    context.SendMessageAsync($"Variant invalid - morphing into default variant...");
                    break;
            }

            // get the creature id from the creature2 table
            Creature2Entry creature2 = GameTableManager.Creature2.GetEntry(creatureId);
            if (creature2 == null || creatureId == 0)
            {
                return context.SendMessageAsync($"Invalid morph variant!");
            }

            // get the creature's display information
            Creature2DisplayGroupEntryEntry displayGroupEntry = GameTableManager.Creature2DisplayGroupEntry.Entries.FirstOrDefault(d => d.Creature2DisplayGroupId == creature2.Creature2DisplayGroupId);
            if (displayGroupEntry == null)
                return Task.CompletedTask;

            log.Info($"Morphing {context.Session.Player.Name} into girrok");
            // change the player's display information to the creature's display information
            context.Session.Player.SetDisplayInfo(displayGroupEntry.Creature2DisplayInfoId);
            return Task.CompletedTask;
        }

        [SubCommandHandler("chompy", "variantName - 10 variants", Permission.CommandMorph)]
        public Task ChompySubCommandHandler(CommandContext context, string command, string[] parameters)
        {
            if (parameters.Length != 1)
                return Task.CompletedTask;

            uint creatureId = 0;
            string variant = parameters[0].ToLower();
            switch (variant)
            {
                case "orange":
                    creatureId = 69483;
                    break;
                case "tawny":
                    creatureId = 69485;
                    break;
                case "dusty":
                    creatureId = 69486;
                    break;
                case "ginger":
                    creatureId = 69487;
                    break;
                case "tawnydarkspur":
                    creatureId = 69488;
                    break;
                case "blue":
                    creatureId = 69489;
                    break;
                case "black":
                    creatureId = 69490;
                    break;
                case "strain":
                    creatureId = 69491;
                    break;
                case "bluedarkspur":
                    creatureId = 69492;
                    break;
                case "white":
                    creatureId = 71360;
                    break;
                default:
                    creatureId = 69483;
                    context.SendMessageAsync($"Variant invalid - morphing into default variant...");
                    break;
            }

            // get the creature id from the creature2 table
            Creature2Entry creature2 = GameTableManager.Creature2.GetEntry(creatureId);
            if (creature2 == null || creatureId == 0)
            {
                return context.SendMessageAsync($"Invalid morph variant!");
            }

            // get the creature's display information
            Creature2DisplayGroupEntryEntry displayGroupEntry = GameTableManager.Creature2DisplayGroupEntry.Entries.FirstOrDefault(d => d.Creature2DisplayGroupId == creature2.Creature2DisplayGroupId);
            if (displayGroupEntry == null)
                return Task.CompletedTask;

            log.Info($"Morphing {context.Session.Player.Name} into chompy");
            // change the player's display information to the creature's display information
            context.Session.Player.SetDisplayInfo(displayGroupEntry.Creature2DisplayInfoId);
            return Task.CompletedTask;
        }

        [SubCommandHandler("slank", "base variant ", Permission.CommandMorph)]
        public Task SlankSubCommandHandler(CommandContext context, string command, string[] parameters)
        {
            // slanks only have one variant

            /*
            if (parameters.Length != 1)
                return Task.CompletedTask;
            */
            uint creatureId = 27250;
            /*
            string variant = parameters[0].ToLower();
            switch (variant)
            {
                case "":
                    creatureId = 0;
                    break;
                default:
                    context.SendMessageAsync($"Variant invalid - morphing into default variant...");
                    break;
            }
            */

            // get the creature id from the creature2 table
            Creature2Entry creature2 = GameTableManager.Creature2.GetEntry(creatureId);
            if (creature2 == null || creatureId == 0)
            {
                return context.SendMessageAsync($"Invalid morph variant!");
            }

            // get the creature's display information
            Creature2DisplayGroupEntryEntry displayGroupEntry = GameTableManager.Creature2DisplayGroupEntry.Entries.FirstOrDefault(d => d.Creature2DisplayGroupId == creature2.Creature2DisplayGroupId);
            if (displayGroupEntry == null)
                return Task.CompletedTask;

            log.Info($"Morphing {context.Session.Player.Name} into x");
            // change the player's display information to the creature's display information
            context.Session.Player.SetDisplayInfo(displayGroupEntry.Creature2DisplayInfoId);
            return Task.CompletedTask;
        }

        [SubCommandHandler("roan", "variantName - 3 variants", Permission.CommandMorph)]
        public Task RoanSubCommandHandler(CommandContext context, string command, string[] parameters)
        {
            if (parameters.Length != 1)
                return Task.CompletedTask;

            uint creatureId = 0;
            string variant = parameters[0].ToLower();
            switch (variant)
            {
                case "brownbull":
                    creatureId = 1741;
                    break;
                case "browncow":
                    creatureId = 2065;
                    break;
                case "greybull":
                    creatureId = 15640;
                    break;
                default:
                    creatureId = 1741;
                    context.SendMessageAsync($"Variant invalid - morphing into default variant...");
                    break;
            }

            // get the creature id from the creature2 table
            Creature2Entry creature2 = GameTableManager.Creature2.GetEntry(creatureId);
            if (creature2 == null || creatureId == 0)
            {
                return context.SendMessageAsync($"Invalid morph variant!");
            }

            // get the creature's display information
            Creature2DisplayGroupEntryEntry displayGroupEntry = GameTableManager.Creature2DisplayGroupEntry.Entries.FirstOrDefault(d => d.Creature2DisplayGroupId == creature2.Creature2DisplayGroupId);
            if (displayGroupEntry == null)
                return Task.CompletedTask;

            log.Info($"Morphing {context.Session.Player.Name} into roan");
            // change the player's display information to the creature's display information
            context.Session.Player.SetDisplayInfo(displayGroupEntry.Creature2DisplayInfoId);
            return Task.CompletedTask;
        }

        [SubCommandHandler("rowsdower", "variantName - 5 variants", Permission.CommandMorph)]
        public Task RowsdowerSubCommandHandler(CommandContext context, string command, string[] parameters)
        {
            if (parameters.Length != 1)
                return Task.CompletedTask;

            uint creatureId = 0;
            string variant = parameters[0].ToLower();
            switch (variant)
            {
                case "white":
                    creatureId = 12921;
                    break;
                case "demonic":
                    creatureId = 48437;
                    break;
                case "augmented":
                    creatureId = 69474;
                    break;
                case "pink":
                    creatureId = 69475;
                    break;
                case "party":
                    creatureId = 70316;
                    break;
                default:
                    creatureId = 12921;
                    context.SendMessageAsync($"Variant invalid - morphing into default variant...");
                    break;
            }

            // get the creature id from the creature2 table
            Creature2Entry creature2 = GameTableManager.Creature2.GetEntry(creatureId);
            if (creature2 == null || creatureId == 0)
            {
                return context.SendMessageAsync($"Invalid morph variant!");
            }

            // get the creature's display information
            Creature2DisplayGroupEntryEntry displayGroupEntry = GameTableManager.Creature2DisplayGroupEntry.Entries.FirstOrDefault(d => d.Creature2DisplayGroupId == creature2.Creature2DisplayGroupId);
            if (displayGroupEntry == null)
                return Task.CompletedTask;

            log.Info($"Morphing {context.Session.Player.Name} into rowsdower");
            // change the player's display information to the creature's display information
            context.Session.Player.SetDisplayInfo(displayGroupEntry.Creature2DisplayInfoId);
            return Task.CompletedTask;
        }

        [SubCommandHandler("warbot", "variantName - 4 variants - storyteller only", Permission.CommandMorphStoryteller)]
        public Task WarbotSubCommandHandler(CommandContext context, string command, string[] parameters)
        {
            if (parameters.Length != 1)
                return Task.CompletedTask;

            uint creatureId = 0;
            string variant = parameters[0].ToLower();
            switch (variant)
            {
                case "dominion":
                    creatureId = 20998;
                    break;
                case "exile":
                    creatureId = 21544;
                    break;
                case "ikthian":
                    creatureId = 34644;
                    break;
                case "osun":
                    creatureId = 32519;
                    break;
                default:
                    creatureId = 32519;
                    context.SendMessageAsync($"Variant invalid - morphing into default variant...");
                    break;
            }

            // get the creature id from the creature2 table
            Creature2Entry creature2 = GameTableManager.Creature2.GetEntry(creatureId);
            if (creature2 == null || creatureId == 0)
            {
                return context.SendMessageAsync($"Invalid morph variant!");
            }

            // get the creature's display information
            Creature2DisplayGroupEntryEntry displayGroupEntry = GameTableManager.Creature2DisplayGroupEntry.Entries.FirstOrDefault(d => d.Creature2DisplayGroupId == creature2.Creature2DisplayGroupId);
            if (displayGroupEntry == null)
                return Task.CompletedTask;

            log.Info($"Morphing {context.Session.Player.Name} into warbot");
            // change the player's display information to the creature's display information
            context.Session.Player.SetDisplayInfo(displayGroupEntry.Creature2DisplayInfoId);
            return Task.CompletedTask;
        }

        [SubCommandHandler("tank", "variantName - 2 variants - storyteller only", Permission.CommandMorphStoryteller)]
        public Task TankSubCommandHandler(CommandContext context, string command, string[] parameters)
        {
            if (parameters.Length != 1)
                return Task.CompletedTask;

            uint creatureId = 0;
            string variant = parameters[0].ToLower();
            switch (variant)
            {
                case "exile":
                    creatureId = 41168;
                    break;
                case "dominion":
                    creatureId = 47567;
                    break;
                default:
                    creatureId = 47567;
                    context.SendMessageAsync($"Variant invalid - morphing into default variant...");
                    break;
            }

            // get the creature id from the creature2 table
            Creature2Entry creature2 = GameTableManager.Creature2.GetEntry(creatureId);
            if (creature2 == null || creatureId == 0)
            {
                return context.SendMessageAsync($"Invalid morph variant!");
            }

            // get the creature's display information
            Creature2DisplayGroupEntryEntry displayGroupEntry = GameTableManager.Creature2DisplayGroupEntry.Entries.FirstOrDefault(d => d.Creature2DisplayGroupId == creature2.Creature2DisplayGroupId);
            if (displayGroupEntry == null)
                return Task.CompletedTask;

            log.Info($"Morphing {context.Session.Player.Name} into tank");
            // change the player's display information to the creature's display information
            context.Session.Player.SetDisplayInfo(displayGroupEntry.Creature2DisplayInfoId);
            return Task.CompletedTask;
        }

        [SubCommandHandler("witchgiant", "variantName - 3 variants - storyteller only", Permission.CommandMorphStoryteller)]
        public Task WitchgiantSubCommandHandler(CommandContext context, string command, string[] parameters)
        {
            if (parameters.Length != 1)
                return Task.CompletedTask;

            uint creatureId = 0;
            string variant = parameters[0].ToLower();
            switch (variant)
            {
                case "life":
                    creatureId = 21804;
                    break;
                case "ice":
                    creatureId = 20857;
                    break;
                case "strain":
                    creatureId = 49391;
                    break;
                default:
                    creatureId = 21804;
                    context.SendMessageAsync($"Variant invalid - morphing into default variant...");
                    break;
            }

            // get the creature id from the creature2 table
            Creature2Entry creature2 = GameTableManager.Creature2.GetEntry(creatureId);
            if (creature2 == null || creatureId == 0)
            {
                return context.SendMessageAsync($"Invalid morph variant!");
            }

            // get the creature's display information
            Creature2DisplayGroupEntryEntry displayGroupEntry = GameTableManager.Creature2DisplayGroupEntry.Entries.FirstOrDefault(d => d.Creature2DisplayGroupId == creature2.Creature2DisplayGroupId);
            if (displayGroupEntry == null)
                return Task.CompletedTask;

            log.Info($"Morphing {context.Session.Player.Name} into witch giant");
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
