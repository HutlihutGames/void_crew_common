using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using VC.Common.PlayerShip;

namespace VC.Common.Editor.PlayerShip
{
    [InitializeOnLoad]
    public static class ShipPreviewController
    {
        private static Mesh _frigateMesh;
        private static Mesh _strikerMesh;
        private static Mesh _destroyerMesh;
        private static Mesh _hubMesh;

        private static Material _previewMaterial;

        private static bool _subscribed;
        private static bool _dataLoaded;

        private static readonly Dictionary<PlayerShipVisuals, GameObject> _previews = new();
        private static readonly HashSet<PlayerShipVisuals> _activeVisuals = new();
        
        private const string SHIP_PREVIEW_NAME = "__ShipPreview__";

        static ShipPreviewController()
        {
            CleanOldData();
            Selection.selectionChanged -= OnSelectionChanged;
            Selection.selectionChanged += OnSelectionChanged;
            ObjectFactory.componentWasAdded -= OnComponentAdded;
            ObjectFactory.componentWasAdded += OnComponentAdded;
        }

        private static void OnComponentAdded(Component component)
        {
            if (component is PlayerShipVisuals)
                OnSelectionChanged();
        }

        private static void LoadData()
        {
            if (_dataLoaded)
                return;

            _previewMaterial = new Material(Shader.Find("HDRP/Lit"))
            {
                color = new Color(0.8f, 0.8f, 0.8f),
                hideFlags = HideFlags.HideAndDontSave
            };

            _frigateMesh = Resources.Load<Mesh>("Frigate_Reference");
            _strikerMesh = Resources.Load<Mesh>("Striker_Reference");
            _destroyerMesh = Resources.Load<Mesh>("Destroyer_Reference");
            _hubMesh = Resources.Load<Mesh>("Hub_Reference");

            _dataLoaded = true;
        }

        private static void CleanOldData()
        {
            foreach (var obj in Resources.FindObjectsOfTypeAll<MeshFilter>())
            {
                if (obj.gameObject.name == SHIP_PREVIEW_NAME &&
                    obj.gameObject.hideFlags.HasFlag(HideFlags.HideAndDontSave))
                {
                    Object.DestroyImmediate(obj.gameObject);
                }
            }
        }

        private static void OnPlayModeChanged(PlayModeStateChange state)
        {
            if (state == PlayModeStateChange.ExitingEditMode)
            {
                Unsubscribe();
                CleanupAll();
            }
        }

        private static void Subscribe()
        {
            if (_subscribed)
                return;
            EditorApplication.update += Update;
            EditorApplication.playModeStateChanged += OnPlayModeChanged;
            _subscribed = true;
        }

        private static void Unsubscribe()
        {
            if (!_subscribed)
                return;
            EditorApplication.update -= Update;
            EditorApplication.playModeStateChanged -= OnPlayModeChanged;
            _subscribed = false;
        }

        private static void OnSelectionChanged()
        {
            var currentActive = new HashSet<PlayerShipVisuals>();
            foreach (var go in Selection.gameObjects)
            {
                var visuals = go.GetComponentInParent<PlayerShipVisuals>();
                if (visuals)
                    currentActive.Add(visuals);
            }

            var toRemove = new List<PlayerShipVisuals>();
            foreach (var visuals in _activeVisuals)
            {
                if (!visuals || !currentActive.Contains(visuals))
                    toRemove.Add(visuals);
            }

            foreach (var visuals in toRemove)
            {
                if (_previews.TryGetValue(visuals, out var obj) && obj)
                    Object.DestroyImmediate(obj);
                _previews.Remove(visuals);
            }

            _activeVisuals.Clear();
            foreach (var v in currentActive)
                _activeVisuals.Add(v);

            if (_activeVisuals.Count > 0)
                Subscribe();
            else
                Unsubscribe();
        }

        public static void Update()
        {
            if (Application.isPlaying || _activeVisuals.Count == 0)
                return;

            LoadData();

            var toRemove = new List<PlayerShipVisuals>();
            foreach (var kvp in _previews)
            {
                if (!kvp.Key || !kvp.Value)
                {
                    toRemove.Add(kvp.Key);
                    if (kvp.Value)
                        Object.DestroyImmediate(kvp.Value);
                }
            }

            foreach (var key in toRemove)
            {
                _previews.Remove(key);
                _activeVisuals.Remove(key);
            }

            foreach (var visuals in _activeVisuals)
            {
                if (!visuals)
                    continue;

                if (!_previews.TryGetValue(visuals, out var previewObj) || !previewObj)
                {
                    previewObj = CreatePreviewObject(visuals);
                    _previews[visuals] = previewObj;
                }

                previewObj.transform.position = visuals.transform.position;
                previewObj.transform.rotation = visuals.transform.rotation;
                previewObj.transform.localScale = visuals.transform.localScale;

                var mesh = GetMeshForType(visuals.Type);
                var filter = previewObj.GetComponent<MeshFilter>();
                if (filter.sharedMesh != mesh)
                    filter.sharedMesh = mesh;
            }
        }

        private static Mesh GetMeshForType(PlayerShipType type) => type switch
        {
            PlayerShipType.Frigate => _frigateMesh,
            PlayerShipType.Striker => _strikerMesh,
            PlayerShipType.Destroyer => _destroyerMesh,
            PlayerShipType.Hub => _hubMesh,
            _ => null
        };

        private static GameObject CreatePreviewObject(PlayerShipVisuals visuals)
        {
            var obj = new GameObject(SHIP_PREVIEW_NAME)
            {
                hideFlags = HideFlags.HideAndDontSave
            };

            obj.AddComponent<MeshFilter>();
            var renderer = obj.AddComponent<MeshRenderer>();
            renderer.sharedMaterial = _previewMaterial;

            return obj;
        }

        private static void CleanupAll()
        {
            foreach (var kvp in _previews)
            {
                if (kvp.Value)
                    Object.DestroyImmediate(kvp.Value);
            }

            _previews.Clear();
            _activeVisuals.Clear();
        }
    }
}