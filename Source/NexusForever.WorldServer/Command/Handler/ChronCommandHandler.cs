using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization.Internal;
using NexusForever.Shared.Network;
using NexusForever.WorldServer.Command.Attributes;
using NexusForever.WorldServer.Command.Contexts;
using NexusForever.WorldServer.Game.Account;
using NexusForever.WorldServer.Game.Account.Static;
using NexusForever.WorldServer.Game.Social;
using NexusForever.WorldServer.Network;
using NexusForever.WorldServer.Network.Message.Model;
using NexusForever.WorldServer.Network.Message.Model.Shared;

namespace NexusForever.WorldServer.Command.Handler
{
    [Name("Chron", Permission.CommandChron)]
    public class TextHandler : NamedCommand
    {

        public TextHandler()
            : base(false, "chron", "send a message to another player's datachron channel")
        {
        }

        protected override Task HandleCommandAsync(CommandContext context, string command, string[] parameters)
        {
            bool isText;

            if (parameters.Length <= 1)
            {
                context.SendErrorAsync("Invalid parameters - must include the recipient's name and a message, separated by a colon (':') - optionally specify if the message should be formatted as a text");
                return Task.CompletedTask;
            }

        /* ********** begin parse logic ********** */
            int nameIndex;
            bool reachedEndOfName = false;
            string recipientName = "";

            if (parameters[0].ToLower().Equals("text") || parameters[0].ToLower().Equals("txt"))
            {
                nameIndex = 1;
                isText = true;
            }
            else
            {
                nameIndex = 0;
                isText = false;
            }

            while (!reachedEndOfName && nameIndex <= parameters.Length)
            {
                if (parameters[nameIndex].EndsWith(":"))
                {
                    reachedEndOfName = true;
                    recipientName += parameters[nameIndex];
                }
                else
                {
                    recipientName += (parameters[nameIndex] + " ");
                    nameIndex++;
                }
            }

            if (!recipientName.EndsWith(":") || recipientName.Length <= 0 || recipientName.Equals(null))
            {
                context.SendErrorAsync("Invalid name - name is empty, null, or improperly formatted!");
                return Task.CompletedTask;
            }
            recipientName = recipientName.Split(":")[0];

            string messageText = "";
            for (int i = nameIndex + 1; i < parameters.Length; i++)
            {
                messageText += (parameters[i] + " ");
            }
            messageText = messageText.Trim();
            if (messageText.Length <= 0 || messageText.Equals(null))
            {
                context.SendErrorAsync("Invalid message - message is empty, null, or improperly formatted!");
                return Task.CompletedTask;
            }

            string messageEchoText = messageText;
            string messageTargetText = messageText;

            if (isText)
            {
                messageEchoText = "[TXT]> " + messageText;
                messageTargetText = "[TXT]< " + messageText;
            }
            /* *********** end parse logic *********** */

            WorldSession targetSession = NetworkManager<WorldSession>.GetSession(s => s.Player?.Name == recipientName);

            if (targetSession != null)
            {
                if (targetSession == context.Session)
                {
                    context.SendErrorAsync("You cannot send a message to yourself.");
                    return Task.CompletedTask;
                }

                // echo message
                context.Session.EnqueueMessageEncrypted(new ServerChat
                {
                    Guid = context.Session.Player.Guid,
                    Channel = ChatChannel.Datachron,
                    Name = "to " + targetSession.Player.Name,
                    Self = true,
                    Text = messageEchoText
                });

                // target message
                targetSession.EnqueueMessageEncrypted(new ServerChat
                {
                    Channel = ChatChannel.Datachron,
                    Name = "from " + context.Session.Player.Name,
                    Text = messageTargetText
                });
            }
            else
            {
                context.SendErrorAsync($"Player {recipientName} not found.");
            }
            
            return Task.CompletedTask;
        }
    }
}
