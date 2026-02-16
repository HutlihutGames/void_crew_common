using System;

namespace VC.Common.CoreData
{
    [Flags]
    public enum SectorDifficulty
    {
        Normal = 1 << 0,
        Hard = 1 << 1,
        Insane = 1 << 2,
        Boss = 1 << 3,
        All = Normal | Hard | Insane | Boss,
    }
}