using NexusForever.WorldServer.Command.Attributes;
using NexusForever.WorldServer.Command.Contexts;
using NexusForever.WorldServer.Command.Helper;
using NexusForever.WorldServer.Game.Account.Static;
using NexusForever.WorldServer.Network.Message.Handler;
using NexusForever.WorldServer.Network.Message.Model;
using NLog;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NexusForever.WorldServer.Command.Handler
{
    [Name("Emote", Permission.CommandEmote)]
    public class EmoteCommandHandler : NamedCommand
    {
        private static readonly ILogger log = LogManager.GetCurrentClassLogger();
        public EmoteCommandHandler()
            : base(true, "emote")
        {
        }

        protected override async Task HandleCommandAsync(CommandContext context, string command, string[] parameters)
        {
            if (parameters.Length < 1 || parameters.Length > 2 || parameters == null)
            {
                await context.SendErrorAsync("Invalid number of parameters.");
                return;
            }

            string emoteName = parameters[0].ToLower();
            if (emoteName.Equals("") || emoteName.Equals(null))
            {
                await context.SendErrorAsync("Invalid emote name.");
                return;
            }

            log.Info($"EmoteCommand : Emote lookup");
            await context.SendMessageAsync($"Loading emote: {emoteName}");

            try 
            {
                await EmoteHelper.IsExclusive(emoteName, context);
                if (!EmoteHelper.IsSelectedEmoteExclusive)
                {
                    await EmoteHelper.GetLegalEmoteId(emoteName, context);
                    await PlayEmote(context, EmoteHelper.SelectedEmoteId);
                }
            }
            catch (TypeInitializationException tie)
            {
                log.Error(tie.ToString());
            }
        }

        public Task PlayEmote(CommandContext context, uint emoteId)
        {
            ClientEmote clientEmote = new ClientEmote
            {
                EmoteId = emoteId,
                Targeted = false,
                Silent = false
            };

            log.Info($"EmoteCommand : PlayEmote: playing emote ID: {emoteId}");
            SocialHandler.HandleEmote(context.Session, clientEmote);

            return Task.CompletedTask;
        }
    }
}
