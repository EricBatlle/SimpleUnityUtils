using UnityEngine;
using UnityEngine.UI;

public class ExampleEmbeddedBrowserController : MonoBehaviour
{
    [SerializeField] private string pageToOpen = "https://www.google.com/";
    [Space()]
    [SerializeField] private Button openerButton = null;
    [SerializeField] private Text infoText = null;

    private void Awake()
    {
        openerButton.onClick.AddListener(OpenBrowser);
    }

    private void OpenBrowser()
    {
        BrowserOpener.s_Instance.OnButtonClicked(pageToOpen);
        infoText.text = "Opened browser in: " + pageToOpen;
        infoText.text += "\n (Remember that you can only see it on Mobile Builds!!!)";
    }
}
