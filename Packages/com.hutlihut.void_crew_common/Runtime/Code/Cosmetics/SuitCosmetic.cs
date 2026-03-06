using UnityEngine;

namespace VC.Common.Cosmetics
{
    [CreateAssetMenu(fileName = "SuitCosmetic", menuName = "Void Crew/Cosmetics/Suit")]
    public class SuitCosmetic : BodyPartCosmetic
    {
        public SkinnedMeshRenderer SuitMesh;
        public SkinnedMeshRenderer ArmMesh;
    }
}
