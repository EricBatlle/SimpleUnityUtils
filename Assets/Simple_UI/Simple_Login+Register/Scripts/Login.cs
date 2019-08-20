using UnityEngine;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    [Header("Login Fields")]
    [SerializeField] private InputField usernameInput = null;
    [SerializeField] private InputField passwordInput = null;
    [Header("Display Information")]
    [SerializeField] private Text infoText = null;
    [Header("Buttons")]
    [SerializeField] private Button confirmButton = null;
    [SerializeField] private Button registerButton = null;
    [SerializeField] private Button loginLaterButton = null;

    private void Awake()
    {
        confirmButton.onClick.AddListener(() => { UIController.s_Instance.OnConfirmLoginButtonClick(GetUsername(), GetPassword()); });
        registerButton.onClick.AddListener(UIController.s_Instance.OnGoRegisterButtonClick);
        loginLaterButton.onClick.AddListener(UIController.s_Instance.OnGoLoginLater);
    }
    
    public string GetUsername()
    {
        return usernameInput.text;
    }
    private string GetPassword()
    {
        return passwordInput.text;
    }

    public void SetInformationPanel(string newInfo)
    {
        this.infoText.text = newInfo;
    }
}
