using UnityEngine;
using UnityEditor;

namespace Editor
{
    [CustomPropertyDrawer(typeof(BunnyEvent))]
    public class BunnyEventPropertyDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var eventNameProperty = property.FindPropertyRelative("eventName");
            return EditorGUI.GetPropertyHeight(eventNameProperty);
        }

         public override void OnGUI(
            Rect position,
            SerializedProperty property,
            GUIContent label
        ) {
            var eventNameProperty = property.FindPropertyRelative("eventName");
            EditorGUI.BeginProperty(position, label, eventNameProperty);
            GUI.enabled = false; // Unity hack to make property appear disabled
            EditorGUI.PropertyField(position, eventNameProperty, label, true);
            GUI.enabled = true;
            EditorGUI.EndProperty();
        }
    }
}