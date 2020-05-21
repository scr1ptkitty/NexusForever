using System.Threading.Tasks;
using NexusForever.WorldServer.Command.Attributes;
using NexusForever.WorldServer.Command.Contexts;
using NLog;
using NexusForever.WorldServer.Game.Account.Static;
using NexusForever.WorldServer.Network.Message.Model;
using NexusForever.WorldServer.Network.Message.Handler;

namespace NexusForever.WorldServer.Command.Handler
{
    [Name("Emote", Permission.None)]
    public class EmoteCommandHandler : CommandCategory
    {
        private static readonly ILogger log = LogManager.GetCurrentClassLogger();

        public EmoteCommandHandler()
            : base(true, "emote")
        {
        }

        [SubCommandHandler("id", "emoteId - applies the given emote or animation ID to the player - GM only", Permission.CommandEmoteId)]
        public Task EmoteSubCommandHandler(CommandContext context, string command, string[] parameters)
        {
            if (parameters != null)
            {
                uint emoteID = uint.Parse(parameters[0]);
                
                log.Info($"{context.Session.Player.Name} : emote : {emoteID}");
                
                ClientEmote emoteToSend = new ClientEmote
                {
                    EmoteId = emoteID,
                };

                SocialHandler.HandleEmote(context.Session, emoteToSend);
            }
            
            return Task.CompletedTask;
        }
    }
}
