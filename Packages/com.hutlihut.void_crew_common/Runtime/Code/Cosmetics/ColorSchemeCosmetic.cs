using UnityEngine;

namespace VC.Common.Cosmetics
{
    [CreateAssetMenu(fileName = "ColorSchemeCosmetic", menuName = "Void Crew/Cosmetics/ColorScheme")]
    public class ColorSchemeCosmetic : VoidCrewCosmetic
    {
        [ColorUsage(false, false)]
        public Color Primary = Color.white;
        [ColorUsage(false, false)]
        public Color Secondary = Color.white;
        [ColorUsage(false, false)]
        public Color Tertiary = Color.white;
    }
}
