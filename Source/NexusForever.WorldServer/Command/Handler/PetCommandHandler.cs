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
    }
}
