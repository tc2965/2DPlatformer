using UnityEngine;
using UnityEditor;

namespace Editor
{
    [CustomPropertyDrawer(typeof(BunnyEvent))]
    public class BunnyEventPropertyDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var eventNameProperty = property.FindPropertyRelative("EventName");
            return EditorGUI.GetPropertyHeight(eventNameProperty);
        }

         public override void OnGUI(
            Rect position,
            SerializedProperty property,
            GUIContent label
        ) {
            var eventNameProperty = property.FindPropertyRelative("EventName");
            EditorGUI.BeginProperty(position, label, eventNameProperty);
            position.width -= 24;
            EditorGUI.PropertyField(position, eventNameProperty, label, true);
            EditorGUI.EndProperty();
        }
    }
}