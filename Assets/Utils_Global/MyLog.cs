using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyLog : MonoBehaviour
{
    #region CustomLogs
    [Serializable]
    public class LogWindowStyle
    {
        //Log window
        public float viewX = Screen.width / 2.5f;
        public float viewY = 0;
        public float viewWidth = Screen.width - Screen.width / 2.5f;
        public float viewHeight = Screen.height - Screen.height / 1.5f;
        public float scrollbarWidth = 100f;
        public float scrollbarHeight = 100f;
        public int fontSize = 10;
       
        #region Constructors
        public LogWindowStyle()
        {
            this.viewX = Screen.width / 2.5f;
            this.viewY = 0;
            this.viewWidth = Screen.width - Screen.width / 2.5f;
            this.viewHeight = Screen.height - Screen.height / 1.5f;
            this.scrollbarWidth = viewWidth / 24;
            this.scrollbarHeight = viewWidth / 24;
        }
        public LogWindowStyle(int fontSize)
        {
            this.viewX = Screen.width / 2.5f;
            this.viewY = 0;
            this.viewWidth = Screen.width - Screen.width / 2.5f;
            this.viewHeight = Screen.height - Screen.height / 1.5f;
            this.scrollbarWidth = viewWidth / 24;
            this.scrollbarHeight = viewWidth / 24;
            this.fontSize = fontSize;
        }
        public LogWindowStyle(float viewWidth, float viewHeight, int fontSize)
        {
            this.viewWidth = viewWidth;
            this.viewHeight = viewHeight;
            this.fontSize = fontSize;
        }
        public LogWindowStyle(float viewWidth, float viewHeight, float scrollbarWidth, float scrollbarHeight, int fontSize)
        {
            this.viewWidth = viewWidth;
            this.viewHeight = viewHeight;
            this.scrollbarWidth = scrollbarWidth;
            this.scrollbarHeight = scrollbarHeight;
            this.fontSize = fontSize;
        }
        public LogWindowStyle(float x, float y, float viewWidth, float viewHeight, float scrollbarWidth, float scrollbarHeight, int fontSize)
        {
            this.viewX = x;
            this.viewY = y;
            this.viewWidth = viewWidth;
            this.viewHeight = viewHeight;
            this.scrollbarWidth = scrollbarWidth;
            this.scrollbarHeight = scrollbarHeight;
            this.fontSize = fontSize;
        }
        #endregion
    }
    //PredefinedPositions
    public enum PredefinedPosition
    {
        TopLeft, TopRight, BottomRight, BottomLeft, Custom
    }
    public Dictionary<PredefinedPosition, Vector2> PredefinedPositionToVector2Dictionary = new Dictionary<PredefinedPosition, Vector2>()
    {
        { PredefinedPosition.Custom, new Vector2(0,0) },
        { PredefinedPosition.TopLeft, new Vector2(0,0) },
        { PredefinedPosition.TopRight, new Vector2(Screen.width / 2.5f, 0) },
        { PredefinedPosition.BottomRight, new Vector2(Screen.width / 2.5f, Screen.height / (1.5f+0.1f)) },
        { PredefinedPosition.BottomLeft, new Vector2(0, Screen.height / (1.5f+0.1f)) }
    };
    //PredefinedDevice
    public enum PredefinedDevice
    {
        Default, Pocophone
    }
    public Dictionary<PredefinedDevice, LogWindowStyle> PredefinedDeviceToLogWindowStyleDictionary = new Dictionary<PredefinedDevice, LogWindowStyle>()
    {
        { PredefinedDevice.Default, new LogWindowStyle() },
        { PredefinedDevice.Pocophone, new LogWindowStyle(30) }
    };
    #endregion

    [SerializeField] private bool hideLog = false;
    [SerializeField] private PredefinedDevice predefinedDevice = PredefinedDevice.Default;
    [SerializeField] private PredefinedPosition predefinedPosition = PredefinedPosition.TopRight;
    [SerializeField] private LogWindowStyle logWindowStyle = new LogWindowStyle();
    [Space()]
    [SerializeField] private Color logsColor = Color.white;
    [SerializeField] private Vector2 customWindowPosition = new Vector2(Screen.width / 2.5f, 0);
    private string myLog;
    private Queue myLogQueue = new Queue();
    private Vector2 scrollPosition = new Vector2();

    private void OnEnable()
    {
        Application.logMessageReceivedThreaded += HandleLog;
        SetPredefinedPosition(predefinedPosition);
        SetPredefinedStyle(predefinedDevice);
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
                textColor = Color.black;
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
        #region GUI Styles
        //LogWindow Position
        SetPredefinedPosition(predefinedPosition);
        //LogWindow Style
        GUI.skin.verticalScrollbar.fixedWidth = logWindowStyle.scrollbarWidth;
        GUI.skin.verticalScrollbarThumb.fixedWidth = logWindowStyle.scrollbarWidth;

        GUI.skin.horizontalScrollbar.fixedWidth = logWindowStyle.viewWidth - GUI.skin.verticalScrollbar.fixedWidth;
        GUI.skin.horizontalScrollbar.fixedHeight = logWindowStyle.scrollbarHeight;
        GUI.skin.horizontalScrollbarThumb.fixedHeight = GUI.skin.horizontalScrollbar.fixedHeight;

        //Buttons Style
        GUIStyle guiStyle_button = new GUIStyle(GUI.skin.button)
        {
            fontSize = logWindowStyle.fontSize
        };

        //Logs Style
        GUIStyle guiStyle_logs = new GUIStyle(GUI.skin.label)
        {            
            richText = true,
            alignment = TextAnchor.MiddleLeft,
            wordWrap = false,
            fontSize = logWindowStyle.fontSize,            
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
            GUI.BeginGroup(new Rect(logWindowStyle.viewX, logWindowStyle.viewY, logWindowStyle.viewWidth, Screen.height));
            #region HorizontalLayout
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Clear", guiStyle_button))
            {
                myLog = string.Empty;
                myLogQueue.Clear();
            }
            if (GUILayout.Button("Hide", guiStyle_button))
                hideLog = true;
            if (GUILayout.Button("+", guiStyle_button))
                logWindowStyle.fontSize += 10;
            if (GUILayout.Button("-", guiStyle_button))
            {
                if (logWindowStyle.fontSize > 10)
                    logWindowStyle.fontSize -= 10;
            }
            GUILayout.EndHorizontal();
            #endregion
            #region ScrollView
            scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Width(logWindowStyle.viewWidth), GUILayout.Height(logWindowStyle.viewHeight));

            GUILayout.Label(myLog, guiStyle_logs);
            GUILayout.EndScrollView();
            #endregion
            GUI.EndGroup();
        }
        else
        {
            GUI.BeginGroup(new Rect(logWindowStyle.viewX, logWindowStyle.viewY, logWindowStyle.viewWidth, Screen.height));
            if (GUILayout.Button("Show Log", guiStyle_button))
                hideLog = false;
            GUI.EndGroup();
        }
    }

    private void SetPredefinedStyle(PredefinedDevice predefinedDevice)
    {
        logWindowStyle = PredefinedDeviceToLogWindowStyleDictionary[predefinedDevice];        
    }
    private void SetPredefinedPosition(PredefinedPosition predefinedPosition)
    {
        Vector2 windowPosition = new Vector2();
        windowPosition = (predefinedPosition == PredefinedPosition.Custom) ? customWindowPosition : PredefinedPositionToVector2Dictionary[predefinedPosition];
        logWindowStyle.viewX = windowPosition.x;
        logWindowStyle.viewY = windowPosition.y;
    }
}