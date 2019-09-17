using UnityEngine;
using UnityEngine.UI;

namespace Simple_UI
{
    /// <summary>
    /// Class in charge of implementing Login buttons events and get information from the Login form
    /// </summary>
    public class Login : MonoBehaviour
    {
        #region Variables
        //Used to let inheritance and abstraction works and set events when the controller is assigned
        [SerializeField] private LoginRegisterController lrController = null;
        public LoginRegisterController LRController
        {
            get { return lrController; }
            set { lrController = value; SetOnClickEvents(); }   //assign new value and set button OnclickEvents
        }
        
        [Header("Login Fields")]
        [SerializeField] private InputField usernameInput = null;
        [SerializeField] private InputField passwordInput = null;
        [Header("Display Information")]
        [SerializeField] private Text infoText = null;
        [Header("Buttons")]
        [SerializeField] private Button confirmButton = null;
        [SerializeField] private Button registerButton = null;
        [SerializeField] private Button loginLaterButton = null;
        #endregion

        private void SetOnClickEvents()
        {
            confirmButton.onClick.AddListener(() => { lrController.OnConfirmLoginButtonClickImpl(GetUsername(), GetPassword()); });
            registerButton.onClick.AddListener(lrController.OnGoRegisterButtonClick);
            loginLaterButton.onClick.AddListener(lrController.OnGoLoginLater);
        }
    
        private string GetUsername()
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
}
