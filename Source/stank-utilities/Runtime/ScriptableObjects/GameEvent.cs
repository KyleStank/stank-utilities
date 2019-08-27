using UnityEngine;

namespace StankUtilities.Runtime.ScriptableObjects
{
    /// <summary>
    /// ScriptableObject that allows us to use an asset in the Unity inspector to interact with the EventSystem.
    /// 
    /// There is a lot of potential for this to be used with any custom game logic.
    /// At the very least, this can be used as an easy way to keep track of event names with a real asset, instead of relying on literal strings in code.
    /// </summary>
    [CreateAssetMenu(fileName = "Game Event", menuName = "Stank Utilities/Game Event", order = 1)]
    public class GameEvent : ScriptableObject
    {
        [SerializeField]
        private string m_EventName = "";

        #region Properties

        /// <summary>
        /// Returns the event's name.
        /// </summary>
        public string EventName
        {
            get { return m_EventName; }

            private set { m_EventName = value; }
        }

        #endregion
    }
}
