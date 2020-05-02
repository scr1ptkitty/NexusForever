using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using NexusForever.Shared;
using NexusForever.Shared.GameTable;
using NexusForever.Shared.GameTable.Model;
using NexusForever.Shared.Network;
using NexusForever.WorldServer.Command.Attributes;
using NexusForever.WorldServer.Command.Contexts;
using NexusForever.WorldServer.Game.Account.Static;
using NexusForever.WorldServer.Database.Character.Model;
using NexusForever.WorldServer.Game.Housing;
using NexusForever.WorldServer.Game.Social;
using NexusForever.WorldServer.Network;
using NexusForever.WorldServer.Network.Message.Model;
using NLog;

namespace NexusForever.WorldServer.Command.Handler
{
    [Name("Teleport", Permission.None)]
    public class TeleportCommandHandler : CommandCategory
    {
        private static readonly ILogger log = LogManager.GetCurrentClassLogger();
        public TeleportCommandHandler()
            : base(true, "teleport", "port")
        {
        }

        [SubCommandHandler("coordinates", "x, y, z, [worldId] - Teleport to the specified coordinates optionally specifying the world.", Permission.CommandTeleportCoords)]
        public async Task TeleportCoordinatesSubCommandHandler(CommandContext context, string command, string[] parameters)
        {
            if (parameters.Length < 3 || parameters.Length > 4
                || !float.TryParse(parameters[0], out float x)
                || !float.TryParse(parameters[1], out float y)
                || !float.TryParse(parameters[2], out float z))
            {
                await SendHelpAsync(context);
                return;
            }

            if (!context.Session.Player.CanTeleport())
            {
                await context.SendErrorAsync("You have a pending teleport! Please wait to use this command.");
                return;
            }

            if (parameters.Length == 4)
            {
                // optional world parameter is supplied, make sure it is valid too
                if (!ushort.TryParse(parameters[3], out ushort worldId) || parameters[3] == "1229")
                {
                    if(parameters[3] == "1229")
                    {
                        context.Session.EnqueueMessageEncrypted(new ServerChat
                        {
                            Guid = context.Session.Player.Guid,
                            Channel = ChatChannel.System,
                            Text = "Oi! No using the teleport coordinates command for housing!"
                        });
                        log.Info($"{context.Session.Player.Name} : teleport coordinates : player tried to go to housing");
                    }
                    context.Session.EnqueueMessageEncrypted(new ServerChat
                    {
                        Guid = context.Session.Player.Guid,
                        Channel = ChatChannel.System,
                        Text = "Teleportation coordinates invalid."
                    });
                    log.Info($"{context.Session.Player.Name} : teleport coordinates : coordinates invalid");
                    return;
                }

                context.Session.Player.TeleportTo(worldId, x, y, z);
            }
            else if(context.Session.Player.Map.Entry.Id == 1229)
            {
                context.Session.EnqueueMessageEncrypted(new ServerChat
                {
                    Guid = context.Session.Player.Guid,
                    Channel = ChatChannel.System,
                    Text = "Using the teleport coordinates command while at player housing is not recommended. Please use !go to relocate somewhere else first."
                });
                log.Info($"{context.Session.Player.Name} : teleport coordinates : player in housing");
                return;
            }
            else
                context.Session.Player.TeleportTo((ushort)context.Session.Player.Map.Entry.Id, x, y, z);
        }

        [SubCommandHandler("location", "worldLocation2Id - Teleport to the specified world location.", Permission.CommandTeleportLoc)]
        public async Task TeleportLocationSubCommandHandler(CommandContext context, string command, string[] parameters)
        {
            if (parameters.Length != 1 || !uint.TryParse(parameters[0], out uint worldLocation2Id))
            {
                await SendHelpAsync(context);
                return;
            }

            if (!context.Session.Player.CanTeleport())
            {
                await context.SendErrorAsync("You have a pending teleport! Please wait to use this command.");
                return;
            }

            WorldLocation2Entry entry = GameTableManager.WorldLocation2.GetEntry(worldLocation2Id);
            if (entry == null)
            {
                await context.SendMessageAsync($"WorldLocation2 entry not found: {worldLocation2Id}");
                log.Info($"{context.Session.Player.Name} : teleport location : location not found");
                return;
            }

            var rotation = new Quaternion(entry.Facing0, entry.Facing1, entry.Facing2, entry.Facing3);
            context.Session.Player.Rotation = rotation.ToEulerDegrees();
            context.Session.Player.TeleportTo((ushort)entry.WorldId, entry.Position0, entry.Position1, entry.Position2);
        }

        [SubCommandHandler("to", "playername - teleport to another player's location.", Permission.CommandTeleportCoords)]
        public async Task TeleportToSubCommandHandler(CommandContext context, string command, string[] parameters)
        {
            if (!context.Session.Player.CanTeleport())
            {
                await context.SendErrorAsync("You have a pending teleport! Please wait to use this command.");
                return;
            }

            //find online players to teleport to
            List<WorldSession> allSessions = NetworkManager<WorldSession>.GetSessions().ToList();
            string name = string.Join(" ", parameters);

            if (name != "")
            {
                foreach (WorldSession whoSession in allSessions)
                {
                    if (whoSession.Player == null)
                        continue;

                    if (whoSession.Player.IsLoading)
                        continue;

                    if (whoSession.Player.Zone == null)
                        continue;

                    if (whoSession.Player.Name == name)
                    {
                        if (whoSession.Player.Map.Entry.Id == 1229)
                        {
                            await context.SendMessageAsync($"{name} is on their house plot! Try using !house teleport {name} instead!");
                            log.Info($"{context.Session.Player.Name} : teleport to : target in housing");
                        }
                        else
                        {
                            log.Info($"{context.Session.Player.Name} : teleport to");
                            context.Session.Player.TeleportTo((ushort)whoSession.Player.Map.Entry.Id, whoSession.Player.Position.X, whoSession.Player.Position.Y + 2, whoSession.Player.Position.Z);
                        }
                    }
                    else
                    {
                        //do nothing
                    }

                }
            }
            else
            {
                await context.SendMessageAsync($"Name not valid.");
                log.Info($"{context.Session.Player.Name} : teleport to : target name not valid");
            }

        }
    }
}
