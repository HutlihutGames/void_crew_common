using UnityEngine;

namespace VC.Common.Carryables
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(VoidCrewAsset))]
    public class CarryableStatModAsset : CarryableBaseAsset
    {
        public bool IsRelic;
        [TextAreaAttribute(3, 40)] public string StatModsDescription;

        private void Reset()
        {
            StatModsDescription = "{\n  \"modifiers\": []\n}";
        }
    }
}