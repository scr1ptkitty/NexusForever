﻿using NexusForever.WorldServer.Command.Contexts;
using NexusForever.WorldServer.Game.Entity.Static;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexusForever.WorldServer.Command.Helper
{
    /// <summary>
    /// (GENESIS PRIME) Helper class for emote command.
    /// </summary>
    public abstract class EmoteHelper
    {
        private static readonly ILogger log = LogManager.GetCurrentClassLogger();

        public static uint SelectedEmoteId { get; set; }
        public static bool IsSelectedEmoteExclusive { get; set; }

        /// <summary>
        /// (GENESIS PRIME) Dictionary of legal emotes and their IDs.
        /// </summary>
        private static readonly Dictionary<String, uint> EmoteLibrary = new Dictionary<String, uint>()
        {
            { "chairsit", 289 },
            { "chairsit2", 288 },
            { "channeling", 280 },
            { "channeling2", 231 },
            { "channeling3", 417 },
            { "combatloop", 199 },
            { "dazed", 59 },
            { "dazedfloat", 259 },
            { "deadfloat", 134 },
            { "deadfloat2", 261 },
            { "dead", 46 },
            { "dead2", 184 },
            { "dead3", 185 },
            { "dead4", 186 },
            { "dead5", 187 },
            { "dominionpose", 291 },
            { "exilepose", 290 },
            { "falling", 214 },
            { "floating", 216 },
            { "holdobject", 83 },
            { "knockdown", 158 },
            { "laser", 96 },
            { "lounge", 425 },
            { "mount", 267 },
            { "pistolfire", 371 },
            { "readyclaws", 86 },
            { "readycombat", 43 },
            { "readycombatfloat", 269 },
            { "readylauncher", 266 },
            { "readypistols", 54 },
            { "readyrifle", 39 },
            { "readysword", 85 },
            { "shiver", 427 },
            { "staffchannel", 249 },
            { "staffraise", 155 },
            { "stealth", 156 },
            { "swordblock", 232 },
            { "talking", 97 },
            { "taxisit", 263 },
            { "tiedup", 102 },
            { "tpose", 203 },
            { "use", 42 },
            { "use2", 35 },
            { "wounded", 98 },
            { "wounded2", 99 },
            { "wounded3", 100 },
            { "wounded4", 101 }
        };

        /// <summary>
        /// (GENESIS PRIME) Dictionary that represents exclusion of certain player races from using certain emotes.
        /// </summary>
        private static readonly Dictionary<String, List<uint>> EmoteExclusionLibrary = new Dictionary<String, List<uint>>()
        {
            { "dominionpose", new List<uint>()
                {
                    3, 4, 16
                }
            },
            { "exilepose", new List<uint>()
                {
                    5, 12, 13
                }
            },
            { "staffchannel", new List<uint>()
                {
                    3, 12, 16
                }
            },
            { "staffraise", new List<uint>()
                {
                    3, 12, 16
                }
            },
            { "stealth", new List<uint>()
                {
                    3, 13
                }
            },
            { "pistolfire", new List<uint>()
                {
                    3, 12
                }
            },
            { "channeling3", new List<uint>()
                {
                    3, 12
                }
            },
            { "channeling2", new List<uint>()
                {
                    4
                }
            },
            { "swordblock", new List<uint>()
                {
                    4
                }
            },
            { "dead5", new List<uint>()
                {
                    5
                }
            }
        };

        /// <summary>
        /// (GENESIS PRIME) Returns whether the given emote and race combo is a no-go.
        /// </summary>
        public static async Task IsExclusive(string emoteName, CommandContext context)
        {
            IsSelectedEmoteExclusive = false;
            
            if (EmoteExclusionLibrary.ContainsKey(emoteName))
            {
                List<uint> exclusionList;
                if (EmoteExclusionLibrary.TryGetValue(emoteName, out exclusionList))
                {
                    foreach (uint raceId in exclusionList)
                    {
                        if (context.Session.Player.Race == (Race) raceId)
                        {
                            await context.SendMessageAsync($"{emoteName} is not a compatible emote with your character race!");
                            log.Info($"{emoteName} excludes raceId {raceId}");
                            IsSelectedEmoteExclusive = true;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// (GENESIS PRIME) Get the ID of a legal emote.
        /// </summary>
        public static async Task GetLegalEmoteId(string emoteName, CommandContext context)
        {
            SelectedEmoteId = 0;

            log.Info($"Looking up {emoteName} in the Emote Library...");
            if (EmoteLibrary.TryGetValue(emoteName, out uint returnEmoteId))
            {
                log.Info($"Found emote {emoteName}, resolved to ID: {returnEmoteId}");
                SelectedEmoteId = returnEmoteId;
                return;
            }
            else
            {
                await context.SendErrorAsync("An Emote ID for the given emote name could not be found!");
                return;
            }
        }
    }
}
