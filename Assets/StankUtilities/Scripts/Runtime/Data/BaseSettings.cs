﻿using System.Collections.Generic;
using System.IO;

using Newtonsoft.Json;

namespace StankUtilities.Runtime.Data
{
    /// <summary>
    /// Base class that allows for easy creation of settings.
    /// </summary>
    public abstract class BaseSettings
    {
        #region Constructor

        /// <summary>
        /// Initializes the settings object.
        /// </summary>
        /// <param name="filePath">Path at which the settings will be saved and loaded.</param>
        protected BaseSettings(string filePath)
        {
            // Set the file path.
            FilePath = filePath;

            // Try to load settings.
            if(!Load())
            {
                // Add default settings.
                OnInitialSetup();
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Returns the path of the file that the settings are saved and loaded from.
        /// </summary>
        public string FilePath { get; set; } = "";

        /// <summary>
        /// Returns the list of settings values.
        /// </summary>
        public List<Setting> SettingsData { get; set; } = new List<Setting>();

        #endregion

        #region Public Methods

        /// <summary>
        /// Invoked when the settings are initially setup.
        /// </summary>
        public abstract void OnInitialSetup();

        /// <summary>
        /// Invoked whe the settings are saved.
        /// </summary>
        public abstract void OnSave();

        /// <summary>
        /// Invoked when the settings are loaded.
        /// </summary>
        public abstract void OnLoad();

        /// <summary>
        /// Saves all of the settings!
        /// </summary>
        public void Save(Formatting formatting = Formatting.None)
        {
            // If settings list is somehow null, create a new instance of it.
            if(SettingsData == null)
            {
                SettingsData = new List<Setting>();
            }

            // If the settings list count is zero, do not proceed.
            if(SettingsData.Count <= 0)
            {
                return;
            }
            
            // Loop through all settings.
            for(int i = 0; i < SettingsData.Count; i++)
            {
                // Serialize JSON directly to a file.
                using(StreamWriter file = File.CreateText(FilePath))
                {
                    // Create serializer object.
                    JsonSerializer serializer = new JsonSerializer();

                    // Set the formatting.
                    serializer.Formatting = formatting;

                    // Serialize to the save file!
                    serializer.Serialize(file, SettingsData);
                }
            }

            // Invoke abstract method.
            OnSave();
        }

        /// <summary>
        /// Loads all of the settings!
        /// </summary>
        public bool Load()
        {
            // If the settings list is null, do not proceed.
            if(SettingsData == null)
            {
                return false;
            }

            // If the settings file does not exist, do not proceed.
            if(!File.Exists(FilePath))
            {
                return false;
            }

            // Clear the current data that has been loaded into the settings list.
            SettingsData.Clear();

            // Load the settings from the file!
            SettingsData = JsonConvert.DeserializeObject<List<Setting>>(File.ReadAllText(FilePath));

            // Invoke abstract method.
            OnLoad();

            return true;
        }

        /// <summary>
        /// Loads a specific setting from all of the settings data and returns it in a usable format.
        /// </summary>
        /// <typeparam name="T">Object Type to return.</typeparam>
        /// <param name="settingName">Name of the setting to get.</param>
        /// <returns>Returns an object of type T that is loaded from the settings list.</returns>
        public T LoadSetting<T>(string settingName)
        {
            // Loop through all of the settings.
            for(int i = 0; i < SettingsData.Count; i++)
            {
                // If there is a settings match, return it!
                if(SettingsData[i].SettingName.ToLower() == settingName.ToLower())
                {
                    // If the setting value's type matches the type we want, just return it now.
                    if(typeof(T) == SettingsData[i].SettingValue.GetType())
                    {
                        return (T)SettingsData[i].SettingValue;
                    }
                    else // Else, convert the setting value into an object type that we need before returning it.
                    {
                        return JsonConvert.DeserializeObject<T>(SettingsData[i].SettingValue.ToString());
                    }
                }
            }

            // Return default value if no setting was found.
            return default;
        }

        #endregion
    }
}
