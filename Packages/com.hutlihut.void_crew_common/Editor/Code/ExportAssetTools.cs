using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using VC.Common.Publishing;

namespace VC.Common.Editor
{
    public class ExportAssetTools
    {
        private const string OUTPUT_PATH = "Exported Assets";

        [MenuItem("Void Crew/Export Asset Bundles")]
        private static void BuildBundles()
        {
            BuildBundlesToPath(OUTPUT_PATH);
        }
        
        public static void BuildBundlesToPath(string outputPath)
        {
            var selections = Selection.assetGUIDs;

            if (selections.Length != 1)
            {
                Debug.LogError(
                    "Select a single directory containing the void crew assets and all of the dependencies for export");
                return;
            }

            var selection = selections[0];
            var path = AssetDatabase.GUIDToAssetPath(selection);

            bool isFolder = AssetDatabase.IsValidFolder(path);
            if (!isFolder)
            {
                Debug.LogError(
                    "Select a single directory containing the void crew assets and all of the dependencies for export");
                return;
            }


            var name = path.Substring(path.LastIndexOf('/') + 1) + ".metem";

            List<AssetBundleBuild> assetBundleDefinitionList = new();

            var files = RecursiveGetAllAssetsInDirectory(path);

            bool vcAssetsFound = false;

            foreach (var file in files)
            {
                var vcAsset = AssetDatabase.LoadAssetAtPath<VoidCrewAsset>(file);
                if (vcAsset)
                {
                    vcAssetsFound = true;
                    var guid = AssetDatabase.GUIDFromAssetPath(file);
                    vcAsset.SetAssetGUID(guid.ToString());
                    Debug.Log($"{vcAsset} assigned GUID: {guid}");
                    EditorUtility.SetDirty(vcAsset);
                    AssetDatabase.SaveAssetIfDirty(guid);
                    continue;
                }
                var vcso = AssetDatabase.LoadAssetAtPath<VoidCrewScriptableObject>(file);
                if (vcso)
                {
                    vcAssetsFound = true;
                    var guid = AssetDatabase.GUIDFromAssetPath(file);
                    vcso.SetAssetGUID(guid.ToString());
                    Debug.Log($"{vcso} assigned GUID: {guid}");
                    EditorUtility.SetDirty(vcso);
                    AssetDatabase.SaveAssetIfDirty(guid);
                    continue;
                }
            }

            if (!vcAssetsFound)
            {
                Debug.LogWarning("No Void Crew assets found in selected directory, aborting");
                return;
            }

            AssetBundleBuild ab = new();
            ab.assetBundleName = name;
            ab.assetNames = files.ToArray();
            assetBundleDefinitionList.Add(ab);

            if (!Directory.Exists(outputPath))
                Directory.CreateDirectory(outputPath);

            Debug.Log($"Exporting files:\n{string.Join("\n", files)}");

            BuildAssetBundlesParameters buildInput = new()
            {
                outputPath = outputPath,
                options = BuildAssetBundleOptions.AssetBundleStripUnityVersion,
                bundleDefinitions = assetBundleDefinitionList.ToArray()
            };

            AssetBundleManifest manifest = BuildPipeline.BuildAssetBundles(buildInput);
            if (manifest != null)
            {
                var projectRelativePath = "";;
                foreach (var bundleName in manifest.GetAllAssetBundles())
                {
                    projectRelativePath = buildInput.outputPath + "/" + bundleName;
                    Debug.Log($"Exported AssetBundle: {projectRelativePath}");
                }
                if (!string.IsNullOrEmpty(projectRelativePath))
                {
                    EditorUtility.RevealInFinder(projectRelativePath);
                }
            }
            else
            {
                Debug.Log("Build failed, see Console and Editor log for details");
            }
        }

        static List<string> RecursiveGetAllAssetsInDirectory(string path)
        {
            List<string> assets = new();
            foreach (var f in Directory.EnumerateFiles(path, "*", SearchOption.AllDirectories))
                if (Path.GetExtension(f) != ".meta" &&
                    Path.GetExtension(f) != ".cs" && // Scripts are not supported in AssetBundles
                    Path.GetExtension(f) != ".unity") // Scenes cannot be mixed with other file types in a bundle
                    assets.Add(f);
            return assets;
        }
    }
}