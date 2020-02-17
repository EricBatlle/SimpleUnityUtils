using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using UnityEngine;

[ExecuteInEditMode]
public class Launcher : MonoBehaviour
{
    public string exeName = "ConsoleApp1.exe";
    public string[] arguments = new string[0];
    [Space()]
    [Tooltip("if true, can use ReadKey, you cannot use it if false cause there is no shell to read the key")]
    [SerializeField] private bool useShellExecute = true;
    [Tooltip("if you want the child process to write output to its own console, set to false")]
    [SerializeField] private bool redirectStandardOutput = false;
    [SerializeField] private bool redirectStandardInput = false;
    [SerializeField] private bool redirectStandardError = false;

    private Process process = null;
    private static StringBuilder output = new StringBuilder();

    [ContextMenu("Start Process")]
    public void StartProcess()
    {
        try
        {
            process = new Process();
            process.EnableRaisingEvents = false;
            process.StartInfo.FileName = Application.dataPath + "/" + exeName;  //Assets/exeName

            string compactedArguments = "";
            foreach (string arg in arguments)
            {
                compactedArguments += arg + " ";
            }

            process.StartInfo.Arguments = compactedArguments;
            process.StartInfo.UseShellExecute = useShellExecute;                //if true, can use ReadKey, you cannot use it if false cause there is no shell to read the key
            process.StartInfo.RedirectStandardOutput = redirectStandardOutput;  //if you want the child process to write output to its own console, set to false
            process.StartInfo.RedirectStandardError = redirectStandardError;
            process.StartInfo.RedirectStandardInput = redirectStandardInput;
            process.EnableRaisingEvents = true;

            process.OutputDataReceived += OutputDataReceived;
            process.ErrorDataReceived += ErrorDataReceived;

            process.Start();
            process.WaitForExit();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();

            UnityEngine.Debug.Log("Successfully launched app with output" + output);
        }
        catch (Exception e)
        {
            UnityEngine.Debug.LogError("Unable to launch app: " + e.Message);
        }
    }
    public void StartProcess(params string[] arguments)
    {
        this.arguments = new string[arguments.Length];
        this.arguments = arguments;
        this.StartProcess();
    }

    void OutputDataReceived(object sender, DataReceivedEventArgs e)
    {
        // Process line provided in e.Data
        UnityEngine.Debug.Log(e.ToString());
        UnityEngine.Debug.Log("dataReceived " + e.Data);
        System.Diagnostics.Debug.WriteLine("dataReceived " + e.Data);
    }

    void ErrorDataReceived(object sender, DataReceivedEventArgs e)
    {
        // Process line provided in e.Data
        UnityEngine.Debug.LogError("error: " + e.Data);
    }
}
