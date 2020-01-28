﻿using System;
using System.Collections.Generic;
using NexusForever.Shared.Database;
using NexusForever.Shared.Database.Auth.Model;
using NexusForever.Shared.GameTable;
using NexusForever.Shared.GameTable.Model;
using NexusForever.WorldServer.Database;
using NexusForever.WorldServer.Database.Character.Model;
using NexusForever.WorldServer.Game.Entity.Static;
using NexusForever.WorldServer.Network;
using NexusForever.WorldServer.Network.Message.Model;
using AccountModel = NexusForever.Shared.Database.Auth.Model.Account;
using AccountEntitlementModel = NexusForever.Shared.Database.Auth.Model.AccountEntitlement;
using CharacterEntitlementModel = NexusForever.WorldServer.Database.Character.Model.CharacterEntitlement;
using NLog;

namespace NexusForever.WorldServer.Game.Entity
{
    public class EntitlementManager : ISaveAuth, ISaveCharacter
    {
        private static readonly ILogger log = LogManager.GetCurrentClassLogger();
        private readonly WorldSession session;

        private readonly Dictionary<EntitlementType, AccountEntitlement> accountEntitlements
            = new Dictionary<EntitlementType, AccountEntitlement>();
        private readonly Dictionary<EntitlementType, CharacterEntitlement> characterEntitlements
            = new Dictionary<EntitlementType, CharacterEntitlement>();

        /// <summary>
        /// Create a new <see cref="EntitlementManager"/> from existing database model.
        /// </summary>
        public EntitlementManager(WorldSession session, AccountModel model)
        {
            this.session = session;

            foreach (AccountEntitlementModel entitlementModel in model.AccountEntitlement)
            {
                EntitlementEntry entry = GameTableManager.Entitlement.GetEntry(entitlementModel.EntitlementId);
                if (entry == null)
                    throw new DatabaseDataException($"Account {model.Id} has invalid entitlement {entitlementModel.EntitlementId} stored!");

                var entitlement = new AccountEntitlement(entitlementModel, entry);
                accountEntitlements.Add(entitlement.Type, entitlement);
            }
        }

        public void Save(AuthContext context)
        {
            foreach (AccountEntitlement entitlement in accountEntitlements.Values)
                entitlement.Save(context);
        }

        public void Save(CharacterContext context)
        {
            foreach (CharacterEntitlement entitlement in characterEntitlements.Values)
                entitlement.Save(context);
        }

        public IEnumerable<AccountEntitlement> GetAccountEntitlements()
        {
            return accountEntitlements.Values;
        }

        public IEnumerable<CharacterEntitlement> GetCharacterEntitlements()
        {
            return characterEntitlements.Values;
        }

        /// <summary>
        /// Return <see cref="AccountEntitlement"/> for supplied <see cref="EntitlementType"/>.
        /// </summary>
        public AccountEntitlement GetAccountEntitlement(EntitlementType type)
        {
            return accountEntitlements.TryGetValue(type, out AccountEntitlement entitlement) ? entitlement : null;
        }

        /// <summary>
        /// Return <see cref="CharacterEntitlement"/> for supplied <see cref="EntitlementType"/>.
        /// </summary>
        public CharacterEntitlement GetCharacterEntitlement(EntitlementType type)
        {
            return characterEntitlements.TryGetValue(type, out CharacterEntitlement entitlement) ? entitlement : null;
        }

        public void OnNewCharacter(Character model)
        {
            characterEntitlements.Clear();
            if (model.CharacterEntitlement.Count != 0)
            {
                foreach (CharacterEntitlementModel entitlementModel in model.CharacterEntitlement)
                {
                    EntitlementEntry entry = GameTableManager.Entitlement.GetEntry(entitlementModel.EntitlementId);
                    if (entry == null)
                        throw new DatabaseDataException($"Character {model.Id} has invalid entitlement {entitlementModel.EntitlementId} stored!");

                    var entitlement = new CharacterEntitlement(entitlementModel, entry);
                    characterEntitlements.Add(entitlement.Type, entitlement);
                }
            }
            
        }

