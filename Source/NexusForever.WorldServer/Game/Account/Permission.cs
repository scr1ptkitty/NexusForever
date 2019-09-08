using System;
using System.Collections.Generic;
using System.Text;

namespace NexusForever.WorldServer.Game.Account.Static
{
    public enum Permission : long
    {
        Everything = -1,
        None = 0,
        ///
        ///
        ///
        CommandAccountCreate = 1,
        CommandAccountDelete = 2,
        //
        CommandBoostAll = 33,
        //
        CommandBroadcast = 3,
        //
        CommandGenericUnlock = 4,
        CommandGenericUnlockAll = 5,
        CommandGenericList = 6,
        //
        CommandGo = 7,
        //
        CommandHelp = 36,
        //
        CommandHouseDecorAdd = 27,
        CommandHouseDecorLookup = 28,
        CommandHouseRemodel = 31,
        CommandHouseTeleport = 26,
        CommandHouseTeleportInside = 32,
        //
        CommandItemAdd = 8,
        CommandItemLookup = 30,
        //
        CommandLocation = 29,
        //
        CommandMorph = 40,
        //
        CommandMovementSplineAdd = 9,
        CommandMovementSplineClear = 10,
        CommandMovementSplineLaunch = 11,
        //
        CommandPathActivate = 12,
        CommandPathAddXp = 16,
        CommandPathUnlock = 13,
        //
        CommandPetUnlockFlair = 17,
        //
        CommandRealmMotd = 41,
        //
        CommandSave = 35,
        //
        CommandSpellAdd = 18,
        CommandSpellCast = 19,
        CommandSpellResetCooldowns = 20,
        //
        CommandSummonClear = 39,
        CommandSummonDisguise = 38,
        CommandSummonEntity = 37,
        //
        CommandTeleportCoords = 21,
        CommandTeleportLoc = 34,
        //
        CommandTitleAdd = 22,
        CommandTitleAll = 24,
        CommandTitleNone = 25,
        CommandTitleRevoke = 23,
        //
        GMFlag = 100,
    }
}