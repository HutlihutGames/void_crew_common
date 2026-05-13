using System;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;
using VC.Common.Cosmetics;
using VC.Common.PlayerShip;
using VC.Common.Publishing;

namespace VC.Common.Editor.Publishing
{
    public static class ExportMod
    {
        private const string OUTPUT_ROOT_PATH = "Exported Mods";

        [MenuItem("Void Crew/Export as Mod")]
        public static void ExportSelectedMod()
        {
            if (!TryGetSelectedDescriptor(out VoidCrewModDescriptor descriptor))
                return;

            if (!ValidateDescriptor(descriptor))
                return;

            if (!Directory.Exists(OUTPUT_ROOT_PATH))
                Directory.CreateDirectory(OUTPUT_ROOT_PATH);

            var modFolderName = $"{descriptor.Name}-{descriptor.ModVersion}";
            var modFolderPath = Path.Combine(OUTPUT_ROOT_PATH, modFolderName);

            if (Directory.Exists(modFolderPath))
                Directory.Delete(modFolderPath, true);

            Directory.CreateDirectory(modFolderPath);

            ExportAssetTools.BuildBundlesToPath(modFolderPath);

            WriteReadme(descriptor, modFolderPath);
            WriteManifest(descriptor, modFolderPath);
            WriteIcon(descriptor, modFolderPath);
            WriteChangelog(descriptor, modFolderPath);

            AssetDatabase.Refresh();

            Debug.Log($"Exported mod to: {modFolderPath}", descriptor);
        }

        private static bool TryGetSelectedDescriptor(out VoidCrewModDescriptor descriptor)
        {
            descriptor = null;

            var selected = Selection.activeObject;

            if (selected == null)
            {
                Debug.LogError(
                    "Select a single directory containing a VoidCrewModDescriptor and all of the dependencies for export");
                return false;
            }

            var selectedPath = AssetDatabase.GetAssetPath(selected);

            if (!AssetDatabase.IsValidFolder(selectedPath))
            {
                Debug.LogError(
                    "Select a single directory containing a VoidCrewModDescriptor and all of the dependencies for export");
                return false;
            }

            var descriptorGuids = AssetDatabase.FindAssets(
                $"t:{nameof(VoidCrewModDescriptor)}",
                new[] { selectedPath });

            if (descriptorGuids.Length != 1)
            {
                Debug.LogError(
                    $"Selected directory must contain exactly one VoidCrewModDescriptor. Found {descriptorGuids.Length}.");
                return false;
            }

            var descriptorPath = AssetDatabase.GUIDToAssetPath(descriptorGuids[0]);
            descriptor = AssetDatabase.LoadAssetAtPath<VoidCrewModDescriptor>(descriptorPath);

            return descriptor != null;
        }

        private static bool ValidateDescriptor(VoidCrewModDescriptor descriptor)
        {
            if (string.IsNullOrWhiteSpace(descriptor.Name))
            {
                Debug.LogError("ModName is required.", descriptor);
                return false;
            }

            if (string.IsNullOrWhiteSpace(descriptor.ModVersion))
            {
                Debug.LogError("ModVersion is required.", descriptor);
                return false;
            }

            if (string.IsNullOrWhiteSpace(descriptor.DescriptionShort))
            {
                Debug.LogError("DescriptionShort is required.", descriptor);
                return false;
            }

            if (descriptor.DescriptionShort.Length > 250)
            {
                Debug.LogError("DescriptionShort must be 250 characters or fewer.", descriptor);
                return false;
            }

            if (descriptor.ReadmeTemplate == null)
            {
                Debug.LogError("ReadmeTemplate is required.", descriptor);
                return false;
            }

            if (descriptor.Icon == null)
            {
                Debug.LogError("Icon is required.", descriptor);
                return false;
            }

            return true;
        }

        private static void WriteReadme(VoidCrewModDescriptor descriptor, string modFolderPath)
        {
            var readme = descriptor.ReadmeTemplate.text;

            readme = readme.Replace("{{ModVersion}}", descriptor.ModVersion);
            readme = readme.Replace("{{DeveloperName}}", descriptor.DeveloperName ?? "");
            readme = readme.Replace("{{DependenciesList}}", FormatDependenciesList(descriptor.Dependencies));
            readme = readme.Replace("{{MultiplayerFunctionalityType}}", GetMultiplayerFunctionalityText(descriptor));

            foreach (VoidCrewModDescriptor.ReadmeDescriptor readmeDescriptor in descriptor.ReadmeDescriptors)
            {
                if (string.IsNullOrWhiteSpace(readmeDescriptor.DescriptorTag))
                    continue;

                var tag = "{{" + readmeDescriptor.DescriptorTag + "}}";
                var value = readmeDescriptor.Text ?? "";

                readme = readme.Replace(tag, readmeDescriptor.Text);
            }

            // Replace ModName last because user text may also contain {{ModName}}.
            readme = readme.Replace("{{ModName}}", descriptor.Name);

            File.WriteAllText(Path.Combine(modFolderPath, "README.md"), readme);
        }

        private static void WriteManifest(VoidCrewModDescriptor descriptor, string modFolderPath)
        {
            ThunderstoreManifest manifest = new ThunderstoreManifest
            {
                name = descriptor.Name,
                version_number = descriptor.ModVersion,
                description = descriptor.DescriptionShort,
                website_url = descriptor.WebsiteURL ?? "",
                dependencies = descriptor.Dependencies ?? Array.Empty<string>()
            };

            var json = JsonUtility.ToJson(manifest, true);
            File.WriteAllText(Path.Combine(modFolderPath, "manifest.json"), json);
        }

