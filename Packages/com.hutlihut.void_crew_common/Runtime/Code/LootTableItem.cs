using System;
using UnityEngine;

namespace VC.Common
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

    public enum LootRarity
    {
        Common = 1 << 0,
        Rare = 1 << 1,
        Epic = 1 << 2,
        Legendary = 1 << 3,
        Special = 1 << 4
    }

    [Serializable]
    public class SectorCompletionReward
    {
        public int Weight = 1;

        // Chapter consists of multiple normal objectives and a single boss encounter across a single solar system, everything post first chapter uses chapter index 1
        public int[] Chapters = new[] { 0 };
        public SectorDifficulty EncounterDifficulty = SectorDifficulty.Normal;
    }

    [Serializable]
    public class EnemyKillReward
    {
        public int Weight = 1;
        public int Quantity = 1;
        public LootRarity Rarity;
    }

    [DisallowMultipleComponent]
    [RequireComponent(typeof(VoidCrewAsset))]
    public class LootTableItem : MonoBehaviour
    {
        public SectorCompletionReward[] SectorCompletionReward;
        public EnemyKillReward[] EnemyKillReward;
    }
}