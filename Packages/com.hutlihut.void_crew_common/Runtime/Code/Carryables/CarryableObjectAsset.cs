using UnityEngine;

namespace VC.Common.Carryables
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(VoidCrewAsset))]
    public class CarryableObjectAsset : MonoBehaviour
    {
        public Sprite Icon;
        
        public Collider OverrideCollider;
        public Renderer OverrideRenderer;
        public AudioClip OverrideImpactAudio;
    }
}