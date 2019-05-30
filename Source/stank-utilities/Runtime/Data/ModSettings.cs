using System.Collections.Generic;

namespace StankUtilities.Runtime.Data
{
    /// <summary>
    /// Class that handles all settings about mods.
    /// </summary>
    public class TestSettings : BaseSettings
    {
        public class Mod
        {
            public Mod(string name, int id)
            {
                ModName = name;
                ModID = id;
            }

            public string ModName = "";
            public int ModID = 0;
        }

        private const string k_ModsListKey = "Mods";

        #region Constructor

        /// <summary>
        /// Initializes the settings object.
        /// </summary>
        /// <param name="filePath">Path at which the settings will be saved and loaded.</param>
        public TestSettings(string filePath) : base(filePath) { }

        #endregion

        #region Properties

        /// <summary>
        /// Returns a list of all of the mods.
        /// </summary>
        public List<Mod> Mods { get; set; } = new List<Mod>();

        public float AudioLevel { get; set; } = 0.0f;

        public bool CanWalk { get; set; } = false;

        public int MoveSpeed { get; set; } = 0;

        #endregion

        #region Public Methods

        /// <summary>
        /// Invoked whe the settings are saved.
        /// </summary>
        public override void OnSave() { }

        /// <summary>
        /// Invoked when the settings are loaded.
        /// </summary>
        public override void OnLoad()
        {
            // Load the mods from the settings.
            Mods = LoadSetting<List<Mod>>(k_ModsListKey);
            AudioLevel = LoadSetting<float>("AudioLevel");
            CanWalk = LoadSetting<bool>("CanWalk");
            MoveSpeed = LoadSetting<int>("MoveSpeed");
        }

        /// <summary>
        /// Sets up the base mod settings.
        /// </summary>
        public override void OnInitialSetup()
        {
            // Create initial list of mods.
            Mods = new List<Mod>();
            Mods.Add(new Mod("More Grass", 1));
            Mods.Add(new Mod("Better Combat", 2));
            Mods.Add(new Mod("Hot Females", 3));

            // Create inital audio level.
            AudioLevel = 100.0f;

            // Create inital walk state.
            CanWalk = true;

            // Create inital walk speed.
            MoveSpeed = 100;

            SettingsData.Add(new Setting("AudioLevel", AudioLevel));
            SettingsData.Add(new Setting("CanWalk", CanWalk));
            SettingsData.Add(new Setting("MoveSpeed", MoveSpeed));
            SettingsData.Add(new Setting(k_ModsListKey, Mods));

            Save();
        }

        #endregion
    }
}
