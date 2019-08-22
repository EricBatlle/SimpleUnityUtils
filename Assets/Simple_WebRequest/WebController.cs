using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Main Controller to call and manage GET and POSTS requests
/// </summary>
public class WebController : Singleton<WebController>
{
    //Used to see in the inspector all the requests on the queue
    [SerializeField] public List<WebRequest> webRequestsList = new List<WebRequest>();

    //Web request with GET http protocol 
    public void GetWebRequest(string url = "https://jsonplaceholder.typicode.com/todos/1", Action<string> OnCompleteRequest = null)
    {
        WebRequest webRequest = new WebRequest(url, OnCompleteRequest);
        webRequestsList.Add(webRequest);
        webRequest.Get();
    }

    //Web request with POST http protocol, can post values through multiple formFields
    public void PostWebRequest(string url = "https://jsonplaceholder.typicode.com/todos/1", Action<string> OnCompleteRequest = null, params FormField[] forms)
    {
        WebRequest webRequest = new WebRequest(url, OnCompleteRequest);
        webRequestsList.Add(webRequest);
        webRequest.Post(forms);
    }
}
