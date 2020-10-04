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
    [Name("Summon", Permission.None)]
    public class SummonCommandHandler : CommandCategory
    {
        private static readonly ILogger log = LogManager.GetCurrentClassLogger();
        public SummonCommandHandler()
            : base(true, "summon")
        {
        }


        [SubCommandHandler("entity", "creature2Id - summons entity to the player's location", Permission.CommandSummonEntity)]
        public Task EntitySubCommandHandler(CommandContext context, string command, string[] parameters)
        {
            if (parameters.Length != 1)
                return Task.CompletedTask;
            
            if (context.Session.Player.VanityPetGuid != null)
            {
                return context.SendErrorAsync("You already have a pet - please dismiss it before summoning another.");
            }
            else
            {
                uint creatureId;
                uint.TryParse(parameters[0], out creatureId);
                Creature2Entry creature2 = GameTableManager.Creature2.GetEntry(creatureId);
                Creature2OutfitGroupEntryEntry outfitGroupEntry = GameTableManager.Creature2OutfitGroupEntry.Entries.FirstOrDefault(d => d.Creature2OutfitGroupId == creature2.Creature2OutfitGroupId);

                log.Info($"Summoning entity {creature2.Id}: '{creature2.Description}' to {context.Session.Player.Position}");

                var tempEntity = new VanityPet(context.Session.Player, creatureId);
                tempEntity.SetDisplayInfo(tempEntity.DisplayInfo, outfitGroupEntry.Creature2OutfitInfoId);
                context.Session.Player.Map.EnqueueAdd(tempEntity, context.Session.Player.Position);
                return Task.CompletedTask;
            }
        }

        [SubCommandHandler("disguise", "creature2Id - changes player disguise", Permission.CommandSummonDisguise)]
        public Task DisguiseSubCommandHandler(CommandContext context, string command, string[] parameters)
        {
            if (parameters.Length != 1)
                return Task.CompletedTask;

            uint creatureId;
            uint.TryParse(parameters[0], out creatureId);

            Creature2Entry creature2 = GameTableManager.Creature2.GetEntry(creatureId);
            if (creature2 == null)
                return Task.CompletedTask;

            Creature2DisplayGroupEntryEntry displayGroupEntry = GameTableManager.Creature2DisplayGroupEntry.Entries.FirstOrDefault(d => d.Creature2DisplayGroupId == creature2.Creature2DisplayGroupId);
            if (displayGroupEntry == null)
                return Task.CompletedTask;

            Creature2OutfitGroupEntryEntry outfitGroupEntry = GameTableManager.Creature2OutfitGroupEntry.Entries.FirstOrDefault(d => d.Creature2OutfitGroupId == creature2.Creature2OutfitGroupId);

            if (outfitGroupEntry != null) // check if the creature has an outfit
            {
                context.Session.Player.SetDisplayInfo(displayGroupEntry.Creature2DisplayInfoId, outfitGroupEntry.Creature2OutfitInfoId); // if there is outfit information, use outfit info parameter
            }
            else
            {
                context.Session.Player.SetDisplayInfo(displayGroupEntry.Creature2DisplayInfoId); 
            }
            return Task.CompletedTask;
        }

        // deprecated - use !morph demorph
        /*
        [SubCommandHandler("clear", "clears player disguise", Permission.CommandSummonClear)]
        public Task ClearSubCommandHandler(CommandContext context, string command, string[] parameters)
        {
            //clear disguise
            context.Session.Player.ResetAppearance();
            return Task.CompletedTask;
        }
        */
    }
}