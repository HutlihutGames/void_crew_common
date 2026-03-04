using UnityEngine;

namespace VC.Common.Cosmetics
{
    [CreateAssetMenu(fileName = "ArmorPatternCosmetic", menuName = "Void Crew/Cosmetics/ArmorPattern")]
    public class ArmorPatternCosmetic : VoidCrewCosmetic
    {
        public Texture2D PatternTexture;
        public bool OverrideMainColor;
        public Color MainColorOverride = Color.white;
    }
}
