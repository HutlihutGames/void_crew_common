using UnityEngine;
using UnityEngine.VFX;

namespace VC.Common.Cosmetics
{
    [CreateAssetMenu(fileName = "JetpackTrailCosmetic", menuName = "Void Crew/Cosmetics/JetpackTrail")]
    public class JetpackTrailCosmetic : VoidCrewCosmetic
    {
        public VisualEffect TrailEffect;
    }
}
