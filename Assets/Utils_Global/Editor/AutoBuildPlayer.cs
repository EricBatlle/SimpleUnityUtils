using UnityEditor;
using UnityEngine;
using UnityEditor.Build.Reporting;
using System.IO;

// Output the build size or a failure depending on BuildPlayer.
public class AutoBuildPlayer : MonoBehaviour
{
    /// <summary>
    /// "C:\Program Files\Unity\Hub\Editor\2019.3.13f1\Editor\Unity.exe" -quit -batchmode -projectPath "C:\Users\ERIC\Workspaces\Unity_Workspace\Sandbox" -executeMethod AutoBuildPlayer.AutoBuild
    /// </summary>
    private static string buildPath = Path.Combine(Directory.GetCurrentDirectory(), "Build");
    
    [MenuItem("Build/AutoBuild")]
    public static void AutoBuild()
    {
        CleanBuildDirectory();

        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
        buildPlayerOptions.scenes = new[] { "Assets/PRODUCTION.unity" };
        buildPlayerOptions.locationPathName = Path.Combine(buildPath, PlayerSettings.productName + ".exe");
        buildPlayerOptions.target = BuildTarget.StandaloneWindows;
        buildPlayerOptions.options = BuildOptions.None;

        BuildReport report = BuildPipeline.BuildPlayer(buildPlayerOptions);
        BuildSummary summary = report.summary;

        if (summary.result == BuildResult.Succeeded)
        {
            Debug.Log("Build succeeded: " + summary.totalSize + " bytes");
        }

        if (summary.result == BuildResult.Failed)
        {
            Debug.Log("Build failed");
        }
    }

    private static void CleanBuildDirectory()
    {
        //Remove previous builds
        DirectoryInfo buildDirectory = new DirectoryInfo(buildPath);
        foreach (FileInfo file in buildDirectory.EnumerateFiles())
        {
            file.Delete();
        }
        foreach (DirectoryInfo dir in buildDirectory.EnumerateDirectories())
        {
            dir.Delete(true);
        }
    }
}