        /// <summary>
        /// Create or update account <see cref="EntitlementType"/> with supplied value.
        /// </summary>
        /// <remarks>
        /// A positive value must be supplied for new entitlements otherwise an <see cref="ArgumentException"/> will be thrown.
        /// For existing entitlements a positive value will increment and a negative value will decrement the entitlement value.
        /// </remarks>
        public void SetAccountEntitlement(EntitlementType type, int value)
        {
            EntitlementEntry entry = GameTableManager.Entitlement.GetEntry((ulong)type);
            if (entry == null)
                throw new ArgumentException($"Invalid entitlement type {type}!");

            AccountEntitlement entitlement = SetEntitlement(accountEntitlements, entry, value,
                () => new AccountEntitlement(session.Account.Id, entry, (uint)value));

            session.EnqueueMessageEncrypted(new ServerAccountEntitlement
            {
                Entitlement = type,
                Count = entitlement.Amount
            });
        }

        /// <summary>
        /// Create or update character <see cref="EntitlementType"/> with supplied value.
        /// </summary>
        /// <remarks>
        /// A positive value must be supplied for new entitlements otherwise an <see cref="ArgumentException"/> will be thrown.
        /// For existing entitlements a positive value will increment and a negative value will decrement the entitlement value.
        /// </remarks>
        public void SetCharacterEntitlement(EntitlementType type, int value)
        {
            EntitlementEntry entry = GameTableManager.Entitlement.GetEntry((ulong)type);
            if (entry == null)
                throw new ArgumentException($"Invalid entitlement type {type}!");

            CharacterEntitlement entitlement = SetEntitlement(characterEntitlements, entry, value,
                () => new CharacterEntitlement(session.Player.CharacterId, entry, (uint)value));

            session.EnqueueMessageEncrypted(new ServerEntitlement
            {
                Entitlement = type,
                Count = entitlement.Amount
            });
        }

        private static T SetEntitlement<T>(IDictionary<EntitlementType, T> collection, EntitlementEntry entry, int value, Func<T> creator)
            where T : Entitlement
        {
            if (!collection.TryGetValue((EntitlementType)entry.Id, out T entitlement))
            {
                if (value < 1)
                    throw new ArgumentException($"Failed to create entitlement {entry.Id}, {value} isn't positive!");

                if (value > entry.MaxCount && entry.Id != 12)
                    throw new ArgumentException($"Failed to create entitlement {entry.Id}, {value} is larger than max value {entry.MaxCount}!");
                if (entry.Id == 12)
                {
                    if (value > 48)
                    {
                        log.Info($"set entitlement : initial character slot unlock exceeded 48");
                        throw new ArgumentException($"Failed to create entitlement {entry.Id}, character slot unlocks cannot exceed 48!");
                    }
                }
                entitlement = creator.Invoke();
                collection.Add(entitlement.Type, entitlement);
            }
            else
            {
                if (entry.Id != 12 && value > 0 && entitlement.Amount + (uint)value > entry.MaxCount)
                    throw new ArgumentException($"Failed to update entitlement {entry.Id}, incrementing by {value} exceeds max value!");

                if (value < 0 && (int)entitlement.Amount + value < 0)
                    throw new ArgumentException($"Failed to update entitlement {entry.Id}, decrementing by {value} subceeds 0!");

                if (entry.Id == 12 && entitlement.Amount + (uint)value > 48)
                {
                    log.Info($"set entitlement : character slot unlock increment exceeded 48");
                    throw new ArgumentException($"Failed to create entitlement {entry.Id}, incrementing by {value} exceeds 48 character slot unlocks!");
                }

                entitlement.Amount = (uint)((int)entitlement.Amount + value);
            }

            return entitlement;
        }
    }
}