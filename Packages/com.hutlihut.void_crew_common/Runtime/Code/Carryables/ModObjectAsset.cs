using System;
using UnityEngine;

namespace VC.Common.Carryables
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(VoidCrewAsset))]
    public class ModObjectAsset : CarryableObjectAsset
    {
        public bool IsRelic;
        public string StatMods;

        private void Reset()
        {
            StatMods = "{" +
                       "" +
                       "}";
        }
    }
}