using System;
using UnityEditor;
using UnityEngine;

public class PluginBoilerplateWindow : BoilerplateWindow
{
    protected override string BoilerplateProjectName { get => pluginName; set => pluginName = value; }

    private string pluginName = "PluginName";
    private bool hasAndroidVersion = false;
    private bool hasiOSVersion = false;
    private bool hasEditorVersion = false;

    private bool hasResultCallbackMethod = false;
    private string javaClassPackageName = "com.example.eric.nativetoolkit.NativeToolkitFragment";

    [MenuItem("Window/PluginBoilerplateWindow")]
    public static void ShowWindow()
    {
        GetWindow<PluginBoilerplateWindow>(boilerplateWindowName);
    }

    protected override void GenerateBoilerplateUI()
    {
        EditorGUILayout.Space();

        GUILayout.Label("Plugin scripts name", EditorStyles.centeredGreyMiniLabel);
        GUILayout.BeginHorizontal();
        GUILayout.Label("PluginName", EditorStyles.boldLabel);
        pluginName = EditorGUILayout.TextField(pluginName);
        GUILayout.EndHorizontal();

        EditorGUILayout.Space();

        GUILayout.BeginVertical();
        GUILayout.Label("Select Platform plugin versions", EditorStyles.centeredGreyMiniLabel);
        hasAndroidVersion = EditorGUILayout.Toggle("Android", hasAndroidVersion);
        hasiOSVersion = EditorGUILayout.Toggle("iOS", hasiOSVersion);
        hasEditorVersion = EditorGUILayout.Toggle("Editor", hasEditorVersion);
        GUILayout.EndVertical();

        if (hasAndroidVersion)
        {
            EditorGUILayout.Space();
            GUILayout.Label("Android plugin config", EditorStyles.centeredGreyMiniLabel);
            hasResultCallbackMethod = EditorGUILayout.Toggle("Add Result Callback?", hasResultCallbackMethod);
            GUILayout.BeginHorizontal();
            GUILayout.Label("JavaClass PackageName", EditorStyles.boldLabel);
            javaClassPackageName = EditorGUILayout.TextField("com.example.eric.nativetoolkit.NativeToolkitFragment");
            GUILayout.EndHorizontal();
        }

        EditorGUILayout.Space();

        if (!hasAndroidVersion && !hasiOSVersion && !hasEditorVersion)
        {
            EditorGUILayout.HelpBox("Select at least one platform for the plugin", MessageType.Warning);
        }
    }

    protected override bool GenerateBoilerplateElements()
    {        
        if(!hasAndroidVersion && !hasiOSVersion && !hasEditorVersion)
        {
            Debug.LogWarning("Can not generate boilerplate code if there is no platform selected");
            return false;
        }
        string pluginDirectoryPath = $"Assets/Plugins/{pluginName}";
        string pluginScriptsDirectoryPath = $"Assets/Scripts/Plugins/{pluginName}";
        string pluginAndroidDirectoryPath = $"Assets/Plugins/{pluginName}/Android";
        string pluginiOSDirectoryPath = $"Assets/Plugins/{pluginName}/iOS";

        //Create main directories
        CreateDirectory(pluginDirectoryPath);
        CreateDirectory(pluginScriptsDirectoryPath);

        //Create main scripts
        CreateFile($"{pluginScriptsDirectoryPath}/{pluginName}.cs", GeneratePluginMonoBehaviourScript());
        CreateFile($"{pluginScriptsDirectoryPath}/{pluginName}Plugin.cs", GeneratePluginScript());

        //Create platform-dependant scripts and directories
        if (hasAndroidVersion)
        {
            CreateDirectory(pluginAndroidDirectoryPath);
            CreateFile($"{pluginScriptsDirectoryPath}/{pluginName}Plugin_Android.cs", GeneratePluginScript_Android());
        }
        if (hasiOSVersion)
        {
            CreateDirectory(pluginiOSDirectoryPath);
            CreateFile($"{pluginScriptsDirectoryPath}/{pluginName}Plugin_iOS.cs", GeneratePluginScript_iOS());
        }
        if (hasEditorVersion)
        {
            CreateFile($"{pluginScriptsDirectoryPath}/{pluginName}Plugin_Editor.cs", GeneratePluginScript_Editor());
        }
        return true;
    }

