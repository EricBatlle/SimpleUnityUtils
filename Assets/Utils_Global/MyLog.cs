using System.Collections;
using UnityEngine;

public class MyLog : MonoBehaviour
{
    [SerializeField] private bool hideLog = false;
    [SerializeField] private Vector2 scrollPosition = new Vector2();
    [SerializeField] private float viewWidth = 100f;
    [SerializeField] private float viewHeight = 100f;
    [SerializeField] private int fontSize = 8;
    private string myLog;
    private Queue myLogQueue = new Queue();

    private void OnEnable()
    {
        Application.logMessageReceived += HandleLog;
    }

    private void OnDisable()
    {
        Application.logMessageReceived -= HandleLog;
    }

    void HandleLog(string logString, string stackTrace, LogType type)
    {
        myLog = logString;
        string newString = "["+myLogQueue.Count+"][" + type + "] : " + myLog+"\n";
        myLogQueue.Enqueue(newString);
        if (type == LogType.Exception)
        {
            newString =  stackTrace+ "\n";
            myLogQueue.Enqueue(newString);
        }
        myLog = string.Empty;
        foreach (string mylog in myLogQueue)
        {
            myLog += mylog;
        }
    }

    private void OnGUI()
    {
        #region GUI Styles
        //Buttons Style
        GUIStyle guiStyle_button = new GUIStyle(GUI.skin.button);
        guiStyle_button.fontSize = fontSize;
        //Logs Style
        GUIStyle guiStyle_logs = new GUIStyle();
        guiStyle_logs.fontSize = fontSize;
        guiStyle_logs.normal.textColor = Color.white;
        #endregion

        if (!hideLog)
        {
            #region HorizontalLayout
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Clear",guiStyle_button))
            {
                myLog = string.Empty;
                myLogQueue.Clear();
            }
            if (GUILayout.Button("Hide",guiStyle_button))
                hideLog = true;
            GUILayout.EndHorizontal();
            #endregion
            #region ScrollView
            scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Width(viewWidth), GUILayout.Height(viewHeight));


            GUILayout.Label(myLog, guiStyle_logs);
            GUILayout.EndScrollView();
            #endregion
        }
        else
        {
            if (GUILayout.Button("Show Log",guiStyle_button))
                hideLog = false;
        }
    }
}
