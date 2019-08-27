using System;
using System.Collections.Generic;

using StankUtilities.Runtime.ScriptableObjects;
using StankUtilities.Runtime.Utilities;

namespace StankUtilities.Runtime.Events
{
    /// <summary>
    /// Generic event system that allows for global and object-specific events in any Unity project.
    /// </summary>
    public static class EventSystem
    {
        private static Dictionary<object, Dictionary<string, List<InvokableActionBase>>> s_ObjectEventDictionary = new Dictionary<object, Dictionary<string, List<InvokableActionBase>>>();
        private static Dictionary<string, List<InvokableActionBase>> s_GlobalEventDictionary = new Dictionary<string, List<InvokableActionBase>>();

        #region Public Methods

        #region Register Event Methods

        /// <summary>
        /// Registers a global event with no parameters.
        /// </summary>
        /// <param name="eventName">Name of event.</param>
        /// <param name="action">Method to invoke when the event is executed.</param>
        public static void RegisterEvent(string eventName, Action action)
        {
            // Get an InvokableAction object from the object pool.
            InvokableAction invokableAction = ObjectPool.Get<InvokableAction>();

            // Initialize the InvokableObject with our current callback method.
            invokableAction.Initialize(action);

            // Register the event!
            RegisterEvent(eventName, invokableAction);
        }

        /// <summary>
        /// Registers a global event with no parameters.
        /// </summary>
        /// <param name="gameEvent">Game event.</param>
        /// <param name="action">Method to invoke when the event is executed.</param>
        public static void RegisterEvent(GameEvent gameEvent, Action action)
        {
            if(gameEvent == null)
            {
#if EDITOR
                DebuggerUtility.LogError("Can't register event because the GameEvent provided was null!");
#endif
                return;
            }

            // Register the event!
            RegisterEvent(gameEvent.EventName, action);
        }

        /// <summary>
        /// Registers an object event with no parameters.
        /// </summary>
        /// <param name="obj">The target object.</param>
        /// <param name="eventName">Name of event.</param>
        /// <param name="action">Method to invoke when the event is executed.</param>
        public static void RegisterEvent(object obj, string eventName, Action action)
        {
            // Get an InvokableAction object from the object pool.
            InvokableAction invokableAction = ObjectPool.Get<InvokableAction>();

            // Initialize the InvokableObject with our current callback method.
            invokableAction.Initialize(action);

            // Register the event!
            RegisterEvent(obj, eventName, invokableAction);
        }

        /// <summary>
        /// Registers an object event with no parameters.
        /// </summary>
        /// <param name="obj">The target object.</param>
        /// <param name="gameEvent">Game event.</param>
        /// <param name="action">Method to invoke when the event is executed.</param>
        public static void RegisterEvent(object obj, GameEvent gameEvent, Action action)
        {
            if(gameEvent == null)
            {
#if EDITOR
                DebuggerUtility.LogError("Can't register event because the GameEvent provided was null!");
#endif
                return;
            }

            // Register the event!
            RegisterEvent(obj, gameEvent.EventName, action);
        }

        /// <summary>
        /// Registers a global event with one parameter.
        /// </summary>
        /// <typeparam name="T1">The object type of the parameter.</typeparam>
        /// <param name="eventName">Name of event.</param>
        /// <param name="action">Method to invoke when the event is executed.</param>
        public static void RegisterEvent<T1>(string eventName, Action<T1> action)
        {
            // Get an InvokableAction object from the object pool.
            InvokableAction<T1> invokableAction = ObjectPool.Get<InvokableAction<T1>>();

            // Initialize the InvokableObject with our current callback method.
            invokableAction.Initialize(action);

            // Register the event!
            RegisterEvent(eventName, invokableAction);
        }

        /// <summary>
        /// Registers a global event with one parameter.
        /// </summary>
        /// <typeparam name="T1">The object type of the parameter.</typeparam>
        /// <param name="gameEvent">Game event.</param>
        /// <param name="action">Method to invoke when the event is executed.</param>
        public static void RegisterEvent<T1>(GameEvent gameEvent, Action<T1> action)
        {
            if(gameEvent == null)
            {
#if EDITOR
                DebuggerUtility.LogError("Can't register event because the GameEvent provided was null!");
#endif
                return;
            }

            // Register the event!
            RegisterEvent(gameEvent.EventName, action);
        }

