using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using VC.Common.Cosmetics;

namespace VC.Common.Editor.Cosmetics
{
    [CustomEditor(typeof(ProjectionCosmetic))]
    public class ProjectionCosmeticEditor : UnityEditor.Editor
    {
        private SerializedProperty _animationModifiers;
        private ReorderableList _list;
        private List<Type> _modifierTypes;

        private void OnEnable()
        {
            _animationModifiers = serializedObject.FindProperty("AnimationModifiers");

            if (_animationModifiers == null)
                return;

            _modifierTypes = TypeCache.GetTypesDerivedFrom<ProjectionAnimationModifier>()
                .Where(t => !t.IsAbstract && !t.IsGenericType)
                .OrderBy(t => t.Name)
                .ToList();

            _list = new ReorderableList(serializedObject, _animationModifiers, true, true, true, true);

            _list.drawHeaderCallback = rect => { EditorGUI.LabelField(rect, "Animation Modifiers"); };

            _list.drawElementCallback = (rect, index, _, _) =>
            {
                var element = _animationModifiers.GetArrayElementAtIndex(index);
                rect.y += 2;

                if (string.IsNullOrEmpty(element.managedReferenceFullTypename))
                {
                    EditorGUI.LabelField(
                        new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight),
                        $"Element {index}: Null");
                    return;
                }

                EditorGUI.PropertyField(rect, element, new GUIContent(GetTypeName(element)), true);
            };

            _list.elementHeightCallback = index =>
            {
                var element = _animationModifiers.GetArrayElementAtIndex(index);
                return EditorGUI.GetPropertyHeight(element, true) + 4f;
            };

            _list.onAddDropdownCallback = (rect, list) =>
            {
                var menu = new GenericMenu();

                foreach (var type in _modifierTypes)
                {
                    var captured = type;
                    menu.AddItem(new GUIContent(type.Name), false, () =>
                    {
                        serializedObject.Update();

                        int index = _animationModifiers.arraySize;
                        _animationModifiers.arraySize++;

                        var element = _animationModifiers.GetArrayElementAtIndex(index);
                        element.managedReferenceValue = Activator.CreateInstance(captured);
                        element.isExpanded = true;

                        serializedObject.ApplyModifiedProperties();
                    });
                }

                menu.DropDown(rect);
            };

            _list.onRemoveCallback = list =>
            {
                serializedObject.Update();
                _animationModifiers.DeleteArrayElementAtIndex(list.index);
                serializedObject.ApplyModifiedProperties();
            };
        }

        private static string GetTypeName(SerializedProperty property)
        {
            var full = property.managedReferenceFullTypename;
            if (string.IsNullOrEmpty(full))
                return "Null";

            int split = full.IndexOf(' ');
            return split >= 0 ? full[(split + 1)..].Split('.').Last() : full;
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            DrawPropertiesExcluding(serializedObject, "AnimationModifiers");

            if (_animationModifiers == null)
            {
                EditorGUILayout.HelpBox("AnimationModifiers property not found", MessageType.Error);
            }
            else if (_list == null)
            {
                EditorGUILayout.HelpBox("List was not initialized", MessageType.Error);
            }
            else
            {
                EditorGUILayout.Space();
                _list.DoLayoutList();
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}