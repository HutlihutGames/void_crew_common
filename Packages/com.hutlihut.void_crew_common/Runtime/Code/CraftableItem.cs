using System;
using UnityEngine;

namespace VC.Common
{
    [Flags]
    public enum GameMode : byte
    {
        Survivor = 1 << 0,
        Endless = 1 << 1,
        All = Survivor | Endless
    }
    
    [Serializable]
    public class CraftableItemConfig
    {
        public int Cost = 5;
        public int RecycleValue = 5;
        
        [Range(0, 2)]
        public int RequiredFabricatorLevel;
        public GameMode AvailableInGameModes = GameMode.All;
        public bool CanBeRecycled = true;
    }
    
    [DisallowMultipleComponent]
    [RequireComponent(typeof(VoidCrewAsset))]
    public class CraftableItem : MonoBehaviour
    {
        public CraftableItemConfig Config;
    }
}
