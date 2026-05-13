using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;

namespace VC.Common.Publishing
{
    [CreateAssetMenu(fileName = "ModDescriptor", menuName = "Void Crew/Publishing/Mod Descriptor")]
    public class VoidCrewModDescriptor : VoidCrewScriptableObject
    {
        private const string DefaultReadmeTemplatePath = "DefaultReadmeTemplate";
        private const string DefaultModIconPath = "DefaultModIcon";
        private static readonly Regex ModNameRegex = new(@"^[a-zA-Z0-9_]+$");
        
        public string DeveloperName;
        [Tooltip("Semver, e.g. 1.2.3")]
        public string ModVersion = "0.1.0";
        
        [Tooltip("Format: Team-Name-Version")]
        public string[] Dependencies = new []{"BepInEx-BepInExPack-5.4.2100"};
        
        [Tooltip("Optional field for Thunderstore manifest")]
        public string WebsiteURL;
        
        [TextArea(1, 5)] [Tooltip("Max 250 characters")]
        public string DescriptionShort = "A short description for displaying within mod managers. Max 250 characters.";

        public List<ReadmeDescriptor> ReadmeDescriptors = new List<ReadmeDescriptor>()
        {
            new ReadmeDescriptor()
            {
                DescriptorTag = "DescriptionFunctionality",
                Text = "Describe the functionality of your mod."
            },
            new ReadmeDescriptor()
            {
                DescriptorTag = "DescriptionClientUsage",
                Text = "Explain how your mod is used by the client."
            },
            new ReadmeDescriptor()
            {
                DescriptorTag = "InstallInstructions",
                Text = "Installation via a Mod Manager is recommended.\n- Via Thunderstore website: press \"Install with Mod Manager\"\n- Via Mod Manager Client: Search for {{ModName}} and install.\n- Via Manual Download: Use your Mod Manager's import function to install a local mod"
            }
        };
        
        [Tooltip("Optional")]
        public List<ChangelogDescriptor> Changelog = new List<ChangelogDescriptor>()
        {
            new ChangelogDescriptor()
            {
                Version = "0.1.0",
                Changes = "Initial release."
            }
        };

        public TextAsset ReadmeTemplate;
        
        [Serializable]
        public class ReadmeDescriptor
        {
            public string DescriptorTag;
            [TextArea(3, 20)] 
            public string Text;
        }
        
        [Serializable]
        public class ChangelogDescriptor
        {
            public string Version;
            [TextArea(3, 20)] 
            public string Changes;
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            AssignDefaultTemplates();
            AssignDefaultIcon();

            ValidateModName();

            ValidateDescriptionShort();
        }

        private void AssignDefaultTemplates()
        {
            if (ReadmeTemplate == null)
            {
                ReadmeTemplate = Resources.Load<TextAsset>(DefaultReadmeTemplatePath);
            }
        }
        
        private void AssignDefaultIcon()
        {
            if (Icon == null)
            {
                Icon = Resources.Load<Sprite>(DefaultModIconPath);
            }
        }

        private void ValidateModName()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                string assetPath = UnityEditor.AssetDatabase.GetAssetPath(this);

                if (!string.IsNullOrWhiteSpace(assetPath))
                {
                    string folderPath = Path.GetDirectoryName(assetPath);

                    if (!string.IsNullOrWhiteSpace(folderPath))
                    {
                        Name = Path.GetFileName(folderPath);
                    }
                }
            }
            
            if (string.IsNullOrWhiteSpace(Name))
                return;

            Name = Name.Replace(" ", "_");

            Name = Regex.Replace(Name, @"[^a-zA-Z0-9_]", "");

            if (!ModNameRegex.IsMatch(Name))
            {
                Debug.LogWarning(
                    $"[{nameof(VoidCrewModDescriptor)}] " +
                    $"ModName contains invalid characters.",
                    this);
            }
        }

        private void ValidateDescriptionShort()
        {
            if (string.IsNullOrWhiteSpace(DescriptionShort))
            {
                Debug.LogWarning(
                    $"[{nameof(VoidCrewModDescriptor)}] " +
                    $"DescriptionShort should not be empty.",
                    this);

                return;
            }

            const int maxLength = 250;

            if (DescriptionShort.Length > maxLength)
            {
                Debug.LogWarning(
                    $"[{nameof(VoidCrewModDescriptor)}] " +
                    $"DescriptionShort exceeds {maxLength} characters and will be truncated.",
                    this);

                DescriptionShort = DescriptionShort.Substring(0, maxLength);
            }
        }
#endif
    }
}