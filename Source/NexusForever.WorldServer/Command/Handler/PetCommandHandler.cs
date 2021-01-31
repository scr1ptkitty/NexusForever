using System;
using System.Linq;
using System.Threading.Tasks;
using NexusForever.Shared.GameTable;
using NexusForever.Shared.GameTable.Model;
using NexusForever.WorldServer.Command.Attributes;
using NexusForever.WorldServer.Command.Contexts;
using NexusForever.WorldServer.Command.Helper;
using NexusForever.WorldServer.Game.Account;
using NexusForever.WorldServer.Game.Account.Static;
using NexusForever.WorldServer.Game.Entity;
using NLog;

namespace NexusForever.WorldServer.Command.Handler
{
    [Name("Pet", Permission.CommandPet)]
    public class PetCommandHandler : NamedCommand
    {
        private static readonly ILogger log = LogManager.GetCurrentClassLogger();

        public PetCommandHandler()
            : base(true, "pet")
        {
        }

        protected override async Task HandleCommandAsync(CommandContext context, string command, string[] parameters)
        {
            if (parameters.Length < 1 || parameters.Length > 3 || parameters == null)
            {
                await context.SendErrorAsync("Invalid number of parameters.");
                return;
            }

            string subCommand = parameters[0].ToLower();
            if (subCommand.Equals("") || subCommand.Equals(null))
            {
                await context.SendErrorAsync("Invalid pet type or subcommand.");
                return;
            }

            bool isStoryteller = false;
            if (RoleManager.HasPermission(context.Session, Permission.CommandMorphStoryteller) ||
                RoleManager.HasPermission(context.Session, Permission.GMFlag) ||
                RoleManager.HasPermission(context.Session, Permission.Everything))
            {
                isStoryteller = true;
            }
            log.Info($"PetCommand : Player is Storyteller: {isStoryteller}");

            if (subCommand.Equals("unlockflair"))
            {
                if (parameters.Length != 2)
                {
                    await context.SendErrorAsync("Invalid number of parameters.");
                    return;
                }
                
                context.Session.Player.PetCustomisationManager.UnlockFlair(ushort.Parse(parameters[0]));
                return;
            }
            else if (subCommand.Equals("dismiss"))
            {
                context.Session.Player.DestroyPet();

                return;
            }
            else if (subCommand.Equals("stay"))
            {
                context.Session.Player.SetPetFollowing(false);
                context.Session.Player.SetPetFacingPlayer(false);
                await context.SendMessageAsync($"Vanity pet set to stay.");

                return;
            }
            else if (subCommand.Equals("side"))
            {
                context.Session.Player.SetPetFollowingOnSide(true);
                context.Session.Player.SetPetFacingPlayer(false);
                await context.SendMessageAsync($"Vanity pet set to walk on the player's side.");
                return;
            }
            else if (subCommand.Equals("behind"))
            {
                context.Session.Player.SetPetFollowingOnSide(false);
                context.Session.Player.SetPetFacingPlayer(true);
                await context.SendMessageAsync($"Vanity pet set to walk behind the player.");
                return;
            }
            else if (subCommand.Equals("follow"))
            {
                float followDistance = 4f;
                float recalcDistance = 5f;

                string distanceParameter = "";
                if (parameters.Length != 2)
                {
                    distanceParameter = "medium";
                }
                distanceParameter = parameters[1].ToLower();

                switch (distanceParameter)
                {
                    case "short":
                        followDistance = 1f;
                        recalcDistance = 1f;
                        break;
                    case "medium":
                        followDistance = 4f;
                        recalcDistance = 5f;
                        break;
                    case "long":
                        followDistance = 7f;
                        recalcDistance = 9f;
                        break;
                }

                context.Session.Player.SetPetFollowing(true);
                context.Session.Player.SetPetFacingPlayer(true);
                context.Session.Player.SetPetFollowDistance(followDistance);
                context.Session.Player.SetPetFollowRecalculateDistance(recalcDistance);
                await context.SendMessageAsync($"Vanity pet set to follow player. Follow distance: {distanceParameter}");

                return;
            }
            else if (subCommand.Equals("summon"))
            {
                log.Info($"PetCommand : Attempt to summon");
                if (context.Session.Player.VanityPetGuid != null)
                {
                    await context.SendErrorAsync("You already have a pet - please dismiss it before summoning another.");
                    return;
                }

                log.Info($"PetCommand : Variant lookup");
                string creatureType = parameters[1];
                string creatureVariant;
                if (parameters.Length == 2)
                {
                    creatureVariant = "";
                }
                else
                {
                    creatureVariant = parameters[2].ToLower();
                }
                await context.SendMessageAsync($"Getting {creatureType} variant: {creatureVariant}");

                try
                {
                    await SummonedCreatureHelper.GetLegalCreatureIdForSummon(creatureType, creatureVariant, context);
                    log.Info($"PetCommand : CreatureID: {SummonedCreatureHelper.SelectedCreatureId}");

                    await SummonedCreatureHelper.IsStorytellerOnly(creatureType, context);
                    if (SummonedCreatureHelper.IsSelectedTypeStorytellerOnly && isStoryteller == false)
                    {
                        log.Info($"PetCommand : Player is not Storyteller");
                        await context.SendErrorAsync($"Your account lacks permission to use this Storyteller Only summon: {creatureType}");
                        return;
                    }
                }
                catch (TypeInitializationException tie)
                {
                    log.Error(tie.ToString());
                }

                log.Info($"PetCommand : Player is summoning");
                await SummonCreatureToPlayer(context, SummonedCreatureHelper.SelectedCreatureId);
                return;
            }
            else
            {
                await context.SendErrorAsync($"Invalid Pet subcommand.");
                return;
            }
        }

        public Task SummonCreatureToPlayer(CommandContext context, uint creatureId)
        {
            Creature2Entry creature2 = GameTableManager.Creature2.GetEntry(creatureId);

            if (creature2 == null || creatureId == 0)
            {
                log.Info($"{context.Session.Player.Name} : summon : invalid variant");
                return context.SendErrorAsync($"Invalid summon variant.");
            }

            var tempEntity = new VanityPet(context.Session.Player, creatureId);
            Creature2OutfitGroupEntryEntry outfitGroupEntry = GameTableManager.Creature2OutfitGroupEntry.Entries.FirstOrDefault(d => d.Creature2OutfitGroupId == creature2.Creature2OutfitGroupId);

            if (outfitGroupEntry != null)
            {
                tempEntity.SetDisplayInfo(tempEntity.DisplayInfo, outfitGroupEntry.Creature2OutfitInfoId);
            }
            log.Info($"Summoning entity {creature2.Id}: '{creature2.Description}' to {context.Session.Player.Position}");
            context.Session.Player.Map.EnqueueAdd(tempEntity, context.Session.Player.Position);
            
            return Task.CompletedTask;
        }
    }
}
