using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization.Internal;
using NexusForever.Shared.Network;
using NexusForever.WorldServer.Command.Attributes;
using NexusForever.WorldServer.Command.Contexts;
using NexusForever.WorldServer.Game.Account;
using NexusForever.WorldServer.Game.Account.Static;
using NexusForever.WorldServer.Game.Entity;
using NexusForever.WorldServer.Game.Map.Search;
using NexusForever.WorldServer.Game.Social;
using NexusForever.WorldServer.Network;
using NexusForever.WorldServer.Network.Message.Model;
using NexusForever.WorldServer.Network.Message.Model.Shared;
using NLog;

namespace NexusForever.WorldServer.Command.Handler
{
    [Name("XRoll", Permission.CommandXRoll)]
    public class XRollHandler : NamedCommand
    {
        private static readonly ILogger log = LogManager.GetCurrentClassLogger();
        private const float LocalChatDistance = 175f;
        public XRollHandler()
            : base(false, "xroll")
        {
        }

        protected override Task HandleCommandAsync(CommandContext context, string command, string[] parameters)
        {
            if (parameters.Length < 1 ||
                parameters[0].StartsWith("d") ||
                !parameters[0].Contains("d"))
            {
                context.SendErrorAsync("Invalid parameters - must be formatted as ( XdY Z ), where X=quantity; Y=sides; Z=modifier (optional, can be blank)");
                return Task.CompletedTask;
            }

            try
            {
                // parameter list: XdY, Z
                int quantity = int.Parse(parameters[0].ToLower().Split("d")[0]);
                int sides = int.Parse(parameters[0].ToLower().Split("d")[1]);
                int modifier = 0;
                if (parameters.Length > 1)
                {
                     modifier = int.Parse(parameters[1]);
                }

                // setup System feedback message
                string rollFeedbackSystem = "";
                string rollFeedbackLocal = "";
                if (modifier > 0)
                {
                    rollFeedbackSystem += "You roll " + quantity + "d" + sides + "+" + modifier + ": ";
                    rollFeedbackLocal += context.Session.Player.Name + " rolls (" + quantity + "d" + sides + ")+" + modifier + " : ";
                }
                else if (modifier < 0)
                {
                    rollFeedbackSystem += "You roll " + quantity + "d" + sides + "" + modifier + ": ";
                    rollFeedbackLocal += context.Session.Player.Name + " rolls (" + quantity + "d" + sides + ")" + modifier + " : ";
                }
                else
                {
                    rollFeedbackSystem += "You roll " + quantity + "d" + sides + ": ";
                    rollFeedbackLocal += context.Session.Player.Name + " rolls (" + quantity + "d" + sides + ") : ";
                }
                // handle random roll(s)
                int totalResult = 0;
                for (int i = 1; i <= quantity; i++)
                {
                    int naturalRoll = new Random().Next(1, sides + 1);
                    if (i == 1)
                    {
                        rollFeedbackSystem += " (" + naturalRoll + ")";
                        rollFeedbackLocal += " (" + naturalRoll + ")";
                    }
                    else if (i > 1 && i < 6)
                    {
                        rollFeedbackSystem += "+(" + naturalRoll + ")";
                        rollFeedbackLocal += "+(" + naturalRoll + ")";
                    }
                    else
                    {
                        rollFeedbackSystem += "+(" + naturalRoll + ")";
                        rollFeedbackLocal += "+(" + naturalRoll + ")";
                    }
                    totalResult += naturalRoll;
                }
                totalResult += modifier;
                if (modifier > 0)
                {
                    rollFeedbackSystem += "+" + modifier;
                    rollFeedbackLocal += "+" + modifier;
                }
                else if (modifier < 0)
                {
                    rollFeedbackSystem += "" + modifier;
                    rollFeedbackLocal += "" + modifier;
                }
                rollFeedbackSystem += "= " + totalResult;
                rollFeedbackLocal += " = (( " + totalResult + " ))";

                context.SendMessageAsync(rollFeedbackSystem);

                ServerChat serverChat = new ServerChat
                {
                    Guid = context.Session.Player.Guid,
                    Channel = ChatChannel.Emote, // roll result to emote channel
                    Text = rollFeedbackLocal
                };

                // get players in local chat range
                context.Session.Player.Map.Search(
                    context.Session.Player.Position,
                    LocalChatDistance,
                    new SearchCheckRangePlayerOnly(context.Session.Player.Position, LocalChatDistance, context.Session.Player),
                    out List<GridEntity> intersectedEntities
                );

                // send roll result to emote channel for players in local range
                log.Info($"{context.Session.Player.Name} xrolled {totalResult} ({parameters[0]}, {modifier})");
                intersectedEntities.ForEach(e => ((Player)e).Session.EnqueueMessageEncrypted(serverChat));
                context.Session.Player.Session.EnqueueMessageEncrypted(serverChat); // send to player's own emote channel as well?

                return Task.CompletedTask;
            }
            catch (Exception e)
            {
                log.Error(e.Message);
                context.SendErrorAsync("Invalid parameters - must be formatted as ( XdY Z ), where X=quantity; Y=sides; Z=modifier (optional, can be blank)");
                return Task.CompletedTask;
            }
        }
    }
}
