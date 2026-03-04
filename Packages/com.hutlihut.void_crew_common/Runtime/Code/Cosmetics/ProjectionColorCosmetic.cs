using UnityEngine;

namespace VC.Common.Cosmetics
{
    [CreateAssetMenu(fileName = "ProjectionColorCosmetic", menuName = "Void Crew/Cosmetics/ProjectionColor")]
    public class ProjectionColorCosmetic : VoidCrewCosmetic
    {
        [ColorUsage(false, true)]
        public Color Color;
    }
}
