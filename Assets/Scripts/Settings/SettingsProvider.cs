using CodeBlaze.Detris.Util;

using UnityEngine;

namespace CodeBlaze.Detris.Settings {

    [DefaultExecutionOrder(-200)]
    public class SettingsProvider : Singleton<SettingsProvider> {
        
        [SerializeField] private GameSettings _settings;

        public GameSettings Settings => _settings;

    }

}