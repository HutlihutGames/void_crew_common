using UnityEngine;

namespace VC.Common.Carryables
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(VoidCrewAsset))]
    public class CarryableBaseAsset : MonoBehaviour
    {
        public Collider OverrideCollider;
        public Renderer OverrideRenderer;
        public AudioClip[] OverrideImpactAudio;

        private VoidCrewAsset _voidCrewAsset;

        public VoidCrewAsset VoidCrewAsset
        {
            get
            {
                if (_voidCrewAsset == null)
                {
                    _voidCrewAsset = GetComponent<VoidCrewAsset>();
                }

                return _voidCrewAsset;
            }
        }
    }
}