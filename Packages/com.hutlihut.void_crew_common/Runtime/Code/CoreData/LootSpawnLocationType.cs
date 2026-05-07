using System;

namespace VC.Common.CoreData
{
    [Flags]
    public enum LootSpawnLocationType
    {
        Floating = 1 << 0,
        Chest = 1 << 1,
        DataTerminal = 1 << 2,
        ArmoryExchange = 1 << 3,
    }
}