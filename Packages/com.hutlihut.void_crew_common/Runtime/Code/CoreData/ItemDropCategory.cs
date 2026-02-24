using System;

namespace VC.Common.CoreData
{
    [Flags]
    public enum ItemDropCategory
    {
        OnDeathPilgrimage = 1 << 0,
        OnDeathSurvivor01 = 1 << 1,
        OnDeathSurvivor23 = 1 << 2,
        OnDeathSurvivor45 = 1 << 3,
        GenericDrop = 1 << 4,
        Wreck_Ambush = 1 << 5,
        Wreck_Generic = 1 << 6,
        Wreck_METEM = 1 << 7,
        Relics = 1 << 8,
        Collector = 1 << 9,
        ContainerSalvage = 1 << 10,
        ContainerSupplies = 1 << 11,
        ContainerTech = 1 << 12,
        AllContainers = ContainerSalvage | ContainerSupplies | ContainerTech,
        AllOnDeath = OnDeathPilgrimage | OnDeathSurvivor01 | OnDeathSurvivor23 | OnDeathSurvivor45,
        AllWrecks = Wreck_Ambush | Wreck_Generic | Wreck_METEM,
    }
}