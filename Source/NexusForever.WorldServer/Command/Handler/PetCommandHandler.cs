using System.Threading.Tasks;
using NexusForever.WorldServer.Command.Attributes;
using NexusForever.WorldServer.Command.Contexts;
using NexusForever.WorldServer.Game.Account.Static;

namespace NexusForever.WorldServer.Command.Handler
{
    [Name("Pet", Permission.None)]
    public class PetCommandHandler : CommandCategory
    {
        public PetCommandHandler()
            : base(true, "pet")
        {
        }

        [SubCommandHandler("unlockflair", "petFlairId - Unlock a pet flair", Permission.CommandPet)]
        public Task AddFlairSubCommandHandler(CommandContext context, string command, string[] parameters)
        {
            if (parameters.Length != 1)
                return Task.CompletedTask;

            context.Session.Player.PetCustomisationManager.UnlockFlair(ushort.Parse(parameters[0]));
            return Task.CompletedTask;
        }

        [SubCommandHandler("dismiss", "dismiss the current pet", Permission.CommandPet)]
        public Task DismissSubCommandHandler(CommandContext context, string command, string[] parameters)
        {
            context.Session.Player.DestroyPet();

            return Task.CompletedTask;
        }

        [SubCommandHandler("stay", "command your pet to stay in its current position", Permission.CommandPet)]
        public Task StaySubCommandHandler(CommandContext context, string command, string[] parameters)
        {
            context.Session.Player.SetPetFollowing(false);
            context.SendMessageAsync($"Vanity pet set to stay.");

            return Task.CompletedTask;
        }

        [SubCommandHandler("side", "pet will stay on player's left when following", Permission.CommandPet)]
        public Task SideSubCommandHandler(CommandContext context, string command, string[] parameters)
        {
            context.Session.Player.SetPetFollowingOnSide(true);
            context.SendMessageAsync($"Vanity pet set to walk on the player's side.");
            return Task.CompletedTask;
        }
        [SubCommandHandler("behind", "pet will stay behind the player when following", Permission.CommandPet)]
        public Task BehindSubCommandHandler(CommandContext context, string command, string[] parameters)
        {
            context.Session.Player.SetPetFollowingOnSide(false);
            context.SendMessageAsync($"Vanity pet set to walk behind the player.");
            return Task.CompletedTask;
        }

        [SubCommandHandler("follow", "distance - command your pet to follow you at short, medium or long distance", Permission.CommandPet)]
        public Task FollowSubCommandHandler(CommandContext context, string command, string[] parameters)
        {
            float followDistance = 4f;
            float recalcDistance = 5f;

            string distanceParameter = "";
            if (parameters.Length != 1)
            {
                distanceParameter = "medium";
            }
            distanceParameter = parameters[0].ToLower();

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
            context.Session.Player.SetPetFollowDistance(followDistance);
            context.Session.Player.SetPetFollowRecalculateDistance(recalcDistance);
            context.SendMessageAsync($"Vanity pet set to follow player. Follow distance: {distanceParameter}");

            return Task.CompletedTask;
        }
    }
}
