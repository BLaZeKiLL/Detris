using UnityEditor;
using UnityEditor.Build.Reporting;

using UnityEngine;

namespace Editor.Build {

    public static class AndroidBuilder {

        private static readonly string[] scenes = {
            "Assets/Scenes/Main.unity"
        };

        private static readonly string path = "Builds";
        private static readonly string name = "detris";
        
        [MenuItem("Build/Android IL2CPP Development Build")]
        public static void DevelopmentBuild() {
            var pathName = $"{path}/{name}-dev.apk";
            
            FileUtil.DeleteFileOrDirectory(pathName);
            
            var buildPlayOptions = new BuildPlayerOptions {
                scenes = scenes,
                locationPathName = pathName,
                target = BuildTarget.Android,
                targetGroup = BuildTargetGroup.Android,
                options = BuildOptions.Development | BuildOptions.AllowDebugging | BuildOptions.CompressWithLz4HC | BuildOptions.ShowBuiltPlayer
            };
            
            Build(buildPlayOptions);
        }
        
        [MenuItem("Build/Android IL2CPP Production Build")]
        public static void ReleaseBuild() {
            var pathName = $"{path}/{name}-prod.apk";
            
            FileUtil.DeleteFileOrDirectory(pathName);
            
            var buildPlayOptions = new BuildPlayerOptions {
                scenes = scenes,
                locationPathName = pathName,
                target = BuildTarget.Android,
                targetGroup = BuildTargetGroup.Android,
                options = BuildOptions.CompressWithLz4HC | BuildOptions.ShowBuiltPlayer
            };
            
            Build(buildPlayOptions);
        }

        private static void Build(BuildPlayerOptions buildPlayOptions) {
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