        /// <summary>
        /// Registers an object event with one parameter.
        /// </summary>
        /// <typeparam name="T1">The object type of the parameter.</typeparam>
        /// <param name="obj">The target object.</param>
        /// <param name="eventName">Name of event.</param>
        /// <param name="action">Method to invoke when the event is executed.</param>
        public static void RegisterEvent<T1>(object obj, string eventName, Action<T1> action)
        {
            // Get an InvokableAction object from the object pool.
            InvokableAction<T1> invokableAction = ObjectPool.Get<InvokableAction<T1>>();

            // Initialize the InvokableObject with our current callback method.
            invokableAction.Initialize(action);

            // Register the event!
            RegisterEvent(obj, eventName, invokableAction);
        }

        /// <summary>
        /// Registers an object event with one parameter.
        /// </summary>
        /// <typeparam name="T1">The object type of the parameter.</typeparam>
        /// <param name="obj">The target object.</param>
        /// <param name="gameEvent">Game event.</param>
        /// <param name="action">Method to invoke when the event is executed.</param>
        public static void RegisterEvent<T1>(object obj, GameEvent gameEvent, Action<T1> action)
        {
            if(gameEvent == null)
            {
#if EDITOR
                DebuggerUtility.LogError("Can't register event because the GameEvent provided was null!");
#endif
                return;
            }

            // Register the event!
            RegisterEvent(obj, gameEvent.EventName, action);
        }

        #endregion

        #region Execute Event Methods

        /// <summary>
        /// Executes a global event with no parameters.
        /// </summary>
        /// <param name="eventName">Name of event.</param>
        public static void ExecuteEvent(string eventName)
        {
            // Get the list of InvokableActions.
            List<InvokableActionBase> actions = GetActionList(eventName);

            // If the actions are null, do not continue.
            if(actions == null)
            {
                return;
            }

            // Loop through all of the actions.
            for(int i = actions.Count - 1; i >= 0; --i)
            {
                // Invoke the current action.
                (actions[i] as InvokableAction).Invoke();
            }
        }

        /// <summary>
        /// Executes a global event with no parameters.
        /// </summary>
        /// <param name="gameEvent">Game event.</param>
        public static void ExecuteEvent(GameEvent gameEvent)
        {
            if(gameEvent == null)
            {
#if EDITOR
                DebuggerUtility.LogError("Can't execute event because the GameEvent provided was null!");
#endif
                return;
            }

            // Execute the event!
            ExecuteEvent(gameEvent.EventName);
        }

        /// <summary>
        /// Executes an object event with no parameters.
        /// </summary>
        /// <param name="obj">The target object.</param>
        /// <param name="eventName">Name of event.</param>
        public static void ExecuteEvent(object obj, string eventName)
        {
            // Get the list of InvokableActions.
            List<InvokableActionBase> actions = GetActionList(obj, eventName);

            // If the actions are null, do not continue.
            if(actions == null)
            {
                return;
            }

            // Loop through all of the actions.
            for(int i = actions.Count - 1; i >= 0; --i)
            {
                // Invoke the current action.
                (actions[i] as InvokableAction).Invoke();
            }
        }

        /// <summary>
        /// Executes an object event with no parameters.
        /// </summary>
        /// <param name="obj">The target object.</param>
        /// <param name="gameEvent">Game event.</param>
        public static void ExecuteEvent(object obj, GameEvent gameEvent)
        {
            if(gameEvent == null)
            {
#if EDITOR
                DebuggerUtility.LogError("Can't execute event because the GameEvent provided was null!");
#endif
                return;
            }

            // Execute the event!
            ExecuteEvent(obj, gameEvent.EventName);
        }

        /// <summary>
        /// Executes a global event with one parameter.
        /// </summary>
        /// <typeparam name="T1">The object type of the parameter.</typeparam>
        /// <param name="eventName">Name of event.</param>
        /// <param name="arg1">The parameter.</param>
        public static void ExecuteEvent<T1>(string eventName, T1 arg1)
        {
            // Get the list of InvokableActions.
            List<InvokableActionBase> actions = GetActionList(eventName);

            // If the actions are null, do not continue.
            if(actions == null)
            {
                return;
            }

            // Loop through all of the actions.
            for(int i = actions.Count - 1; i >= 0; --i)
            {
                // Invoke the current action.
                (actions[i] as InvokableAction<T1>).Invoke(arg1);
            }
        }

        /// <summary>
        /// Executes a global event with one parameter.
        /// </summary>
        /// <typeparam name="T1">The object type of the parameter.</typeparam>
        /// <param name="gameEvent">Game event.</param>
        /// <param name="arg1">The parameter.</param>
        public static void ExecuteEvent<T1>(GameEvent gameEvent, T1 arg1)
        {
            if(gameEvent == null)
            {
#if EDITOR
                DebuggerUtility.LogError("Can't execute event because the GameEvent provided was null!");
#endif
                return;
            }

            // Execute the event!
            ExecuteEvent(gameEvent.EventName, arg1);
        }