        private static void WriteIcon(VoidCrewModDescriptor descriptor, string modFolderPath)
        {
            var texture = GetReadableTexture(descriptor.Icon);
            var resized = ResizeTexture(texture, 256, 256);

            byte[] pngBytes = resized.EncodeToPNG();
            File.WriteAllBytes(Path.Combine(modFolderPath, "icon.png"), pngBytes);

            UnityEngine.Object.DestroyImmediate(resized);
            UnityEngine.Object.DestroyImmediate(texture);
        }

        private static void WriteChangelog(VoidCrewModDescriptor descriptor, string modFolderPath)
        {
            var changelog = "";

            for (int i = descriptor.Changelog.Count - 1; i >= 0; i--)
            {
                var entry = descriptor.Changelog[i];

                if (string.IsNullOrWhiteSpace(entry.Version))
                    continue;

                changelog += $"## {entry.Version}\n";

                var changes = entry.Changes ?? "";
                var lines = changes
                    .Split(new[] { "\r\n", "\n" }, StringSplitOptions.None)
                    .Where(line => !string.IsNullOrWhiteSpace(line))
                    .ToArray();

                foreach (string line in lines)
                {
                    var trimmed = line.Trim();
                    changelog += trimmed.StartsWith("-") ? $"{trimmed}\n" : $"- {trimmed}\n";
                }

                changelog += "\n";
            }

            File.WriteAllText(
                Path.Combine(modFolderPath, "CHANGELOG.md"),
                changelog.TrimEnd() + "\n");
        }

        private static string FormatDependenciesList(string[] dependencies)
        {
            if (dependencies == null || dependencies.Length == 0)
                return "None";

            return string.Join(", ", dependencies);
        }

        private const string MultiplayerFunctionalityPlayerCosmetics = 
            "\n - Client: Clients must have mod installed to see cosmetics on each other.";
        private const string MultiplayerFunctionalityShipCosmetics = 
            "\n - Client: Clients must have mod installed to see cosmetics on each other.";
        private const string MultiplayerFunctionalityCarryables = 
            "\n- All\n  - All players must have this mod installed.";

        private static string GetMultiplayerFunctionalityText(VoidCrewModDescriptor descriptor)
        {
            
            var selected = Selection.activeObject;
            var selectedPath = AssetDatabase.GetAssetPath(selected);

            var guids = AssetDatabase.FindAssets(
                "",
                new[] { selectedPath });
            
            bool hasCosmetics =
                AssetDatabase.FindAssets(
                    $"t:{nameof(VoidCrewCosmetic)}",
                    new[] { selectedPath }).Length > 0;
            bool hasShipVisuals =
                AssetDatabase.FindAssets(
                    $"t:{nameof(PlayerShipVisuals)}",
                    new[] { selectedPath }).Length > 0;

            bool hasVoidCrewAssets =
                AssetDatabase.FindAssets(
                    $"t:{nameof(VoidCrewAsset)}",
                    new[] { selectedPath }).Length > 0;

            StringBuilder builder = new();

            if (hasVoidCrewAssets)
            {
                builder.AppendLine("- ✅ Session");
                builder.AppendLine("  - All players must have this mod installed to play together.");
            }
            else
            {
                if (hasCosmetics || hasShipVisuals)
                {
                    builder.AppendLine("- ✅ Local");
                }
                if (hasCosmetics)
                {
                    builder.AppendLine("  - Cosmetics can be equipped by clients with the mod installed. Clients without the mod will see fallback cosmetics.");
                }
                if (hasShipVisuals)
                {
                    builder.AppendLine("  - Ship visuals are only visible to clients with the mod installed.");
                }
            }

            return builder.ToString().TrimEnd();
        }

        private static Texture2D GetReadableTexture(Sprite sprite)
        {
            Texture2D source = sprite.texture;
            Rect rect = sprite.textureRect;

            RenderTexture temporary = RenderTexture.GetTemporary(
                source.width,
                source.height,
                0,
                RenderTextureFormat.Default,
                RenderTextureReadWrite.Linear);

            Graphics.Blit(source, temporary);

            RenderTexture previous = RenderTexture.active;
            RenderTexture.active = temporary;

            Texture2D readable = new Texture2D(
                Mathf.RoundToInt(rect.width),
                Mathf.RoundToInt(rect.height),
                TextureFormat.RGBA32,
                false);

            readable.ReadPixels(rect, 0, 0);
            readable.Apply();

            RenderTexture.active = previous;
            RenderTexture.ReleaseTemporary(temporary);

            return readable;
        }

        private static Texture2D ResizeTexture(Texture2D source, int width, int height)
        {
            RenderTexture temporary = RenderTexture.GetTemporary(width, height);
            Graphics.Blit(source, temporary);

            RenderTexture previous = RenderTexture.active;
            RenderTexture.active = temporary;

            Texture2D resized = new Texture2D(width, height, TextureFormat.RGBA32, false);
            resized.ReadPixels(new Rect(0, 0, width, height), 0, 0);
            resized.Apply();

            RenderTexture.active = previous;
            RenderTexture.ReleaseTemporary(temporary);

            return resized;
        }
    }
}