    #region GeneratePluginScripts
    private string GeneratePluginMonoBehaviourScript()
    {
        string usingReference = (hasResultCallbackMethod) ? @"
using static "+ pluginName + @"Plugin; " : "";
        string pluginInterfaceImplementation = (hasResultCallbackMethod) ? ", I"+pluginName+"Plugin" : "";
        string resultCallbackMethod = (hasResultCallbackMethod) ? @"       
    public void OnResult(string recognizedResult)
    {

    }": "";

        return
@"using UnityEngine;" + usingReference + @"

public class " + pluginName + @": MonoBehaviour" + pluginInterfaceImplementation + @"
{
    private " + pluginName + @"Plugin plugin = null;

    void Start()
    {
        plugin = " + pluginName + @"Plugin.GetPlatformPluginVersion(this.gameObject.name);
    }
" + resultCallbackMethod + @"
}";
    }

    private string GeneratePluginScript()
    {
        string getPlatformPluginVersion = "";
        if (hasEditorVersion)
        {
            getPlatformPluginVersion += @"        if (Application.isEditor)
            return new " + pluginName + @"Plugin_Editor(gameObjectName);
        else";
        }
        else if (hasAndroidVersion)
        {
            getPlatformPluginVersion += @"        if (Application.isEditor)
            return new " + pluginName + @"Plugin_Android(gameObjectName);
        else";
        }
        else if (hasiOSVersion)
        {
            getPlatformPluginVersion += @"        if (Application.isEditor)
            return new " + pluginName + @"Plugin_iOS(gameObjectName);
        else";
        }

        string returnAndroidVersion = (hasAndroidVersion) ? @"            #if UNITY_ANDROID
                return new " + pluginName + @"Plugin_Android(gameObjectName);
            #endif" : "";
        string returniOSVersion = (hasiOSVersion) ? @"            #if UNITY_IOS
                return new " + pluginName + @"Plugin_iOS(gameObjectName);
            #endif" : "";

        string pluginInterface = (hasResultCallbackMethod) ? @"
    public interface I" + pluginName + @"Plugin
    {
        void OnResult(string result);
    }
" : "";

        return
@"using UnityEngine;

public abstract class " + pluginName + @"Plugin
{
    protected string gameObjectName = " + '"' + pluginName + '"' + @";

    protected " + pluginName + @"Plugin(string gameObjectName = null)
    {
        this.gameObjectName = gameObjectName;
        this.SetUp();
    }
    public static " + pluginName + @"Plugin GetPlatformPluginVersion(string gameObjectName = null)
    {
" + getPlatformPluginVersion + @"
        {
" + returnAndroidVersion + @"
" + returniOSVersion + @"
            Debug.LogWarning(" + '"' + "Remember to set project build to mobile device" + '"' + @");
            return null;
        }
    }
" + pluginInterface + @"
    //Features
    protected abstract void SetUp();
}";
    }

    private string GeneratePluginScript_Android()
    {
        return
@"using UnityEngine;

public class " + pluginName + @"Plugin_Android : " + pluginName + @"Plugin
{
    public string javaClassPackageName = " + '"' + javaClassPackageName + '"' + @";
    private AndroidJavaClass javaClass = null;
    AndroidJavaObject instance = null;

    public " + pluginName + @"Plugin_Android(string gameObjectName) : base(gameObjectName) { }

    protected override void SetUp()
    {
        javaClass = new AndroidJavaClass(javaClassPackageName);
        javaClass.CallStatic(" + '"' + "SetUp" + '"' + @", gameObjectName);
        instance = javaClass.GetStatic<AndroidJavaObject>(" + '"' + "instance" + '"' + @");
    }
}";
    }

    private string GeneratePluginScript_iOS()
    {
        return
@"using UnityEngine;

public class " + pluginName + @"Plugin_iOS : " + pluginName + @"Plugin
{
    public " + pluginName + @"Plugin_iOS(string gameObjectName) : base(gameObjectName) { }

    protected override void SetUp()
    {
    }
}";
    }

    private string GeneratePluginScript_Editor()
    {
        return
@"using UnityEngine;

public class " + pluginName + @"Plugin_Editor : " + pluginName + @"Plugin
{
    public " + pluginName + @"Plugin_Editor(string gameObjectName) : base(gameObjectName) { }

    protected override void SetUp()
    {
        Debug.LogWarning(" + '"' + " <b> WARNING </b>: You are running this plugin on Editor mode. Real recognition only works running on mobile device." + '"' +@");
    }
}";
    }
    #endregion
}
