using System;
using System.Diagnostics;
using UnityEngine;

[ExecuteInEditMode]
public class Launcher : MonoBehaviour
{
    [SerializeField] private string exePath = "ConsoleApp1.exe";
    [SerializeField] private string[] arguments = new string[0];
    [Space()]
    [Tooltip("if true, it is possible to use ReadKey. If false, it is not recommended, cause there is no shell to read the key")]
    [SerializeField] private bool useShellExecute = true;
    [Tooltip("if you want the child process to write output to its own console, set to false")]
    [SerializeField] private bool redirectStandardOutput = false;   //It requires to use shellExecute = false
    [SerializeField] private bool redirectStandardInput = false;    //It requires to use shellExecute = false
    [SerializeField] private bool redirectStandardError = false;    //It requires to use shellExecute = false

    private Process process = null;

    [ContextMenu("Start Process")]
    public void StartProcess()
    {
        try
        {
            //Create new process
            process = new Process();
            process.StartInfo.FileName = Application.dataPath + "/" + exePath;  //Assets/exeName

            //Pass arguments as a single string with spaces on each argument
            string compactedArguments = "";
            foreach (string arg in arguments)
            {
                compactedArguments += arg + " ";
            }
            
            //Set process arguments
            process.StartInfo.Arguments = compactedArguments;
            
            process.StartInfo.UseShellExecute = useShellExecute;
            //Set process StandardRedirections
            process.StartInfo.RedirectStandardOutput = redirectStandardOutput;
            process.StartInfo.RedirectStandardError = redirectStandardError;
            process.StartInfo.RedirectStandardInput = redirectStandardInput;
            
            //Set process Exited handler
            process.EnableRaisingEvents = true;
            process.Exited += OnProcessEnds;
            //Set process output and error handlers
            process.OutputDataReceived += OutputDataReceived;
            process.ErrorDataReceived += ErrorDataReceived;

            //Start and wait the process to end
            process.Start();
            //process.WaitForExit();    //This will block the main thread

            //Async reading
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
        }
        catch (Exception e)
        {            
            UnityEngine.Debug.LogError(e.Message);
        }
    }
    public void StartProcess(string exePath, params string[] arguments)
    {
        this.exePath = exePath;
        this.arguments = new string[arguments.Length];
        this.arguments = arguments;
        this.StartProcess();
    }


    void OutputDataReceived(object sender, DataReceivedEventArgs e)
    {
        UnityEngine.Debug.Log(e.Data);
    }

    //Handle possible Errors recived from the external process
    void ErrorDataReceived(object sender, DataReceivedEventArgs e)
    {
        if(!String.IsNullOrEmpty(e.Data))
            UnityEngine.Debug.LogError(e.Data);
    }

    // Handle Exited event and display process information.
    private void OnProcessEnds(object sender, System.EventArgs e)
    {
        UnityEngine.Debug.Log(
            $"Exit time    : {process.ExitTime}\n" +
            $"Exit code    : {process.ExitCode}\n" +
            $"Elapsed time : {Math.Round((process.ExitTime - process.StartTime).TotalMilliseconds)}");
    }
}