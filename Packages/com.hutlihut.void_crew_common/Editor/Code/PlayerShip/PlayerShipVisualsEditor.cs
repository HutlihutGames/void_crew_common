using UnityEditor;
using UnityEngine;
using VC.Common.PlayerShip;

namespace VC.Common.Editor.PlayerShip
{
    [CustomEditor(typeof(PlayerShipVisuals))]
    public class PlayerShipVisualsEditor : UnityEditor.Editor
    {
        private Mesh _frigateMesh;
        private Mesh _strikerMesh;
        private Mesh _destroyerMesh;
        private Mesh _hubMesh;

        private Material _previewMaterial;

        private PlayerShipVisuals _visuals;

        private void OnEnable()
        {
            _previewMaterial = new Material(Shader.Find("HDRP/Lit"))
            {
                color = new Color(0.5f, 0.8f, 1f, 0.3f)
            };

            LoadMeshData();
            
            _visuals = (PlayerShipVisuals)target;
        }


        private void OnDisable()
        {
            if (_previewMaterial)
                DestroyImmediate(_previewMaterial);
        }

        private void LoadMeshData()
        {
            //TODO: proper persistent mesh loading
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Preview Meshes", EditorStyles.boldLabel);

            _frigateMesh = (Mesh)EditorGUILayout.ObjectField("Frigate", _frigateMesh, typeof(Mesh), false);
            _strikerMesh = (Mesh)EditorGUILayout.ObjectField("Striker", _strikerMesh, typeof(Mesh), false);
            _destroyerMesh = (Mesh)EditorGUILayout.ObjectField("Destroyer", _destroyerMesh, typeof(Mesh), false);
            _hubMesh = (Mesh)EditorGUILayout.ObjectField("Hub", _hubMesh, typeof(Mesh), false);
        }

        private void OnSceneGUI()
        {
            Handles.matrix = _visuals.transform.localToWorldMatrix;
            // Handles.DrawMesh(mesh, Vector3.zero, Quaternion.identity, Vector3.one);
        }

        private Mesh GetMeshForType(PlayerShipType type) => type switch
        {
            PlayerShipType.Frigate => _frigateMesh,
            PlayerShipType.Striker => _strikerMesh,
            PlayerShipType.Destroyer => _destroyerMesh,
            PlayerShipType.Hub => _hubMesh,
            _ => null
        };
    }
}