using System;

namespace VC.Common.CoreData
{
    [Flags]
    public enum LootRarities
    {
        Common = 1 << 0,
        Rare = 1 << 1,
        Epic = 1 << 2,
        Legendary = 1 << 3,
        Special = 1 << 4
    }
}