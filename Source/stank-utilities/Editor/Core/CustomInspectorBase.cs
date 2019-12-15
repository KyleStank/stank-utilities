#if EDITOR

namespace StankUtilities.Editor.Core
{
    /// <summary>
    /// Class responsible for setting up the base of a custom inspector.
    /// </summary>
    /// <typeparam name="T">Type to draw inspector for.</typeparam>
    public abstract class CustomInspectorBase<T> : UnityEditor.Editor where T : UnityEngine.MonoBehaviour
    {
        #region Public Methods

        /// <summary>
        /// Draws custom editor GUI.
        /// </summary>
        /// <param name="target">Target of type T.</param>
        public abstract void DrawGUI(T target);

        /// <summary>
        /// Draws the custom GUI.
        /// 
        /// If you need to override this method, just don't inherit from CustomInspectorBase.
        /// </summary>
        public override void OnInspectorGUI()
        {
            // Draw the custom GUI.
            DrawGUI(target as T);

            // If any serialized objects have been modifed, apply those changes!
            if(serializedObject.hasModifiedProperties)
            {
                serializedObject.ApplyModifiedProperties();
            }
        }

        #endregion
    }
}

#endif
