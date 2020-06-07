using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyLog : MonoBehaviour
{
    #region CustomLogs
    public class LogStyle
    {
        public float viewWidth = 100f;
        public float viewHeight = 100f;
        public int fontSize = 8;

        public LogStyle(float viewWidth = 100f, float viewHeight = 100f, int fontsize = 8)
        {
            this.viewWidth = viewWidth;
            this.viewHeight = viewHeight;
            this.fontSize = fontsize;
        }
    }
    public enum PredefinedDevice
    {
        None, Pocophone
    }
    public Dictionary<PredefinedDevice, LogStyle> LogtypeToLogstyleDictionary = new Dictionary<PredefinedDevice, LogStyle>()
    {
        { PredefinedDevice.None, new LogStyle() },
        { PredefinedDevice.Pocophone, new LogStyle(800,800,70) }
    };
    #endregion

    [SerializeField] private bool hideLog = false;
    [SerializeField] private PredefinedDevice predefinedDevice = PredefinedDevice.None;
    [SerializeField] private Vector2 scrollPosition = new Vector2();
    [SerializeField] private float viewWidth = 100f;
    [SerializeField] private float viewHeight = 100f;
    [SerializeField] private int fontSize = 8;
    private string myLog;
    private Queue myLogQueue = new Queue();

    private void OnEnable()
    {
        Application.logMessageReceivedThreaded += HandleLog;
    }

    private void OnDisable()
    {
        Application.logMessageReceivedThreaded -= HandleLog;
    }

    void HandleLog(string logString, string stackTrace, LogType type)
    {
        string hourString = ((DateTime.Now.Hour <= 9) ? "0" : "") + DateTime.Now.Hour;
        string minuteString = ((DateTime.Now.Minute <= 9) ? "0" : "") + DateTime.Now.Minute;
        string secString = ((DateTime.Now.Second <= 9) ? "0" : "") + DateTime.Now.Second;
        string time = hourString + ":" + minuteString + ":" + secString;

        Color textColor = Color.white;
        switch (type)
        {
            case LogType.Error:
                textColor = new Color(255f / 255f, 112f / 255f, 112f / 255f);
                break;
            case LogType.Assert:
                textColor = new Color(255f / 255f, 112f / 255f, 112f / 255f);
                break;
            case LogType.Warning:
                textColor = new Color(252f / 255f, 174f / 255f, 78f / 255f);
                break;
            case LogType.Log:
                textColor = Color.white;
                break;
            case LogType.Exception:
                textColor = new Color(255f / 255f, 112f / 255f, 112f / 255f);
                break;
            default:
                textColor = Color.white;
                break;
        }

        myLog = logString;
        string newString = "[" + myLogQueue.Count + "][" + time + "]: " + myLog + "\n";
        newString = "<color=#" + ColorUtility.ToHtmlStringRGBA(textColor) + ">" + newString + "</color>";
        myLogQueue.Enqueue(newString);
        if (type == LogType.Exception)
        {
            newString = stackTrace + "\n";
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
        //Set predefined styles
        LogStyle deviceStyle = LogtypeToLogstyleDictionary[predefinedDevice];
        fontSize = deviceStyle.fontSize;
        viewHeight = deviceStyle.viewHeight;
        viewWidth = deviceStyle.viewHeight;

        #region GUI Styles
        //Buttons Style
        GUIStyle guiStyle_button = new GUIStyle(GUI.skin.button);
        guiStyle_button.fontSize = fontSize;
        //Logs Style
        GUIStyle guiStyle_logs = new GUIStyle(UnityEngine.GUI.skin.label)
        {
            richText = true,
            alignment = TextAnchor.MiddleLeft,
            wordWrap = false,
            fontSize = fontSize,
            stretchWidth = true,
            //font = (Font)EditorGUIUtility.LoadRequired("Fonts/Lucida Grande.ttf"),
            //fontStyle = FontStyle.Bold,
            padding = new RectOffset()
            {
                left = 0,
                right = 0,
                top = 0,
                bottom = 0
            },
            clipping = TextClipping.Overflow
        };
        #endregion

        if (!hideLog)
        {
            #region HorizontalLayout
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Clear", guiStyle_button))
            {
                myLog = string.Empty;
                myLogQueue.Clear();
            }
            if (GUILayout.Button("Hide", guiStyle_button))
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
            if (GUILayout.Button("Show Log", guiStyle_button))
                hideLog = false;
        }
    }
}