﻿using NexusForever.WorldServer.Command.Contexts;
using NLog;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NexusForever.WorldServer.Command.Helper
{
    /// <summary>
    /// (GENESIS PRIME) Helper class for morph/summon commands.
    /// </summary>
    public abstract class SummonedCreatureHelper
    {
        private static readonly ILogger log = LogManager.GetCurrentClassLogger();

        public static uint SelectedCreatureId { get; set; }
        public static bool IsSelectedTypeStorytellerOnly { get; set; }

        /// <summary>
        /// (GENESIS PRIME) 2D Dictionary of legal creature type/variant combinations.
        /// </summary>
        private static readonly Dictionary<string, Dictionary<string, uint>> CreatureLibrary = new Dictionary<string, Dictionary<string, uint>>() {
            // Type: Boss (Storyteller Only)
            { "boss", new Dictionary<string, uint>(){
                { "avatus", 60057 },
                { "bevorage", 61463 },
                { "dreadwatcher", 68254 },
                { "frostgale", 70446 },
                { "jackshade", 62681 },
                { "kundar", 70447 },
                { "laveka", 65997 },
                { "mordechai", 24489 },
                { "octog", 24486 },
                { "ohmna", 49395 },
                { "stormtalon", 17163 },
                { "zombiemordechai", 65800 },
                { "zombieoctog", 72758 },
                { "zombieoctog2", 75240 }
            }},
            
            // Type: Canimid
            { "canimid", new Dictionary<string, uint>(){
                { "augmented", 26973 },
                { "augmentedred", 21609 },
                { "augmentedred2", 32981 },
                { "brown", 19299 },
                { "grey", 32526 },
                { "red", 8120 },
                { "smoke", 61994 }
            }},
            
            // Type: Chompy
            { "chompy", new Dictionary<string, uint>(){
                { "black", 69490 },
                { "blue", 69489 },
                { "bluedarkspur", 69492 },
                { "dusty", 69486 },
                { "ginger", 69487 },
                { "orange", 69483 },
                { "strain", 69491 },
                { "tawny", 69485 },
                { "tawnydarkspur", 69488 },
                { "white", 71360 }
            }},
            
            // Type: Construct (Storyteller Only)
            { "construct", new Dictionary<string, uint>(){
                { "augmentor", 32956 },
                { "commander", 37375 },
                { "darkaugmentor", 65082 },
                { "darkcommander", 34806 },
                { "darkprobe", 34739 },
                { "darkprotector", 58289 },
                { "goldaugmentor", 63525 },
                { "goldcommander", 57165 },
                { "goldprotector", 70892 },
                { "phageaugmentor", 54935 },
                { "phagecommander", 20859 },
                { "phageprobe", 20863 },
                { "phageprotector", 20865 },
                { "probe", 45865 },
                { "protector", 20866 }
            }},
            
            // Type: Dagun
            { "dagun", new Dictionary<string, uint>(){
                { "black", 69407 },
                { "grim", 45867 },
                { "purple", 69411 },
                { "silver", 69410 },
                { "spacefaring", 69409 },
                { "strain", 69408 },
                { "white", 73296 }
            }},
            
            // Type: Dawngrazer
            { "dawngrazer", new Dictionary<string, uint>(){
                { "blue", 69404 },
                { "brown", 69401 },
                { "grey", 69402 },
                { "strain", 69405 },
                { "tan", 69403 },
                { "white", 69027 },
                { "zebra", 69406 }
            }},
            
            // Type: Eeklu
            { "eeklu", new Dictionary<string, uint>(){
                { "belt", 4087 },
                { "belt2", 75355 },
                { "buccaneer", 24983 },
                { "captain", 27609 },
                { "corsair", 25007 },
                { "duster", 24981 },
                { "grimbuccaneer", 27873 },
                { "grimduster", 27895 },
                { "grimjacket", 45979 },
                { "jacket", 24950 },
                { "maskbelt", 72895 },
                { "pinkbuccaneer", 63269 },
                { "pinkcaptain", 28187 },
                { "pinkjacket", 27903 },
                { "zombie", 75432 },
                { "zombiebelt", 72760 }
            }},
            
            // Type: Ekose
            { "ekose", new Dictionary<string, uint>(){
                { "female", 32219 },
                { "femalespace", 29797 },
                { "maleblue", 6520 },
                { "malegreen", 45195 },
                { "malegreenspace", 46921 },
                { "malered", 1684 },
                { "maleredspace", 29798 },
                { "maleyellow", 45009 }
            }},

            // Type: Elemental (Storyteller Only)
            { "elemental", new Dictionary<string, uint>(){
                { "air", 52514 },
                { "earth", 52519 },
                { "fire", 52516 },
                { "life", 52518 },
                { "logic", 52517 },
                { "soulfrost", 75622 },
                { "water", 52515 }
            }},

            // Type: Falkrin
            { "falkrin", new Dictionary<string, uint>(){
                { "basemale", 20648 },
                { "basefemale", 36357 },
                { "bluewarrior", 5205 },
                { "goldwarrior", 61720 },
                { "goldwitch", 8861 },
                { "matriarch", 24007 },
                { "purplematriarch", 73307 },
                { "purplewarrior", 8858 },
                { "warrior", 8396 },
                { "warrior2", 17359 },
                { "warrior3", 19603 },
                { "witch", 20656 }
            }},

            // Type: Freebot
            { "freebot", new Dictionary<string, uint>(){
                { "blue", 26449 },
                { "blueneedles", 24382 },
                { "bronze", 75865 },
                { "bronzeclubs", 24381 },
                { "dominion", 17873 },
                { "exile", 24099 },
                { "green", 14271 },
                { "heavyblue", 52112 },
                { "heavygold", 59184 },
                { "heavygrey", 61852 },
                { "heavyred", 61854 },
                { "pellprobe", 49366 },
                { "reddrills", 70341 },
                { "silverclubs", 19778 }
            }},

            // Type: Garr
            { "garr", new Dictionary<string, uint>(){
                { "green", 69520 },
                { "olive", 69522 },
                { "patrol", 69519 },
                { "red", 69518 },
                { "strain", 69523 }
            }},

            // Type: Girrok
            { "girrok", new Dictionary<string, uint>(){
                { "augmented", 19741 },
                { "black", 1753 },
                { "bone", 23775 },
                { "brown", 17646 },
                { "purple", 19969 },
                { "purplestripe", 45505 },
                { "scarred", 26846 },
                { "skeledroid", 26201 },
                { "strain", 38289 },
                { "white", 19742 },
                { "riding", 73222 },
                { "polar", 73225 },
                { "nightmare", 75208 },
                { "loftite", 75568 }
            }},
            
            // Type: Grumpel
            { "grumpel", new Dictionary<string, uint>(){
                { "base", 17381 },
                { "space", 75253 }
            }},

            // Type: Grund
            { "grund", new Dictionary<string, uint>(){
                { "augmented", 48649 },
                { "grim", 63556 },
                { "grim3", 11902 },
                { "grim6", 70196 },
                { "red", 9634 },
                { "red2", 23588 },
                { "red3", 24951 },
                { "red4", 24986 },
                { "red5", 53395 },
                { "red6", 61901 },
                { "white", 18624 },
                { "white2", 68841 },
                { "white3", 8982 },
                { "white5", 63252 },
                { "zombie", 68632 },
                { "zombie2", 68633 }
            }},

            // Type: Heynar
            { "heynar", new Dictionary<string, uint>(){
                { "grey", 48028 },
                { "icewarhound", 73291 },
                { "purple", 18610 },
                { "warhound", 30377 }
            }},

            // Type: High Priest (Storyteller Only)
            { "highpriest", new Dictionary<string, uint>(){
                { "armored", 48554 },
                { "armored2", 70445 },
                { "armoredwhite", 70893 },
                { "base", 42948 },
                { "dark", 75509 },
                { "strain", 55015 }
            }},

            // Type: Ikthian
            { "ikthian", new Dictionary<string, uint>(){
                { "armor1", 21439 },
                { "armor2", 27765 },
                { "armor3", 27769 },
                { "armor4", 28508 },
                { "base", 21436 },
                { "base2", 26034 },
                { "claws", 21373 }
            }},

            // Type: Jabbit
            { "jabbit", new Dictionary<string, uint>(){
                { "blue", 69412 },
                { "brown", 69413 },
                { "grey", 69414 },
                { "strain", 69416 }
            }},

            // Type: Krogg
            { "krogg", new Dictionary<string, uint>(){
                { "base", 19729 },
                { "highwayman", 23804 }
            }},

            // Type: Kurg
            { "kurg", new Dictionary<string, uint>(){
                { "caravantan", 41810 },
                { "caravanwhite", 24091 },
                { "tan", 42293 },
                { "white", 73288 }
            }},

            // Type: Lopp
            { "lopp", new Dictionary<string, uint>(){
                { "femaleblue", 24142 },
                { "flower", 20810 },
                { "femalegreen", 25285 },
                { "femalegreenspace", 29906 },
                { "femalered", 24353 },
                { "femaleredspace", 27915 },
                { "malegreen", 24116 },
                { "malegreenspace", 28348 },
                { "malered", 25283 },
                { "maleredspace", 28346 },
                { "maleyellow", 24118 },
                { "maleyellowspace", 28347 },
                { "marshal", 20809 },
                { "snowfemale", 24361 },
                { "snowmale", 11010 }
            }},

            // Type: Malverine
            { "malverine", new Dictionary<string, uint>(){
                { "augmented", 32213 },
                { "black", 41659 },
                { "golden", 25982 },
                { "purple", 23851 },
                { "strain", 38071 },
                { "white", 31755 }
            }},

            // Type: Moodie
            { "moodie", new Dictionary<string, uint>(){
                { "chieftain", 69595 },
                { "fighter", 69593 },
                { "slasher", 69594 },
                { "witchdoctor", 69592 }
            }},

            // Type: Nerid
            { "nerid", new Dictionary<string, uint>(){
                { "blue", 30602 },
                { "blue2", 26044 }
            }},

            // Type: Oghra
            { "oghra", new Dictionary<string, uint>(){
                { "augmented", 65441 },
                { "captain", 28079 },
                { "coat", 4091 },
                { "duster", 33426 },
                { "grimduster", 27875 },
                { "grimvest", 63557 },
                { "skin", 9633 },
                { "skirt", 4089 },
                { "vest", 15756 },
                { "zombieskin", 69110 },
                { "zombievest", 75681 }
            }},

            // Type: Osun (Storyteller Only)
            { "osun", new Dictionary<string, uint>(){
                { "warlord", 13021 },
                { "icewarlord", 75614 },
                { "icewarlord2", 75459 },
                { "redwarlord", 70444 },
                { "warrior", 13019 },
                { "icewarrior", 75615 },
                { "redwarrior", 73099 },
                { "warrior2", 14342 },
                { "icewarrior2", 71367 },
                { "warrior3", 14343 },
                { "redwarrior3", 48177 },
                { "strainwarrior", 55016 },
                { "witch", 15202 },
                { "witch2", 71767 },
                { "blueghostwitch", 75621 },
                { "redghostwitch", 75617 },
                { "redwitch2", 48295 },
                { "icewitch", 71250 },
                { "icewitch2", 70373 },
                { "strainwitch", 52969 }
            }},

            // Type: Pell
            { "pell", new Dictionary<string, uint>(){
                { "augmented", 8257 },
                { "brown", 14397 },
                { "brown2", 21615 },
                { "brown3", 43195 },
                { "brown4", 41703 },
                { "brownarmored", 70873 },
                { "brownarmored2", 20706 },
                { "brownarmored3", 20708 },
                { "greybase", 30464 },
                { "grey", 26603 },
                { "grey2", 30507 },
                { "grey3", 75458 },
                { "greyarmored", 30450 },
                { "greyarmored2", 30462 },
                { "greyarmored3", 49352 },
                { "greyarmored4", 49346 }
            }},

            // Type: Protostar
            { "protostar", new Dictionary<string, uint>(){
                { "employee", 7301 },
                { "phineas", 26454 },
                { "phineas2", 65788 },
                { "papaphineas", 72565 }
            }},

            // Type: Pumera
            { "pumera", new Dictionary<string, uint>(){
                { "chua", 69430 },
                { "frosted", 69421 },
                { "golden", 69424 },
                { "grey", 69417 },
                { "magenta", 69423 },
                { "maroon", 69420 },
                { "sabertooth", 69427 },
                { "sienna", 69422 },
                { "snowy", 69418 },
                { "snowstripe", 69428 },
                { "steely", 69426 },
                { "strain", 69432 },
                { "tawny", 69419 },
                { "torine", 69429 },
                { "riding", 73213 },
                { "pridelord", 73717 },
                { "redpridelord", 75569 },
                { "hardlight", 75340 },
                { "frostbite", 75648 },
                { "exotic", 75872 },
                { "whitevale", 69425 }
            }},

            // Type: Ravenok
            { "ravenok", new Dictionary<string, uint>(){
                { "purple", 69449 },
                { "steelblue", 69450 },
                { "teal", 69453 },
                { "golden", 69454 },
                { "augmented", 69455 },
                { "snowy", 69456 },
                { "strain", 69457 }
            }},

            // Type: Roan
            { "roan", new Dictionary<string, uint>(){
                { "brownbull", 1741 },
                { "browncow", 2065 },
                { "greybull", 15640 }
            }},

            // Type: Rowsdower
            { "rowsdower", new Dictionary<string, uint>(){
                { "white", 12921 },
                { "demonic", 48437 },
                { "augmented", 69474 },
                { "pink", 69475 },
                { "party", 70316 },
            }},

            // Type: Slank
            { "slank", new Dictionary<string, uint>(){
                { "", 27250 },
            }},

            // Type: Strain (Storyteller Only)
            { "strain", new Dictionary<string, uint>(){
                { "brute", 55050 },
                { "technobrute", 52970 },
                { "corruptor", 30208 },
                { "technocorruptor", 52963 },
                { "crawler", 48146 },
                { "mauler", 55010 },
                { "technomauler", 52964 },
                { "peep", 37962 },
                { "ravager", 30210 },
                { "technoravager", 52968 }
            }},

            // Type: Tank (Storyteller Only)
            { "tank", new Dictionary<string, uint>(){
                { "dominion", 47567 },
                { "exile", 41168 }
            }},

            // Type: Triton (Storyteller Only)
            { "triton", new Dictionary<string, uint>(){
                { "armored", 34094 },
                { "armored2", 48592 },
                { "armored3", 61635 },
                { "base", 34093 },
                { "strain", 55018 },
            }},

            // Type: Vind
            { "vind", new Dictionary<string, uint>(){
                { "", 2410 },
            }},

            // Type: Warbot (Storyteller Only)
            { "warbot", new Dictionary<string, uint>(){
                { "dominion", 20998 },
                { "exile", 21544 },
                { "osun", 32519 },
                { "ikthian", 34644 }
            }},

            // Type: Witch Giant (Storyteller Only)
            { "witchgiant", new Dictionary<string, uint>(){
                { "life", 21804 },
                { "ice", 20857 },
                { "strain", 49391 },
            }},

            // Type: Equivar
            { "equivar", new Dictionary<string, uint>(){
                { "brown", 8725 },
                { "luminous", 61693 },
                { "purple", 71475 },
                { "blue", 71476 },
                { "green", 71477 },
                { "black", 71481 },
                { "floral", 72621 },
                { "augmented", 73269 },
                { "verdant", 74855 },
                { "ice", 75683 },
                { "technophage", 75907 },
            }},

            // Type: Velocirex
            { "velocirex", new Dictionary<string, uint>(){
                { "green", 54261 },
                { "alien", 61390 },
                { "badlands", 71419 },
                { "ascendant", 71464 },
                { "contagion", 75109 },
                { "skeletal", 75204 },
                { "electric", 75205 },
                { "ebon", 75906 },
                { "mecha", 75844 },
                { "ice", 71038 },
            }},

            // Type: Trask
            { "trask", new Dictionary<string, uint>(){
                { "purple", 5212 },
                { "dreg", 61762 },
                { "ice", 71429 },
                { "jungle", 71484 },
                { "toxic", 71485 },
                { "blurple", 71486 },
                { "pinkbelly", 71487 },
                { "strain", 73555 },
                { "darkspur", 75293 },
                { "hardlight", 75366 },
            }},

            // Type: Warpig
            { "warpig", new Dictionary<string, uint>(){
                { "red", 22297 },
                { "redbase", 9622 },
                { "savage", 61428 },
                { "reptile", 71483 },
                { "skeletal", 72355 },
                { "armoredred", 73076 },
                { "greybase", 44457 },
                { "ice", 73305 }
            }},

            // Type: Woolie
            { "woolie", new Dictionary<string, uint>(){
                { "purple", 56827 },
                { "war", 61823 },
                { "green", 71478 },
                { "yellow", 71479 },
                { "blue", 71480 },
                { "bandit", 71482 },
                { "empyrean", 75107 },
                { "dream", 75218 }
            }},

            // Type: Uniblade
            { "uniblade", new Dictionary<string, uint>(){
                { "basic", 48178 },
                { "retro", 61392 },
                { "hotrod", 61393 }
            }},

            // Type: Grinder
            { "grinder", new Dictionary<string, uint>(){
                { "basic", 11085 },
                { "rally", 61833 }
            }},

            // Type: Orbitron
            { "orbitron", new Dictionary<string, uint>(){
                { "basic", 52319 },
                { "marauder", 61391 }
            }},

            // Type: Speeder
            { "speeder", new Dictionary<string, uint>(){
                { "krogg", 75649 },
                { "goldkrogg", 75724 },
                { "blackkrogg", 75725 },
                { "whitekrogg", 75726 },
                { "osun", 71531 },
                { "arctic", 73037 }
            }},

            // Type: Hellhound
            { "hellhound", new Dictionary<string, uint>(){
                { "red", 75743 },
                { "blue", 75744 },
                { "green", 75745 },
                { "gold", 75746 }
            }},

            // Type: Mech
            { "mech", new Dictionary<string, uint>(){
                { "red", 75918 },
                { "blue", 75919 },
                { "gold", 75920 },
                { "yellow", 75921 }
            }},

            // Type: Spiderbot
            { "spiderbot", new Dictionary<string, uint>(){
                { "protostar", 70020 },
                { "goldprotostar", 73342 },
                { "phage", 75590 },
                { "crystal", 75905 },
                { "lava", 75917 }
            }},

            // Type: Skeledroid
            { "skeledroid", new Dictionary<string, uint>(){
                { "", 26193 }
            }},

            // Type: Gorganoth (Storyteller Only)
            { "gorganoth", new Dictionary<string, uint>(){
                { "skeledroid", 48065 },
                { "brown", 1743 },
                { "augmented", 22539 },
                { "dreg", 24244 },
                { "undead", 28077 },
                { "squirg", 62010 },
                { "white", 63122 }
            }},

            // Type: Invisible
            { "invisible", new Dictionary<string, uint>(){
                { "", 5609 },
            }}
        };

        /// <summary>
        /// (GENESIS PRIME) Creature types privileged to the Storyteller Role.
        /// </summary>
        private static readonly List<string> StorytellerOnly = new List<string>() {
            "boss",
            "construct",
            "elemental",
            "highpriest",
            "osun",
            "strain",
            "tank",
            "triton",
            "warbot",
            "witchgiant",
            "gorganoth"
        };

        /// <summary>
        /// (GENESIS PRIME) Returns whether a given creature type is privileged to Storytellers.
        /// </summary>
        public static async Task IsStorytellerOnly(string creatureType, CommandContext context)
        {
            IsSelectedTypeStorytellerOnly = false;
            if (StorytellerOnly.Contains(creatureType))
            {
                await context.SendMessageAsync($"{creatureType} is Storyteller Only.");
                log.Info($"{creatureType} is Storyteller Only!");
                IsSelectedTypeStorytellerOnly = true;
            }
            else
            {
                log.Info($"{creatureType} is not Storyteller Only.");
                IsSelectedTypeStorytellerOnly = false;
            }
        }

        /// <summary>
        /// (GENESIS PRIME) Get the ID of a legal creature type/variant combination for morph/summon commands.
        /// </summary>
        public static async Task GetLegalCreatureIdForSummon(string creatureType, string creatureVariant, CommandContext context)
        {
            SelectedCreatureId = 0;
            Dictionary<string, uint> creatureSubLibrary;
            // get the creature type-specific dictionary
            log.Info($"Looking up {creatureType} in the Creature Library...");
            if (CreatureLibrary.TryGetValue(creatureType, out creatureSubLibrary))
            {
                log.Info($"{creatureType} found as a creature type!");
                // get the creature ID corresponding to the variant
                if (creatureSubLibrary.TryGetValue(creatureVariant, out uint returnCreatureId))
                {
                    log.Info($"Creature type {creatureType} and variant {creatureVariant} resolved to ID: {returnCreatureId}");
                    SelectedCreatureId = returnCreatureId;
                    return;
                }
                else
                {
                    await context.SendErrorAsync("A Creature ID for the given type and variant could not be found!");
                    log.Info($"Creature type {creatureType} and variant {creatureVariant} could not be resolved to a known ID");
                }
            }
            return;
        }
    }
}

