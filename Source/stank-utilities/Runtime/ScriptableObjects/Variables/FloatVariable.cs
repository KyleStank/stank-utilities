using UnityEngine;

namespace StankUtilities.Runtime.ScriptableObjects.Variables
{
    /// <summary>
    /// ScriptableObject that acts as a float variable.
    /// </summary>
    [CreateAssetMenu(fileName = "Float Variable", menuName = "StankUtilities/Variables/Float Variable")]
    public class FloatVariable : Variable<float>, INumericalVariable<float>
    {
        /// <summary>
        /// Increases value.
        /// </summary>
        /// <param name="amount">Value to increase by.</param>
        public void IncrementValue(float amount)
        {
            RuntimeValue += amount;
        }

        /// <summary>
        /// Decreases value.
        /// </summary>
        /// <param name="amount">Value to decrease by.</param>
        public void DecrementValue(float amount)
        {
            RuntimeValue -= amount;
        }
    }
}
