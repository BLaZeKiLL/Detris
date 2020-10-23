using CodeBlaze.Detris.Settings;

using IngameDebugConsole;

using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBlaze.Detris.Console {

#if DEVELOPMENT_BUILD || UNITY_EDITOR
    public static class Commands {

        [ConsoleMethod("settings", "Prints the current settings")]
        public static void PrintSettings() {
            Application.SetStackTraceLogType(LogType.Log, StackTraceLogType.None);
            Debug.Log($"{JsonUtility.ToJson(SettingsProvider.Current.Settings, true)}");
            Application.SetStackTraceLogType(LogType.Log, StackTraceLogType.ScriptOnly);
        }

        [ConsoleMethod("restart", "Restarts the current level")]
        public static void Restart() {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

    }
#endif
    
}