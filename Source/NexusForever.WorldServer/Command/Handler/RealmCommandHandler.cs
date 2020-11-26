using System.Threading.Tasks;
using NexusForever.Shared.Configuration;
using NexusForever.Shared.Database.Auth;
using NexusForever.Shared.Network;
using NexusForever.WorldServer.Command.Attributes;
using NexusForever.WorldServer.Command.Contexts;
using NexusForever.WorldServer.Game.Social;
using NexusForever.WorldServer.Network;
using NexusForever.WorldServer.Game.Account.Static;
using System.Linq;
using System.Collections.Generic;

namespace NexusForever.WorldServer.Command.Handler
{
    [Name("Realm Management", Permission.None)]
    public class RealmCommandHandler : CommandCategory
    {
        public RealmCommandHandler()
            : base(false, "realm", "server")
        {
        }

        [SubCommandHandler("motd", "message - Set the realm's Message of the Day and announce to the realm", Permission.CommandRealmMotd)]
        public async Task HandleMotd(CommandContext context, string subCommand, string[] parameters)
        {
            if (parameters.Length < 1)
            {
                await SendHelpAsync(context).ConfigureAwait(false);
                return;
            }

            ConfigurationManager<WorldServerConfiguration>.Config.MessageOfTheDay = string.Join(" ", parameters);
            ConfigurationManager<WorldServerConfiguration>.Save();

            string motd = ConfigurationManager<WorldServerConfiguration>.Config.MessageOfTheDay;
            foreach (WorldSession session in NetworkManager<WorldSession>.GetSessions())
                SocialManager.SendMessage(session, "MOTD: " + motd, channel: ChatChannel.Realm);

            await context.SendMessageAsync($"MOTD Updated!");
        }

        [SubCommandHandler("online", "Displays the users online", Permission.CommandRealmOnline)]
        public async Task HandleOnlineCheck(CommandContext context, string subCommand, string[] parameters)
        {
            List<WorldSession> allSessions = NetworkManager<WorldSession>.GetSessions().ToList();

            int index = 0;
            foreach (WorldSession session in allSessions)
            {
                string infoString = "";
                infoString += $"[{index++}] {session.Account?.Email} id:{session.Account?.Id}";

                if (session.Player != null)
                    infoString += $" | {session.Player?.Name}";

                infoString += $" | {session.Uptime:%d}d {session.Uptime:%h}h {session.Uptime:%m}m";

                await context.SendMessageAsync(infoString);
            }

            if (allSessions.Count == 0)
                await context.SendMessageAsync($"No sessions connected.");

            await Task.CompletedTask;
        }

        [SubCommandHandler("uptime", "Display the current uptime of the server.", Permission.CommandRealmUptime)]
        public async Task HandleUptimeCheck(CommandContext context, string subCommand, string[] parameters)
        {
            await context.SendMessageAsync($"Currently up for {WorldServer.Uptime:%d}d {WorldServer.Uptime:%h}h {WorldServer.Uptime:%m}m {WorldServer.Uptime:%s}s");

            await Task.CompletedTask;
        }
    }
}