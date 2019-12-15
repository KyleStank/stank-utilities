#if EDITOR

using UnityEngine;
using UnityEditor;

using StankUtilities.Runtime.Utilities;

namespace StankUtilities.Editor.Utilities
{
    /// <summary>
    /// Useful utility class that makes drawing custom editor GUIs easier.
    /// </summary>
    public static class EditorLayoutUtility
    {
        #region Public Methods

        /// <summary>
        /// Wrapper for vertical layout.
        /// </summary>
        /// <param name="callback">Callback to execute inside layout.</param>
        /// <param name="style">Style to apply to layout.</param>
        /// <param name="options">Options for layout.</param>
        public static void DoVerticalLayout(System.Action callback, GUIStyle style = null, params GUILayoutOption[] options)
        {
            if(callback == null)
            {
                DebuggerUtility.LogError("Cannot draw vertical layout because provided callback is null!");
            }

            if(style == null)
            {
                EditorGUILayout.BeginVertical(options);
            }
            else
            {
                EditorGUILayout.BeginVertical(style, options);
            }

            callback();

            EditorGUILayout.EndVertical();
        }

        /// <summary>
        /// Wrapper for horizontal layout.
        /// </summary>
        /// <param name="callback">Callback to execute inside layout.</param>
        /// <param name="style">Style to apply to layout.</param>
        /// <param name="options">Options for layout.</param>
        public static void DoHorizontalLayout(System.Action callback, GUIStyle style = null, params GUILayoutOption[] options)
        {
            if(callback == null)
            {
                DebuggerUtility.LogError("Cannot draw horizontal layout because provided callback is null!");
            }

            if(style == null)
            {
                EditorGUILayout.BeginHorizontal(options);
            }
            else
            {
                EditorGUILayout.BeginHorizontal(style, options);
            }

            callback();

            EditorGUILayout.EndHorizontal();
        }

        /// <summary>
        /// Wrapper for indent level.
        /// </summary>
        /// <param name="callback">Callback to execute inside indent.</param>
        public static void DoIndent(System.Action callback)
        {
            if(callback == null)
            {
                DebuggerUtility.LogError("Cannot modify indent because provided callback is null!");
            }

            EditorGUI.indentLevel++;

            callback();

            EditorGUI.indentLevel--;
        }

#endregion
    }
}

#endif
