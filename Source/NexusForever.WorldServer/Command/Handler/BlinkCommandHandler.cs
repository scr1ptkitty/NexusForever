using NexusForever.WorldServer.Command.Attributes;
using NexusForever.WorldServer.Command.Contexts;
using NexusForever.WorldServer.Game.Account.Static;
using NLog;
using System.Threading.Tasks;

namespace NexusForever.WorldServer.Command.Handler
{
    [Name("Blink", Permission.None)]
    public class BlinkCommandHandler : CommandCategory
    {
        private static readonly ILogger log = LogManager.GetCurrentClassLogger();
        public BlinkCommandHandler()
            : base(true, "blink")
        {
        }

        [SubCommandHandler("north", "units - teleport 1-10 units north of your current location.", Permission.CommandBlink)]
        public async Task NorthSubCommandHandler(CommandContext context, string command, string[] parameters)
        {
            if (parameters.Length != 1 || !float.TryParse(parameters[0], out float units))
            {
                await SendHelpAsync(context);
                return;
            }
            if (!context.Session.Player.CanTeleport())
            {
                await context.SendErrorAsync("You have a pending teleport! Please wait to use this command.");
                return;
            }
            if (context.Session.Player.Map.Entry.Id == 1229)
            {
                await context.SendMessageAsync("Using the Blink command is not allowed on skyplots! If you are stuck, use !go home");
                log.Info($"{context.Session.Player.Name} : blink : player in housing");
                return;
            }
            if (units < 1 || units > 10)
            {
                await context.SendErrorAsync("Please enter a blink distance between 1 and 10 units.");
                return;
            }

            log.Info($"{context.Session.Player.Name} : blink north");
            // north = negative Z axis
            context.Session.Player.TeleportTo((ushort)context.Session.Player.Map.Entry.Id, context.Session.Player.Position.X,
                    context.Session.Player.Position.Y, context.Session.Player.Position.Z-units);
        }

        [SubCommandHandler("south", "units - teleport 1-10 units south of your current location.", Permission.CommandBlink)]
        public async Task SouthSubCommandHandler(CommandContext context, string command, string[] parameters)
        {
            if (parameters.Length != 1
                || !float.TryParse(parameters[0], out float units))
            {
                await SendHelpAsync(context);
                return;
            }
            if (!context.Session.Player.CanTeleport())
            {
                await context.SendErrorAsync("You have a pending teleport! Please wait to use this command.");
                return;
            }
            if (context.Session.Player.Map.Entry.Id == 1229)
            {
                await context.SendMessageAsync("Using the Blink command is not allowed on skyplots! If you are stuck, use !go home");
                log.Info($"{context.Session.Player.Name} : blink : player in housing");
                return;
            }
            if (units < 1 || units > 10)
            {
                await context.SendErrorAsync("Please enter a blink distance between 1 and 10 units.");
                return;
            }

            log.Info($"{context.Session.Player.Name} : blink south");
            // south = positive Z axis
            context.Session.Player.TeleportTo((ushort)context.Session.Player.Map.Entry.Id, context.Session.Player.Position.X,
                    context.Session.Player.Position.Y, context.Session.Player.Position.Z+units);
        }

        [SubCommandHandler("east", "units - teleport 1-10 units east of your current location.", Permission.CommandBlink)]
        public async Task EastSubCommandHandler(CommandContext context, string command, string[] parameters)
        {
            if (parameters.Length != 1
                || !float.TryParse(parameters[0], out float units))
            {
                await SendHelpAsync(context);
                return;
            }
            if (!context.Session.Player.CanTeleport())
            {
                await context.SendErrorAsync("You have a pending teleport! Please wait to use this command.");
                return;
            }
            if (context.Session.Player.Map.Entry.Id == 1229)
            {
                await context.SendMessageAsync("Using the Blink command is not allowed on skyplots! If you are stuck, use !go home");
                log.Info($"{context.Session.Player.Name} : blink : player in housing");
                return;
            }
            if (units < 1 || units > 10)
            {
                await context.SendErrorAsync("Please enter a blink distance between 1 and 10 units.");
                return;
            }

            log.Info($"{context.Session.Player.Name} : blink east");
            // east = positive X axis
            context.Session.Player.TeleportTo((ushort)context.Session.Player.Map.Entry.Id, context.Session.Player.Position.X+units,
                    context.Session.Player.Position.Y, context.Session.Player.Position.Z);
        }

        [SubCommandHandler("west", "units - teleport 1-10 units west of your current location.", Permission.CommandBlink)]
        public async Task WestSubCommandHandler(CommandContext context, string command, string[] parameters)
        {
            if (parameters.Length != 1
                || !float.TryParse(parameters[0], out float units))
            {
                await SendHelpAsync(context);
                return;
            }
            if (!context.Session.Player.CanTeleport())
            {
                await context.SendErrorAsync("You have a pending teleport! Please wait to use this command.");
                return;
            }
            if (context.Session.Player.Map.Entry.Id == 1229)
            {
                await context.SendMessageAsync("Using the Blink command is not allowed on skyplots! If you are stuck, use !go home");
                log.Info($"{context.Session.Player.Name} : blink : player in housing");
                return;
            }
            if (units < 1 || units > 10)
            {
                await context.SendErrorAsync("Please enter a blink distance between 1 and 10 units.");
                return;
            }

            log.Info($"{context.Session.Player.Name} : blink west");
            // west = negative X axis
            context.Session.Player.TeleportTo((ushort)context.Session.Player.Map.Entry.Id, context.Session.Player.Position.X-units,
                    context.Session.Player.Position.Y, context.Session.Player.Position.Z);
        }
    }
}
