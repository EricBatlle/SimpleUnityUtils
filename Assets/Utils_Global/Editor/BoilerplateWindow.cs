using System;
using System.IO;
using UnityEditor;
using UnityEngine;

public abstract class BoilerplateWindow : EditorWindow
{
    protected static string boilerplateWindowName = "BoilerplateWindow";
    protected static string boilerplateProjectName = "BoilerplateProject";

    [MenuItem("Window/BoilerplateWindow")]
    public static void ShowWindow()
    {
        GetWindow<BoilerplateWindow>(boilerplateWindowName);
    }

    protected virtual void OnGUI()
    {
        GUILayout.Label("Create fast new boilerplate scripts", EditorStyles.boldLabel);

        if (GUILayout.Button("Generate Boilerplate"))
        {
            GenerateBoilerplateElements();
            Debug.Log("<b>Generated " + boilerplateProjectName + " directories and scripts</b>");
        }
    }

    protected abstract void GenerateBoilerplateElements();

    protected void CreateDirectory(string path)
    {
        // Specify the directory you want to manipulate.        
        try
        {
            // Determine whether the directory exists.
            if (Directory.Exists(path))
            {
                Debug.Log("Path already exists.");
                return;
            }
            else
            {
                // Try to create the directory.
                DirectoryInfo di = Directory.CreateDirectory(path);
                Debug.Log("Directory was created successfully");
            }
        }
        catch (Exception e)
        {
            Debug.Log("Directory creation process failed: " + e.ToString());
        }
        finally { }
    }
    protected void CreateFile(string path, string content)
    {
        try
        {
            if (File.Exists(path))
            {
                Debug.Log("File already exists.");
                return;
            }
            else
            {
                //Write the file
                StreamWriter writer = new StreamWriter(path, true);
                writer.Write(content);
                writer.Close();
                Debug.Log("Created new file in " + path);
            }
        }
        catch (Exception e)
        {
            Debug.Log("File creation process failed: " + e.ToString());
        }
    }

}