using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExampleControllerWebRequest : MonoBehaviour
{
    [Header("Url")]
    [SerializeField] private InputField m_UrlText = null; 
    [Header("Get")]
    [SerializeField] private Button m_GetButton = null;
    [SerializeField] private Text m_GetTextArea = null;
    [Header("Post")]
    [SerializeField] private Button m_PostButton = null;
    [SerializeField] private InputField m_PostFormName = null;
    [SerializeField] private InputField m_PostFormValue = null;
   
    private void Start()
    {
        m_GetButton.onClick.AddListener(OnClickGetButton);
        m_PostButton.onClick.AddListener(OnClickPostButton);
    }

    private void OnClickGetButton()
    {
        m_GetTextArea.text = ""; //Clear text area
        string url = m_UrlText.text;

        WebController.s_Instance.GetWebRequest(url, (result)=> 
        {
            m_GetTextArea.text = result;
            Debug.Log("Completed GET request");
        });
    }

    private void OnClickPostButton()
    {
        string url = m_UrlText.text;
        FormField form = new FormField(m_PostFormName.text,m_PostFormValue.text);

        WebController.s_Instance.PostWebRequest(url, (result)=> 
        {
            Debug.Log("Completed POST request");
        }, form);
    }
}
