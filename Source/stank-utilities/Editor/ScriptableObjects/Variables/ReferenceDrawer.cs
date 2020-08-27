using UnityEngine;
using UnityEditor;

namespace StankUtilities.Editor.ScriptableObjects.Variables
{
    /// <summary>
    /// Custom property drawer for Reference.
    /// </summary>
    public abstract class ReferenceDrawer : PropertyDrawer
    {
        protected readonly string[] popupOptions = { "Use Constant", "Use Variable" };
        protected GUIStyle popupStyle;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            // Setup the popup style.
            if (popupStyle == null)
            {
                popupStyle = new GUIStyle(GUI.skin.GetStyle("PaneOptions"));
                popupStyle.imagePosition = ImagePosition.ImageOnly;
            }

            // Create label and position.
            label = EditorGUI.BeginProperty(position, label, property);
            position = EditorGUI.PrefixLabel(position, label);

            EditorGUI.BeginChangeCheck();

            // Get properties.
            SerializedProperty useConstant = property.FindPropertyRelative("UseConstant");
            SerializedProperty variable = property.FindPropertyRelative("Variable");
            SerializedProperty constantValue = property.FindPropertyRelative("m_ConstantValue");

            // Calculate rect for configuration button.
            Rect buttonRect = new Rect(position);
            buttonRect.yMin += popupStyle.margin.top;
            buttonRect.width = popupStyle.fixedWidth + popupStyle.margin.right;
            position.xMin = buttonRect.xMax;

            // Store old indent level and set it to 0, the PrefixLabel takes care of it.
            int indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            // Set property based on user's popup selection.
            int result = EditorGUI.Popup(buttonRect, useConstant.boolValue ? 0 : 1, popupOptions, popupStyle);
            useConstant.boolValue = result == 0;

            // Draw property field based on user's popup selection.
            EditorGUI.PropertyField(position,
                useConstant.boolValue ? constantValue : variable,
                GUIContent.none);

            // Apply changes.
            if (EditorGUI.EndChangeCheck())
                property.serializedObject.ApplyModifiedProperties();

            // Reset indent.
            EditorGUI.indentLevel = indent;
            EditorGUI.EndProperty();
        }
    }
}
