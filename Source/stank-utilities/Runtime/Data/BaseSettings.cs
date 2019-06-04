using System.Collections.Generic;
using System.IO;

using StankUtilities.Runtime.Utilities;

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
            // If the file path is empty, do not proceed.
            if(string.IsNullOrEmpty(filePath))
            {
                return;
            }

            // Set the file path.
            FilePath = filePath;
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
        public void Save()
        {
            // If settings list is somehow null, create a new instance.
            if(SettingsData == null)
            {
                SettingsData = new List<Setting>();
            }

            // If the settings list count is zero, do not proceed.
            if(SettingsData.Count <= 0)
            {
                return;
            }

            // Serialize JSON directly to a file.
            File.WriteAllText(FilePath, JSONUtility.SerializeObject(SettingsData));

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
            SettingsData = JSONUtility.DeserializeObject<List<Setting>>(File.ReadAllText(FilePath));

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
                        return JSONUtility.DeserializeObject<T>(SettingsData[i].SettingValue.ToString());
                    }
                }
            }

            // Return default value if no setting was found.
            return default;
        }

        /// <summary>
        /// Sets a specific settings to a value.
        /// </summary>
        /// <typeparam name="T">Object Type to set setting value.</typeparam>
        /// <param name="settingName">Setting to set.</param>
        /// <param name="settingValue">Value to set.</param>
        public void SetSetting<T>(string settingName, object settingValue)
        {
            // Loop through all of the settings.
            for(int i = 0; i < SettingsData.Count; i++)
            {
                // If there is a settings match, update the setting's value!
                if(SettingsData[i].SettingName.ToLower() == settingName.ToLower())
                {
                    SettingsData[i].SettingValue = (T)settingValue;
                }
            }
        }

        #endregion
    }
}
