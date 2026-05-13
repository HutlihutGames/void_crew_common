using UnityEditor;
using UnityEngine;
using VC.Common.Publishing;

namespace VC.Common.Editor.Publishing
{
    [CustomEditor(typeof(VoidCrewModDescriptor))]
    public class VoidCrewModDescriptorEditor : UnityEditor.Editor
    {
        private SerializedProperty changelogProperty;

        private void OnEnable()
        {
            changelogProperty = serializedObject.FindProperty("Changelog");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            DrawPropertiesExcluding(serializedObject, "Changelog", "Description");

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Changelog", EditorStyles.boldLabel);

            DrawReversedChangelog();

            serializedObject.ApplyModifiedProperties();
        }

        private void DrawReversedChangelog()
        {
            changelogProperty.isExpanded = EditorGUILayout.Foldout(
                changelogProperty.isExpanded,
                $"Changelog Entries ({changelogProperty.arraySize})",
                true);

            if (!changelogProperty.isExpanded)
                return;
            
            if (GUILayout.Button("Add Changelog Entry"))
            {
                changelogProperty.InsertArrayElementAtIndex(changelogProperty.arraySize);
            }

            EditorGUI.indentLevel++;

            for (int i = changelogProperty.arraySize - 1; i >= 0; i--)
            {
                SerializedProperty entry = changelogProperty.GetArrayElementAtIndex(i);
                SerializedProperty version = entry.FindPropertyRelative("Version");

                string title = string.IsNullOrWhiteSpace(version.stringValue)
                    ? "New Version"
                    : version.stringValue;

                EditorGUILayout.BeginHorizontal();

                entry.isExpanded = EditorGUILayout.Foldout(
                    entry.isExpanded,
                    title,
                    true);

                if (GUILayout.Button("−", GUILayout.Width(24)))
                {
                    changelogProperty.DeleteArrayElementAtIndex(i);
                    break;
                }

                EditorGUILayout.EndHorizontal();

                if (entry.isExpanded)
                {
                    EditorGUI.indentLevel++;

                    EditorGUILayout.PropertyField(entry.FindPropertyRelative("Version"));
                    EditorGUILayout.PropertyField(entry.FindPropertyRelative("Changes"));

                    EditorGUI.indentLevel--;
                }
            }

            EditorGUILayout.Space(4);

            EditorGUI.indentLevel--;
        }
    }    
}