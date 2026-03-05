using UnityEngine;

namespace VC.Common.Cosmetics
{
    public abstract class BodyPartCosmetic : VoidCrewCosmetic
    {
        public Texture ColorMap;
        public Texture NormalMap;
        public Texture ZonesMap;
        public Texture MaskMap;
        public Texture EmissionMap;
        [ColorUsage(false, true)]
        public Color EmissionColor;
        public float MainPatternTiling = 4f;
    }
}
