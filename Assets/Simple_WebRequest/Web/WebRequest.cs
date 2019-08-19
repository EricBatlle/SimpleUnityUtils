using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// Class that encapsulates Request behaviour, creating and dispatching requests coroutines
/// Needs to be Serializable, so UnityEditor can show the list on WebController gameObject
/// </summary>
[Serializable]
public class WebRequest
{
    private Action<string> m_OnSuccessfulWebRequest = null;
    private string m_Url = "https://jsonplaceholder.typicode.com/todos/1";

    //Constructor
    public WebRequest(string url = "https://jsonplaceholder.typicode.com/todos/1", Action<string> OnSuccessfulWebRequest = null)
    {
        this.m_Url = url;
        this.m_OnSuccessfulWebRequest = OnSuccessfulWebRequest;
    }

    #region Initiate Coroutines
    //Initiate Get Coroutine
    public void Get()
    {
        WebController.s_Instance.StartCoroutine(GetRequestCoroutine());
    }
    //Initiate Post Coroutine
    public void Post(params FormField[] forms)
    {
        WebController.s_Instance.StartCoroutine(PostRequestCoroutine(forms));
    }
    #endregion

    #region GET/POST Coroutines
    //Coroutine to make GET requests, dispatch m_OnSuccessfulWebRequest when it finishes without www errors
    private IEnumerator GetRequestCoroutine()
    {
        using (UnityWebRequest www = UnityWebRequest.Get(m_Url))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
                Debug.Log("Error: " + www.error + " on the url: " + m_Url);
            else
            {                
                m_OnSuccessfulWebRequest(www.downloadHandler.text);
                RemoveRequestFromWebControllerList();
            }
        }
    }
    //Coroutine to make POST requests, dispatch m_OnSuccessfulWebRequest when it finishes without www errors
    private IEnumerator PostRequestCoroutine(params FormField[] fields)
    {
        WWWForm form = new WWWForm();
        foreach(FormField fieldObject in fields)
        {
            if (fieldObject.IsBinaryField())
                form.AddBinaryData(fieldObject.fieldName, fieldObject.valueInBytes);
            else
                form.AddField(fieldObject.fieldName, fieldObject.value);
        }
        using (UnityWebRequest www = UnityWebRequest.Post(m_Url,form))
        {
            yield return www.SendWebRequest();

            if(www.isNetworkError || www.isHttpError)
                Debug.Log("Error: "+ www.error + " on the url: "+ m_Url );
            else
            {
                m_OnSuccessfulWebRequest?.Invoke(www.downloadHandler.text);
                RemoveRequestFromWebControllerList();
            }                
        }
    }
    #endregion

    //Remove this WebRequest from WebController requests list
    private void RemoveRequestFromWebControllerList()
    {
        WebController.s_Instance.webRequestsList.Remove(this);
    }
}