        /// <summary>
        /// Executes an object event with one parameter.
        /// </summary>
        /// <typeparam name="T1">The object type of the parameter.</typeparam>
        /// <param name="obj">The target object.</param>
        /// <param name="eventName">Name of event.</param>
        /// <param name="arg1">The parameter.</param>
        public static void ExecuteEvent<T1>(object obj, string eventName, T1 arg1)
        {
            // Get the list of InvokableActions.
            List<InvokableActionBase> actions = GetActionList(obj, eventName);

            // If the actions are null, do not continue.
            if(actions == null)
            {
                return;
            }

            // Loop through all of the actions.
            for(int i = actions.Count - 1; i >= 0; --i)
            {
                // Invoke the current action.
                (actions[i] as InvokableAction<T1>).Invoke(arg1);
            }
        }

        /// <summary>
        /// Executes an object event with one parameter.
        /// </summary>
        /// <typeparam name="T1">The object type of the parameter.</typeparam>
        /// <param name="obj">The target object.</param>
        /// <param name="gameEvent">Game event.</param>
        /// <param name="arg1">The parameter.</param>
        public static void ExecuteEvent<T1>(object obj, GameEvent gameEvent, T1 arg1)
        {
            if(gameEvent == null)
            {
#if EDITOR
                DebuggerUtility.LogError("Can't execute event because the GameEvent provided was null!");
#endif
                return;
            }

            // Execute the event!
            ExecuteEvent(obj, gameEvent.EventName, arg1);
        }

        #endregion

        #region Unregister Event Methods

        /// <summary>
        /// Unregisters a global event with no parameters.
        /// </summary>
        /// <param name="eventName">Name of event.</param>
        /// <param name="action">Method to remove from event.</param>
        public static void UnregisterEvent(string eventName, Action action)
        {
            // Get the list of InvokableActions.
            List<InvokableActionBase> actions = GetActionList(eventName);

            // If the actions are null, do not continue.
            if(actions == null)
            {
                return;
            }

            // Loop through all of the actions.
            for(int i = 0; i < actions.Count; ++i)
            {
                // Turn the current Action into an InvokableAction.
                InvokableAction invokableAction = (actions[i] as InvokableAction);

                // If the provided Action does not match the Action of the current InvokableAction, continue on to the next Action.
                if(!invokableAction.IsAction(action))
                {
                    continue;
                }

                // Give the InvokableAction object to the object pool for use at a later date.
                ObjectPool.Return(invokableAction);

                // Remove the provided action from the list of actions.
                actions.RemoveAt(i);

                break;
            }

            // Removes the event from the global event dictionary, if possible.
            CheckForEventRemoval(eventName, actions);
        }

        /// <summary>
        /// Unregisters a global event with no parameters.
        /// </summary>
        /// <param name="gameEvent">Game event.</param>
        /// <param name="action">Method to remove from event.</param>
        public static void UnregisterEvent(GameEvent gameEvent, Action action)
        {
            if(gameEvent == null)
            {
#if EDITOR
                DebuggerUtility.LogError("Can't unregister event because the GameEvent provided was null!");
#endif
                return;
            }

            // Unregister the event!
            UnregisterEvent(gameEvent.EventName, action);
        }

        /// <summary>
        /// Unregisters an object event with no parameters.
        /// </summary>
        /// <param name="obj">The target object.</param>
        /// <param name="eventName">Name of event.</param>
        /// <param name="action">Method to remove from event.</param>
        public static void UnregisterEvent(object obj, string eventName, Action action)
        {
            // Get the list of InvokableActions.
            List<InvokableActionBase> actions = GetActionList(obj, eventName);
            
            // If the actions are null, do not continue.
            if(actions == null)
            {
                return;
            }

            // Loop through all of the actions.
            for(int i = 0; i < actions.Count; ++i)
            {
                // Turn the current Action into an InvokableAction.
                InvokableAction invokableAction = (actions[i] as InvokableAction);

                // If the provided Action does not match the Action of the current InvokableAction, continue on to the next Action.
                if(!invokableAction.IsAction(action))
                {
                    continue;
                }

                // Give the InvokableAction object to the object pool for use at a later date.
                ObjectPool.Return(invokableAction);

                // Remove the provided action from the list of actions.
                actions.RemoveAt(i);

                break;
            }

            // Removes the event from the object event dictionary, if possible.
            CheckForEventRemoval(obj, eventName, actions);
        }

