using System;

namespace StankUtilities.Runtime.Events
{
    internal abstract class InvokableActionBase { }

    internal class InvokableAction : InvokableActionBase
    {
        private event Action m_Action = null;

        /// <summary>
        /// Initializes the action to the specificed function.
        /// </summary>
        /// <param name="action">The function to initialize the action to.</param>
        public void Initialize(Action action)
        {
            m_Action = action;
        }

        /// <summary>
        /// Invokes the action.
        /// </summary>
        public void Invoke()
        {
            m_Action();
        }

        /// <summary>
        /// Does the inputted action match the object that the InvokeableAction represents?
        /// </summary>
        /// <param name="action">The action to test against.</param>
        /// <returns>True if the actions match.</returns>
        public bool IsAction(Action action)
        {
            return m_Action == action;
        }
    }

    internal class InvokableAction<T1> : InvokableActionBase
    {
        private event Action<T1> m_Action = null;

        /// <summary>
        /// Initializes the action to the specificed function.
        /// </summary>
        /// <param name="action">The function to initialize the action to.</param>
        public void Initialize(Action<T1> action)
        {
            m_Action = action;
        }

        /// <summary>
        /// Invokes the action.
        /// </summary>
        /// <param name="arg1">The first parameter.</param>
        public void Invoke(T1 arg1)
        {
            m_Action(arg1);
        }

        /// <summary>
        /// Does the inputted action match the object that the InvokeableAction represents?
        /// </summary>
        /// <param name="action">The action to test against.</param>
        /// <returns>True if the actions match.</returns>
        public bool IsAction(Action<T1> action)
        {
            return m_Action == action;
        }
    }
}
