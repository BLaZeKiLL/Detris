using CodeBlaze.Detris.Util;

using UnityEngine;

namespace CodeBlaze.Detris.Settings {

    public class SettingsProvider : Singleton<SettingsProvider> {
        
        [SerializeField] private GameSettings _settings;

        public GameSettings Settings => _settings;

    }

}