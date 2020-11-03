using System.Threading.Tasks;
using NexusForever.WorldServer.Command.Attributes;
using NexusForever.WorldServer.Command.Contexts;
using NLog;
using NexusForever.WorldServer.Command.Helper;
using NexusForever.WorldServer.Game.Account.Static;
using NexusForever.Shared.GameTable.Model;
using NexusForever.Shared.GameTable;
using System.Linq;
using NexusForever.Shared.Database.Auth.Model;
using System;

namespace NexusForever.WorldServer.Command.Handler
{
    [Name("Morph", Permission.CommandMorph)]
    public class MorphCommandHandler : NamedCommand
    {
        private static readonly ILogger log = LogManager.GetCurrentClassLogger();

        public MorphCommandHandler()
            : base(true, "morph")
        {
        }

        protected override async Task HandleCommandAsync(CommandContext context, string command, string[] parameters)
        {
            if (parameters.Length < 1 || parameters.Length > 2 || parameters == null)
            {
                await context.SendErrorAsync("Invalid number of parameters.");
                return;
            }

                string subCommand = parameters[0].ToLower();
            if (subCommand.Equals("") || subCommand.Equals(null))
            {
                await context.SendErrorAsync("Invalid morph type or subcommand.");
                return;
            }

            bool isStoryteller = false;
            foreach (AccountPermission permission in context.Session.Account.AccountPermission)
            {
                if (permission.PermissionId == 43)
                    isStoryteller = true;
            }
            log.Info($"MorphCommand : Player is Storyteller: {isStoryteller}");

            if (subCommand.Equals("demorph"))
            {
                // remove current morph
                context.Session.Player.ResetAppearance();
                await context.SendMessageAsync("Player appearance reset.");
                return;
            }
            else
            {
                log.Info($"MorphCommand : Attempt to morph");

                log.Info($"MorphCommand : Variant lookup");
                string creatureVariant;
                if (parameters.Length == 1)
                {
                    creatureVariant = "";
                }
                else
                {
                    creatureVariant = parameters[1].ToLower();
                }
                await context.SendMessageAsync($"Getting {subCommand.ToUpper()} variant: {creatureVariant}");

                try
                {
                    await SummonedCreatureHelper.GetLegalCreatureIdForSummon(subCommand, creatureVariant, context);
                    log.Info($"MorphCommand : CreatureID: {SummonedCreatureHelper.SelectedCreatureId}");

                    bool isStorytellerType = false;
                    await SummonedCreatureHelper.IsStorytellerOnly(subCommand, context);
                    if (isStorytellerType && isStoryteller == false)
                    {
                        log.Info($"MorphCommand : Player is not Storyteller");
                        await context.SendErrorAsync($"Your account lacks permission to use this Storyteller Only morph: {subCommand}");
                        return;
                    }
                }
                catch (TypeInitializationException tie)
                {
                    log.Error(tie.ToString());
                }


                log.Info($"MorphCommand : Player is morphing");
                await ChangePlayerDisguise(context, SummonedCreatureHelper.SelectedCreatureId);
                return;
            }
        }

        public Task ChangePlayerDisguise(CommandContext context, uint creatureId)
        {
            // get the creature id from the creature2 table
            Creature2Entry creature2 = GameTableManager.Creature2.GetEntry(creatureId);

            if (creature2 == null || creatureId == 0)
            {
                log.Info($"{context.Session.Player.Name} : morph : invalid variant");
                return context.SendErrorAsync($"Invalid morph variant.");
            }

            // get the creature's display information
            Creature2DisplayGroupEntryEntry displayGroupEntry = GameTableManager.Creature2DisplayGroupEntry.Entries.FirstOrDefault(d => d.Creature2DisplayGroupId == creature2.Creature2DisplayGroupId);
            if (displayGroupEntry == null)
                return Task.CompletedTask;

            // change the player's display information to the creature's display information
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
    }
}
