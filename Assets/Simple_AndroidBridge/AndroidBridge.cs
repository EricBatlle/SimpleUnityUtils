using System;
using UnityEngine;

public abstract class AndroidBridge : MonoBehaviour
{
    private static string ANDROIDBRIDGE_GO_NAME = "AndroidBridge";
    private static char[] delimiterChars = { '~' };
    private string[] results;

    protected void SetAndroidBridgeName()
    {
        this.gameObject.name = ANDROIDBRIDGE_GO_NAME;
    }
    
    private void OnResult(string recognizedResult)
    {
        results = recognizedResult.Split(delimiterChars);
        OnGetResults(results);
    }
    protected abstract void OnGetResults(string[] results);

    #region AndroidCall Utility Methods
    //Android Call
    public void AndroidCall(string method, params object[] args)
    {
        using (AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            using (AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity"))
            {
                if (args != null)
                    jo.Call(method, args);
                else
                    jo.Call(method);
            }
        }
    }
    public T AndroidCall<T>(string method, params object[] args)
    {
        using (AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            using (AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity"))
            {
                if (args != null)
                    return jo.Call<T>(method, args);
                else
                    return jo.Call<T>(method);
            }
        }
    }

    //Android Static Call
    public void AndroidStaticCall(string method, params object[] args)
    {
        using (AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            using (AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity"))
            {
                if (args != null)
                    jo.CallStatic(method, args);
                else
                    jo.CallStatic(method);
            }
        }
    }
    public T AndroidStaticCall<T>(string method, params object[] args)
    {
        using (AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            using (AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity"))
            {
                if (args != null)
                    return jo.CallStatic<T>(method, args);
                else
                    return jo.CallStatic<T>(method);
            }
        }
    }

    //Android Runnable Call
    public void AndroidRunnableCall(string method, params object[] args)
    {
        using (AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            using (AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity"))
            {
                if (args != null)
                {
                    jo.Call("runOnUiThread", new AndroidJavaRunnable(() =>
                    {
                        AndroidCall(method, args);
                    }));
                }
                else
                {
                    jo.Call("runOnUiThread", new AndroidJavaRunnable(() =>
                    {
                        AndroidCall(method);
                    }));
                }
            }
        }
    }
    #endregion
}
