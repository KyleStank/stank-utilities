using UnityEditor;

using StankUtilities.Runtime.ScriptableObjects.Variables;

namespace StankUtilities.Editor.ScriptableObjects.Variables
{
    /// <summary>
    /// Custom property drawer for FloatReference.
    /// </summary>
    [CustomPropertyDrawer(typeof(FloatReference))]
    public class FloatReferenceDrawer : ReferenceDrawer { }
}