        /// <summary>
        /// Unregisters an object event with no parameters.
        /// </summary>
        /// <param name="obj">The target object.</param>
        /// <param name="gameEvent">Game event.</param>
        /// <param name="action">Method to remove from event.</param>
        public static void UnregisterEvent(object obj, GameEvent gameEvent, Action action)
        {
            if(gameEvent == null)
            {
#if EDITOR
                DebuggerUtility.LogError("Can't unregister event because the GameEvent provided was null!");
#endif
                return;
            }

            // Unregister the event!
            UnregisterEvent(obj, gameEvent.EventName, action);
        }

        /// <summary>
        /// Unregisters a global event with one parameter.
        /// </summary>
        /// <typeparam name="T1">The object type of the parameter.</typeparam>
        /// <param name="eventName">Name of event.</param>
        /// <param name="action">Method to remove from event.</param>
        public static void UnregisterEvent<T1>(string eventName, Action<T1> action)
        {
            // Get the list of InvokableActions.
            List<InvokableActionBase> actions = GetActionList(eventName);

            // If the actions are null, do not continue.
            if(actions == null)
            {
                return;
            }

            // Loop through all of the actions.
            for(int i = 0; i < actions.Count; ++i)
            {
                // Turn the current Action into an InvokableAction.
                InvokableAction<T1> invokableAction = (actions[i] as InvokableAction<T1>);

                // If the provided Action does not match the Action of the current InvokableAction, continue on to the next Action.
                if(!invokableAction.IsAction(action))
                {
                    continue;
                }

                // Give the InvokableAction object to the object pool for use at a later date.
                ObjectPool.Return(invokableAction);

                // Remove the provided action from the list of actions.
                actions.RemoveAt(i);

                break;
            }

            // Removes the event from the global event dictionary, if possible.
            CheckForEventRemoval(eventName, actions);
        }

        /// <summary>
        /// Unregisters a global event with one parameter.
        /// </summary>
        /// <typeparam name="T1">The object type of the parameter.</typeparam>
        /// <param name="gameEvent">Game event.</param>
        /// <param name="action">Method to remove from event.</param>
        public static void UnregisterEvent<T1>(GameEvent gameEvent, Action<T1> action)
        {
            if(gameEvent == null)
            {
#if EDITOR
                DebuggerUtility.LogError("Can't unregister event because the GameEvent provided was null!");
#endif
                return;
            }

            // Unregister the event!
            UnregisterEvent(gameEvent.EventName, action);
        }

        /// <summary>
        /// Unregisters an object event with one parameter.
        /// </summary>
        /// <typeparam name="T1">The object type of the parameter.</typeparam>
        /// <param name="obj">The target object.</param>
        /// <param name="eventName">Name of event.</param>
        /// <param name="action">Method to remove from event.</param>
        public static void UnregisterEvent<T1>(object obj, string eventName, Action<T1> action)
        {
            // Get the list of InvokableActions.
            List<InvokableActionBase> actions = GetActionList(obj, eventName);

            // If the actions are null, do not continue.
            if(actions == null)
            {
                return;
            }

            // Loop through all of the actions.
            for(int i = 0; i < actions.Count; ++i)
            {
                // Turn the current Action into an InvokableAction.
                InvokableAction<T1> invokableAction = (actions[i] as InvokableAction<T1>);

                // If the provided Action does not match the Action of the current InvokableAction, continue on to the next Action.
                if(!invokableAction.IsAction(action))
                {
                    continue;
                }

                // Give the InvokableAction object to the object pool for use at a later date.
                ObjectPool.Return(invokableAction);

                // Remove the provided action from the list of actions.
                actions.RemoveAt(i);

                break;
            }

            // Removes the event from the object event dictionary, if possible.
            CheckForEventRemoval(obj, eventName, actions);
        }

        /// <summary>
        /// Unregisters an object event with one parameter.
        /// </summary>
        /// <typeparam name="T1">The object type of the parameter.</typeparam>
        /// <param name="obj">The target object.</param>
        /// <param name="gameEvent">Game event.</param>
        /// <param name="action">Method to remove from event.</param>
        public static void UnregisterEvent<T1>(object obj, GameEvent gameEvent, Action<T1> action)
        {
            if(gameEvent == null)
            {
#if EDITOR
                DebuggerUtility.LogError("Can't unregister event because the GameEvent provided was null!");
#endif
                return;
            }

            // Unregister the event!
            UnregisterEvent(obj, gameEvent.EventName, action);
        }

        #endregion

        #endregion

        #region Private Methods

        #region Event Methods

