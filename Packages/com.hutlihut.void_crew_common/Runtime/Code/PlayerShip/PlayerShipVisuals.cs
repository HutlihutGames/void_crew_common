using UnityEngine;

namespace VC.Common.PlayerShip
{
    public enum PlayerShipType
    {
        Frigate,
        Striker,
        Destroyer,
        Hub
    }
    
    [DisallowMultipleComponent]
    [RequireComponent(typeof(VoidCrewAsset))]
    public class PlayerShipVisuals : MonoBehaviour
    {
        public PlayerShipType Type; 

        private void OnDrawGizmos()
        {
            //draw ship/check for ship
        }
    }
}
