using UnityEditor;
using UnityEngine;
using System.IO;

namespace CriminalDrugLordCity.EditorTools
{
    public static class BuildManager
    {
        [MenuItem("Criminal Drug Lord City/Build Executable")]
        public static void BuildGame()
        {
            string buildPath = "Builds/CriminalDrugLordCity.exe";
            string buildFolder = Path.GetDirectoryName(buildPath);

            if (!Directory.Exists(buildFolder))
            {
                Directory.CreateDirectory(buildFolder);
            }

            BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
            buildPlayerOptions.scenes = new[] { "Assets/Scenes/Main.unity" };
            buildPlayerOptions.locationPathName = buildPath;
            buildPlayerOptions.target = BuildTarget.StandaloneWindows64;
            buildPlayerOptions.options = BuildOptions.None;

            Debug.Log("Building Criminal Drug Lord City to " + buildPath + "...");
            
            BuildPipeline.BuildPlayer(buildPlayerOptions);
            
            Debug.Log("Build Complete! Check the Builds folder.");
        }
    }
}
