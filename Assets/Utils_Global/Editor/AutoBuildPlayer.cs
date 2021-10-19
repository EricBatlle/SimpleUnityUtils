using System.Diagnostics;
using System.IO;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;

// Output the build size or a failure depending on BuildPlayer.
public class AutoBuildPlayer : MonoBehaviour
{
    /// <summary>
    /// "C:\Program Files\Unity\Hub\Editor\2019.1.14f1\Editor\Unity.exe" -quit -batchmode -projectPath "C:\Users\ERIC\Workspaces\Unity_Workspace\Sandbox" -executeMethod AutoBuildPlayer.AutoBuild
    /// </summary>
    private static string buildPath = Path.Combine(Directory.GetCurrentDirectory(), "Build");

    [MenuItem("Build/AutoBuild")]
    public static void AutoBuild()
    {
        System.Console.WriteLine("AUTO BUILD");
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
            UnityEngine.Debug.Log("Build succeeded: " + summary.totalSize + " bytes");
            CmdCommand(@"/k echo Build succeeded");
        }

        if (summary.result == BuildResult.Failed)
        {
            UnityEngine.Debug.Log("Build failed");
            CmdCommand(@"/k echo Build succeeded");
        }
    }

    private static void CleanBuildDirectory()
    {
        //Remove previous builds        
        DirectoryInfo buildDirectory = Directory.CreateDirectory(buildPath);
        if (buildDirectory.Exists)
        {
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

    public static void CmdCommand(string command)
    {
        var processInfo = new ProcessStartInfo("cmd.exe", command)
        {
            CreateNoWindow = false,
            UseShellExecute = true
        };

        var process = Process.Start(processInfo);

        process.WaitForExit();
    }
}
