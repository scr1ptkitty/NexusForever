using System;
using System.Collections.Generic;
using NexusForever.Shared.GameTable;
using NexusForever.Shared.GameTable.Model;
using NexusForever.Shared.Network;
using NexusForever.Shared.Network.Message;
using NexusForever.WorldServer.Command;
using NexusForever.WorldServer.Game.Entity.Static;
using NexusForever.WorldServer.Game.Social;
using NexusForever.WorldServer.Network.Message.Model;
using NexusForever.WorldServer.Network.Message.Model.Shared;
using NLog;

namespace NexusForever.WorldServer.Network.Message.Handler
{
    public static class SocialHandler
    {
        private static readonly Logger log = LogManager.GetCurrentClassLogger();

        public static readonly string CommandPrefix = "!";

        [MessageHandler(GameMessageOpcode.ClientChat)]
        public static void HandleChat(WorldSession session, ClientChat chat)
        {
            if (chat.Message.StartsWith(CommandPrefix))
            {
                try
                {
                    CommandManager.Instance.HandleCommand(session, chat.Message, true);
                }
                catch (Exception e)
                {
                    log.Warn($"{e.Message}: {e.StackTrace}");
                }
            }
            else
                SocialManager.Instance.HandleClientChat(session, chat);
        }

        [MessageHandler(GameMessageOpcode.ClientEmote)]
        public static void HandleEmote(WorldSession session, ClientEmote emote)
        {
            uint emoteId = emote.EmoteId;
            uint standState = 0;
            if (emoteId != 0)
            {
                EmotesEntry entry = GameTableManager.Instance.Emotes.GetEntry(emote.EmoteId);
                if (entry == null)
                    throw (new InvalidPacketValueException("HandleEmote: Invalid EmoteId"));

                standState = entry.StandState;
            }
            session.Player.EnqueueToVisible(new ServerEmote
            {
                Guid = session.Player.Guid,
                StandState = standState,
                EmoteId = emoteId
            });
        }

        [MessageHandler(GameMessageOpcode.ClientWhoRequest)]
        public static void HandleWhoRequest(WorldSession session, ClientWhoRequest request)
        {
            List<ServerWhoResponse.WhoPlayer> players = new List<ServerWhoResponse.WhoPlayer>
            {
                new ServerWhoResponse.WhoPlayer
                {
                    Name = session.Player.Name,
                    Level = session.Player.Level,
                    Race = session.Player.Race,
                    Class = session.Player.Class,
                    Path = session.Player.Path,
                    Faction = session.Player.Faction,
                    Sex = session.Player.Sex,
                    Zone = session.Player.Zone.Id
                }
            };

            session.EnqueueMessageEncrypted(new ServerWhoResponse
            {
                Players = players
            });
        }

        [MessageHandler(GameMessageOpcode.ClientChatWhisper)]
        public static void HandleWhisper(WorldSession session, ClientChatWhisper whisper)
        {
            SocialManager.Instance.HandleWhisperChat(session, whisper);
        }
    }
}
