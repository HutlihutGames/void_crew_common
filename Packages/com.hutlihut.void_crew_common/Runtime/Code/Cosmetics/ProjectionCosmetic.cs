using System.Collections.Generic;
using UnityEngine;

namespace VC.Common.Cosmetics
{
    [CreateAssetMenu(fileName = "ProjectionCosmetic", menuName = "Void Crew/Cosmetics/Projection")]
    public class ProjectionCosmetic : VoidCrewCosmetic
    {
        public Texture2D Texture;
        [SerializeReference, SubclassSelector] public List<ProjectionAnimationModifier> AnimationModifiers = new();
    }

}
