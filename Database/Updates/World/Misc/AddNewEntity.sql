SET @GUID = (SELECT IFNULL(MAX(`id`), 0) FROM `entity`); /*  Selects the last record in the table   */

/*  
ID - set above ^^^
Type - Creature2.tbl CreationTypeEnum
Creature - Creature2.tbl ID
World
Area(zone)
XYZ
Rotation XYZ
DisplayInfo - Take creature2DisplayGroupId in Creature2.tbl and find the creature2DisplayInfoId in Creature2DisplayGroupEntry.tbl
Outfit - Creature2.tbl creature2OutfitGroupId
Faction 1 and 2 - Creature2.tbl factionId

Set stats or NPCs will be dead on world startup
*/
INSERT INTO `entity` (`Id`, `Type`, `Creature`, `World`, `Area`, `X`, `Y`, `Z`, `RX`, `RY`, `RZ`, `DisplayInfo`, `OutfitInfo`, `Faction1`, `Faction2`) VALUES
    (@GUID+1, 0, 40495, 51, 2548, 4074.557, -803.2069, -2348.054, 110, 0, 0, 27961, 0, 219, 219);
    
INSERT INTO `entity_stats` (`Id`, `Stat`, `Value`) VALUES
    (@GUID+1, 0, 1337), /*  health   */
    (@GUID+1, 10, 50), /*  level   */
    (@GUID+1, 15, 0),
    (@GUID+1, 20, 0),
    (@GUID+1, 21, 0),
    (@GUID+1, 22, 0);