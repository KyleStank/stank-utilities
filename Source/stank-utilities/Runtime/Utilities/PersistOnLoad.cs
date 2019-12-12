using UnityEngine;

namespace StankUtilities.Runtime.Utilities
{
    /// <summary>
    /// MonoBehaviour responsible for making a game object persistent on level loads.
    /// </summary>
    public class PersistOnLoad : MonoBehaviour
    {
        #region Private Methods

        protected void Awake()
        {
            // Mark as DontDestroyOnLoad.
            DontDestroyOnLoad(gameObject);
        }

        #endregion
    }
}