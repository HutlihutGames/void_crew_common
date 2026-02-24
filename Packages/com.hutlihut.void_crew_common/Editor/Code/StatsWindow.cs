using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using VC.Common.CoreData;

namespace VC.Common.Editor
{
    public class StatsWindow : EditorWindow
    {
        [MenuItem("Void Crew/Stat table")]
        static void Init()
        {
            StatsWindow window = (StatsWindow) GetWindow(typeof(StatsWindow));
            window.name = "Stat table";
            window.Show();
        }

        private void CreateGUI()
        {
            VisualElement root = rootVisualElement;

            var data = ModStatID.GetAllStatTypes();
            
            root.style.flexGrow = 1;
            
            var header = new VisualElement();
            header.style.flexDirection = FlexDirection.Row;
            header.style.height = 22;
            header.style.unityFontStyleAndWeight = FontStyle.Bold;

            root.Add(header);

            var listView = new ListView();
            listView.itemsSource = data;
            listView.fixedItemHeight = 22;
            listView.selectionType = SelectionType.Single;
            listView.style.flexGrow = 1;
            listView.style.flexShrink = 1;

            listView.makeItem = () =>
            {
                var row = new VisualElement();
                row.style.flexDirection = FlexDirection.Row;

                var key = MakeCell("");
                key.name = "key";

                var value = MakeCell("");
                value.name = "value";

                row.Add(key);
                row.Add(value);

                return row;
            };

            listView.bindItem = (element, i) =>
            {
                element.Q<TextField>("key").value = data[i].Name;
                element.Q<TextField>("value").value = data[i].Id.ToString();
            };

            root.Add(listView);
            
        }
        
        static TextField MakeCell(string text)
        {
            var tf = new TextField { value = text };
            tf.isReadOnly = true;
            tf.style.paddingLeft = 6;
            tf.style.unityTextAlign = TextAnchor.MiddleLeft;
            tf.style.borderBottomWidth = 1;
            tf.style.borderBottomColor = new Color(0, 0, 0, 0.25f);
            tf.style.borderRightWidth = 1;
            tf.style.borderRightColor = new Color(0, 0, 0, 0.25f);
            tf.style.height = 22;
            
            tf.style.flexGrow = 1;
            tf.style.flexShrink = 1;
            tf.style.flexBasis = 0;
            tf.style.minWidth = 0;

            tf.style.backgroundColor = Color.clear;
            tf.style.borderLeftWidth = 0;
            tf.style.borderTopWidth = 0;
            return tf;
        }
    }
}
