using System;
using UnityEngine;
using VC.Common.CoreData;

namespace VC.Common
{
    [Serializable]
    public class SectorCompletionReward
    {
        [Min(1)] public int Weight = 1;
        [Min(1)] public int Amount = 1;

        // Chapter consists of multiple normal objectives and a single boss encounter across a single solar system, everything post first chapter uses chapter index 1
        public int[] Chapters = new[] { 0 };
        public SectorDifficulty EncounterDifficulty = SectorDifficulty.Normal;
    }

    [Serializable]
    public class DropTableEntry
    {
        [Min(1)] public int Weight = 1;
        [Min(1)] public int Amount = 1;
        public LootRarities Rarity = LootRarities.Common;
        public LootSpawnLocationType LocationType = LootSpawnLocationType.Floating;
        public ItemDropCategory DropCategory = ItemDropCategory.OnDeathPilgrimage;

    }

    [DisallowMultipleComponent]
    [RequireComponent(typeof(VoidCrewAsset))]
    public class LootTableItem : MonoBehaviour
    {
        public SectorCompletionReward[] SectorCompletionReward;
        public DropTableEntry[] EnemyKillReward;
    }
}