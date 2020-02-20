using NexusForever.Shared.Game.Events;
using NexusForever.Shared.Network;
using NexusForever.Shared.Network.Message;
using NexusForever.WorldServer.Database.Character;
using NexusForever.WorldServer.Database.Character.Model;
using NexusForever.WorldServer.Game.Contact.Static;
using NexusForever.WorldServer.Game.Entity;
using NexusForever.WorldServer.Game.Entity.Static;
using NexusForever.WorldServer.Game.Map.Search;
using NexusForever.WorldServer.Game.Social;
using NexusForever.WorldServer.Network.Message.Model;
using NexusForever.WorldServer.Network.Message.Model.Shared;
using System;
using System.Collections.Generic;
using NLog;

namespace NexusForever.WorldServer.Network.Message.Handler
{
    public static class MiscHandler
    {
        private static readonly ILogger log = LogManager.GetCurrentClassLogger();
        private const float LocalChatDistance = 175f;
        
        [MessageHandler(GameMessageOpcode.ClientPing)]
        public static void HandlePing(WorldSession session, ClientPing ping)
        {
            session.Heartbeat.OnHeartbeat();
        }

        /// <summary>
        /// Handled responses to Player Info Requests.
        /// TODO: Put this in the right place, this is used by Mail & Contacts, at minimum. Probably used by Guilds, Circles, etc. too.
        /// </summary>
        /// <param name="session"></param>
        /// <param name="request"></param>
        [MessageHandler(GameMessageOpcode.ClientPlayerInfoRequest)]
        public static void HandlePlayerInfoRequest(WorldSession session, ClientPlayerInfoRequest request)
        {
            session.EnqueueEvent(new TaskGenericEvent<Character>(CharacterDatabase.GetCharacterById(request.Identity.CharacterId),
                character =>
            {
                if (character == null)
                    throw new InvalidPacketValueException();

                if (request.Type == ContactType.Ignore) // Ignored user data request
                    session.EnqueueMessageEncrypted(new ServerPlayerInfoBasicResponse
                    {
                        ResultCode = 0,
                        Identity = new TargetPlayerIdentity
                        {
                            RealmId = WorldServer.RealmId,
                            CharacterId = character.Id
                        },
                        Name = character.Name,
                        Faction = (Faction)character.FactionId,
                    });
                else
                    session.EnqueueMessageEncrypted(new ServerPlayerInfoFullResponse
                    {
                        BaseData = new ServerPlayerInfoBasicResponse
                        {
                            ResultCode = 0,
                            Identity = new TargetPlayerIdentity
                            {
                                RealmId = WorldServer.RealmId,
                                CharacterId = character.Id
                            },
                            Name = character.Name,
                            Faction = (Faction)character.FactionId
                        },
                        IsClassPathSet = true,
                        Path = (Path)character.ActivePath,
                        Class = (Class)character.Class,
                        Level = character.Level,
                        IsLastLoggedOnInDaysSet = true,
                        LastLoggedInDays = NetworkManager<WorldSession>.GetSession(s => s.Player?.CharacterId == character.Id) != null ? 0 : -30f // TODO: Get Last Online from DB & Calculate Time Offline (Hard coded for 30 days currently)
                    });
            }));
            
        }

        [MessageHandler(GameMessageOpcode.ClientToggleWeapons)]
        public static void HandleWeaponToggle(WorldSession session, ClientToggleWeapons toggleWeapons)
        {
            session.Player.Sheathed = toggleWeapons.ToggleState;
        }

        [MessageHandler(GameMessageOpcode.ClientRandomRollRequest)]
        public static void HandleRandomRoll(WorldSession session, ClientRandomRollRequest randomRoll)
        {
            if (randomRoll.MinRandom > randomRoll.MaxRandom)
                throw new InvalidPacketValueException();

            if (randomRoll.MaxRandom > 1000000u)
                throw new InvalidPacketValueException();

            int RandomRollResult = new Random().Next((int)randomRoll.MinRandom, (int)randomRoll.MaxRandom);
            ServerChat serverChat = new ServerChat
            {
                Guid = session.Player.Guid,
                Channel = ChatChannel.Emote, // roll result to emote channel
                Text = $"♥♦♣♠ {session.Player.Name} rolled {RandomRollResult} ({randomRoll.MinRandom} - {randomRoll.MaxRandom}) ♠♣♦♥"
            };

            // get players in local chat range
            session.Player.Map.Search(
                session.Player.Position,
                LocalChatDistance,
                new SearchCheckRangePlayerOnly(session.Player.Position, LocalChatDistance, session.Player),
                out List<GridEntity> intersectedEntities
            );

            // send roll result to emote channel for players in local range
            log.Info($"{session.Player.Name} rolled {RandomRollResult} ({randomRoll.MinRandom} - {randomRoll.MaxRandom})");
            intersectedEntities.ForEach(e => ((Player)e).Session.EnqueueMessageEncrypted(serverChat));
            session.Player.Session.EnqueueMessageEncrypted(serverChat); // send to player's own emote channel as well?

            log.Info($"{session.Player.Name} : random roll : begin enqueue message encrypted");
            session.EnqueueMessageEncrypted(new ServerRandomRollResponse
            {
                TargetPlayerIdentity = new TargetPlayerIdentity
                {
                    RealmId = WorldServer.RealmId,
                    CharacterId = session.Player.CharacterId
                },
                MinRandom = randomRoll.MinRandom,
                MaxRandom = randomRoll.MaxRandom,
                RandomRollResult = RandomRollResult
                
            });
        }
    }
}
