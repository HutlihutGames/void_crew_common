using System;

namespace VC.Common.CoreData
{
    [Flags]
    public enum LootSpawnLocationType
    {
        Floating = 1 << 0,
        Chest = 1 << 1,
        LoreTerminal = 1 << 2
    }
}