using UnityEngine;

namespace StankUtilities.Runtime.Utilities
{
    /// <summary>
    /// Generic singleton class that converts any MonoBehavior into a singleton.
    /// </summary>
    /// <typeparam name="T">Type that we want to convert to a singleton.</typeparam>
    public class Singleton<T> : MonoBehaviour where T : Component
    {
        private static T s_Instance = null;
        private static bool s_IsShuttingDown = false;
        private static object s_Lock = new object();

        #region Properties

        /// <summary>
        /// Returns the instance of the class.
        /// </summary>
        public static T Instance
        {
            get
            {
                // If the application is shutting down, return null.
                if(s_IsShuttingDown)
                {
                    DebuggerUtility.LogWarning("[Singleton] Instance '" + typeof(T) +
                        "' already destroyed. Returning null.");
                    return null;
                }

                lock(s_Lock)
                {
                    // If the instance is null, try to find it.
                    if(s_Instance == null)
                    {
                        // Search everywhere for the type.
                        s_Instance = FindObjectOfType<T>();

                        // If we still didn't find it, create one.
                        if(s_Instance == null)
                        {
                            // Create the game object.
                            GameObject obj = new GameObject
                            {
                                name = typeof(T).Name
                            };

                            // Add this type to the object.
                            s_Instance = obj.AddComponent<T>();
                        }
                    }

                    return s_Instance;
                }
            }

            protected set { s_Instance = value; }
        }

        /// <summary>
        /// Returns true if the instance is spawned. Returns false otherwise.
        /// </summary>
        public static bool IsSpawned
        {
            get { return s_Instance != null; }
        }

        #endregion

        #region Unity Methods

        /// <summary>
        /// Invoked when the GameObject is awakened.
        /// </summary>
        protected virtual void Awake()
        {
            // If the instance is null, create and/or retrieve it.
            if(s_Instance == null)
            {
                // Set the instance.
                s_Instance = this as T;

                // Set the game object to NOT get destroyed on a scene loader.
                DontDestroyOnLoad(s_Instance);
            }
            else // If there is already an instance in the scene, destroy this one.
            {
                Destroy(gameObject);
            }
        }

        /// <summary>
        /// Invoked when the application starts to quit.
        /// </summary>
        protected virtual void OnApplicationQuit()
        {
            s_IsShuttingDown = true;
        }

        /// <summary>
        /// Invoked when the Singleton is destroyed.
        /// </summary>
        protected virtual void OnDestroy()
        {
            s_IsShuttingDown = true;
        }

        #endregion
    }
}