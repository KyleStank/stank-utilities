using UnityEngine;

namespace StankUtilities.Runtime.Utilities
{
    /// <summary>
    /// Useful class that makes performing math operations easier.
    /// </summary>
    public static class MathUtility
    {
        #region Public Methods

        /// <summary>
        /// Generates a random GUID.
        /// </summary>
        /// <returns>Returns a string that contains the new random GUID.</returns>
        public static string GenerateGUID()
        {
            return System.Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Exponentially grows a number.
        /// </summary>
        /// <param name="initialValue">The initial value to grow.</param>
        /// <param name="rate">The rate to grow by.</param>
        /// <param name="power">The power to multiply everything by.</param>
        /// <returns>Returns a float that has exponentially grown.</returns>
        public static float ExponentiallyGrow(float initialValue, float rate, float power)
        {
            return initialValue * Mathf.Pow(1.0f + rate, power);
        }

        /// <summary>
        /// Exponentially decays a number.
        /// </summary>
        /// <param name="initialValue">The initial value to decay.</param>
        /// <param name="rate">The rate to decay by.</param>
        /// <param name="power">The power to multiply everything by.</param>
        /// <returns>Returns a float that has exponentially decayed.</returns>
        public static float ExponentiallyDecay(float initialValue, float rate, float power)
        {
            return initialValue * Mathf.Pow(Mathf.Clamp(rate, 0.0f, 1.0f - rate), power);
        }

        /// <summary>
        /// Calculates the angle required for a position to rotate towards a target position.
        /// </summary>
        /// <param name="origin">Origin position.</param>
        /// <param name="target">Target position.</param>
        /// <param name="direction">Rotation direction.</param>
        /// <returns>Returns a Vector3 euler angle.</returns>
        public static Vector3 GetLookAtAngle(Vector3 origin, Vector3 target, Vector3 direction)
        {
            // Calculate the direction from the target to the transform.
            Vector3 lookDirection = target - origin;

            // Calculate the angle required to rotate.
            float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90.0f;

            // Return rotation value.
            return direction * angle;
        }

        #endregion
    }
}
