using UnityEngine;

namespace VC.Common
{
    public class VoidCrewScriptableObject : ScriptableObject
    {
        [SerializeField] [HideInInspector] private string assetGuid;
        public string AssetGuid => assetGuid;

        public string Name;
        public string Description;
        public Sprite Icon;

#if UNITY_EDITOR
        public void SetAssetGUID(string guid)
        {
            assetGuid = guid;
        }
#endif
    }
}