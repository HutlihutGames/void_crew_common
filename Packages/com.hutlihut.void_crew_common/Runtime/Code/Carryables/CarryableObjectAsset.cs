using UnityEngine;

namespace VC.Common.Carryables
{
    public class CarryableObjectAsset : MonoBehaviour
    {
        public Collider OverrideCollider;
        public Renderer OverrideRenderer;
        public AudioClip OverrideImpactAudio;
    }
}