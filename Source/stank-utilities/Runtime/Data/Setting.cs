namespace StankUtilities.Runtime.Data
{
    /// <summary>
    /// Class that simply holds information that can be used as "settings" data.
    /// </summary>
    public class Setting
    {
        #region Constructor

        /// <summary>
        /// Initializes the setting.
        /// </summary>
        /// <param name="settingName">Name of the setting.</param>
        /// <param name="settingValue">Value of the setting.</param>
        public Setting(string settingName, object settingValue)
        {
            SettingName = settingName;
            SettingValue = settingValue;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Returns the name of the setting.
        /// </summary>
        public string SettingName { get; set; } = "";

        /// <summary>
        /// Returns the value of the setting.
        /// </summary>
        public object SettingValue { get; set; } = default;

        #endregion
    }
}
