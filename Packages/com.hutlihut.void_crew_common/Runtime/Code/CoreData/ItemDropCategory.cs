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
        Wrecks = 1 << 5,
        Relics = 1 << 6,
        Collector = 1 << 7,
        ContainerSalvage = 1 << 8,
        ContainerSupplies = 1 << 9,
        ContainerTech = 1 << 10,
        AllContainers = ContainerSalvage | ContainerSupplies | ContainerTech,
        AllOnDeath = OnDeathPilgrimage | OnDeathSurvivor01 | OnDeathSurvivor23 | OnDeathSurvivor45,
    }
}