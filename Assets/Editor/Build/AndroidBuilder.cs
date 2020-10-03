using UnityEditor;
using UnityEditor.Build.Reporting;

using UnityEngine;

namespace Editor.Build {

    public static class AndroidBuilder {

        private static readonly string[] scenes = {
            "Assets/Scenes/Main.unity"
        };

        private static readonly string path = "Builds";
        private static readonly string name = "Detris";
        
        [MenuItem("Build/Android IL2CPP Development Build")]
        public static void DevelopmentBuild() {
            var buildPlayOptions = new BuildPlayerOptions {
                scenes = scenes,
                locationPathName = $"{path}/{name}.apk",
                target = BuildTarget.Android,
                targetGroup = BuildTargetGroup.Android,
                options = BuildOptions.Development | BuildOptions.AllowDebugging | BuildOptions.CompressWithLz4HC | BuildOptions.ShowBuiltPlayer
            };
            
            Build(buildPlayOptions);
        }

        private static void Build(BuildPlayerOptions buildPlayOptions) {
            FileUtil.DeleteFileOrDirectory(path);
            
            var report = BuildPipeline.BuildPlayer(buildPlayOptions);
            var summary = report.summary;

            switch (summary.result) {
                case BuildResult.Succeeded:
                    Debug.Log("Build succeeded: " + summary.totalSize + " bytes");

                    break;
                case BuildResult.Failed:
                    Debug.Log("Build failed");

                    break;
            }
        }

    }

}