        /// <summary>
        /// Registers a global event.
        /// </summary>
        /// <param name="eventName">Name of event.</param>
        /// <param name="invokableAction">InvokableAction to invoke when the event is executed.</param>
        private static void RegisterEvent(string eventName, InvokableActionBase invokableAction)
        {
            // If the event already exists, just add the InvokableAction to it.
            if(s_GlobalEventDictionary.TryGetValue(eventName, out List<InvokableActionBase> actionList))
            {
                actionList.Add(invokableAction);
            }
            else // Else, create the event and add the InvokableAction to it.
            {
                // Create the list of actions and add the InvokableAction to the list.
                actionList = new List<InvokableActionBase>
                {
                    invokableAction
                };

                // Add the event and action list to the global event dictionary.
                s_GlobalEventDictionary.Add(eventName, actionList);
            }
        }

        /// <summary>
        /// Registers an object event.
        /// </summary>
        /// <param name="obj">The target object.</param>
        /// <param name="eventName">Name of event.</param>
        /// <param name="invokableAction">InvokableAction to invoke when the event is executed.</param>
        private static void RegisterEvent(object obj, string eventName, InvokableActionBase invokableAction)
        {
            // If the list of actions is null, create it.
            if(!s_ObjectEventDictionary.TryGetValue(obj, out Dictionary<string, List<InvokableActionBase>> handlers))
            {
                // Create the list of actions.
                handlers = new Dictionary<string, List<InvokableActionBase>>();

                // Add the list of actions to the current object's place in the dicionary.
                s_ObjectEventDictionary.Add(obj, handlers);
            }

            // If the event already exists, just add the InvokableAction to it.
            if(handlers.TryGetValue(eventName, out List<InvokableActionBase> actionList))
            {
                actionList.Add(invokableAction);
            }
            else // Else, create the event and add the InvokableAction to it.
            {
                // Create the list of actions and add the InvokableAction to the list.
                actionList = new List<InvokableActionBase>
                {
                    invokableAction
                };

                // Add the event and action list to the object event dictionary.
                handlers.Add(eventName, actionList);
            }
        }

        #endregion

        #region Misc. Methods

        /// <summary>
        /// Returns the list of InvokableActions from a global event.
        /// </summary>
        /// <param name="eventName">Name of event.</param>
        /// <returns>Returns a List of InvokableActionBase from a global event.</returns>
        private static List<InvokableActionBase> GetActionList(string eventName)
        {
            // If the event has any actions on it, return them.
            if(s_GlobalEventDictionary.TryGetValue(eventName, out List<InvokableActionBase> actionList))
            {
                return actionList;
            }

            // Since no actions were found, return null.
            return null;
        }

        /// <summary>
        /// Returns the list of InvokableActions from an object event.
        /// </summary>
        /// <param name="obj">The target object.</param>
        /// <param name="eventName">Name of event.</param>
        /// <returns>Returns a List of InvokableActionBase from an object event.</returns>
        private static List<InvokableActionBase> GetActionList(object obj, string eventName)
        {
            // If the object has any events on it, create a Dictionary for the events.
            if(s_ObjectEventDictionary.TryGetValue(obj, out Dictionary<string, List<InvokableActionBase>> handlers))
            {
                // If the event has any actions on it, return them.
                if(handlers.TryGetValue(eventName, out List<InvokableActionBase> actionList))
                {
                    return actionList;
                }
            }

            // Since the object didn't have any events or the event didn't have any actions, return null.
            return null;
        }

        /// <summary>
        /// Determines if the event can be removed from the global event dictionary.
        /// </summary>
        /// <param name="eventName">Name of event.</param>
        /// <param name="actionList">List of InvokableActions.</param>
        private static void CheckForEventRemoval(string eventName, List<InvokableActionBase> actionList)
        {
            if(actionList.Count == 0)
            {
                s_GlobalEventDictionary.Remove(eventName);
            }
        }

        /// <summary>
        /// Updates the event table to determine if any objects can be removed.
        /// </summary>
        /// <param name="obj">The target object.</param>
        /// <param name="eventName">Name of event.</param>
        /// <param name="actionList">List of InvokableActions.</param>
        private static void CheckForEventRemoval(object obj, string eventName, List<InvokableActionBase> actionList)
        {
            if(actionList.Count == 0)
            {
                if(s_ObjectEventDictionary.TryGetValue(obj, out Dictionary<string, List<InvokableActionBase>> handlers))
                {
                    handlers.Remove(eventName);
                    if(handlers.Count == 0)
                    {
                        s_ObjectEventDictionary.Remove(obj);
                    }
                }
            }
        }

        #endregion

        #endregion
    }
}
