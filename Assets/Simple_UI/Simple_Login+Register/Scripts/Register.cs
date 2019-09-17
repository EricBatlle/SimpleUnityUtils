using UnityEngine;
using UnityEngine.UI;

namespace Simple_UI
{
    /// <summary>
    /// Class in charge of implementing Register buttons events and get information from the Register form
    /// </summary>
    public class Register : MonoBehaviour
    {
        //Used to let inheritance and abstraction works and set events when the controller is assigned
        [SerializeField] private LoginRegisterController lrController = null;
        public LoginRegisterController LRController
        {
            get { return lrController; }
            set { lrController = value; SetOnClickEvents(); }   //assign new value and set button OnclickEvents
        }

        [Header("Register Fields")]
        [SerializeField] private InputField usernameInput = null;
        [SerializeField] private InputField passwordInput = null;
        [SerializeField] private InputField emailInput = null;

        [Header("Display Information")]
        [SerializeField] private Text infoText = null;

        [Header("Buttons")]
        [SerializeField] private Button confirmRegisterButton = null;
        [SerializeField] private Button backButton = null;

        private void SetOnClickEvents()
        {
            confirmRegisterButton.onClick.AddListener(() => { LRController.OnConfirmRegisterButtonClickImpl(GetRegisterUserData()); });
            backButton.onClick.AddListener(LRController.OnGoBackToLoginFromRegisterButtonClick);
        }

        protected virtual object GetRegisterUserData()
        {
            string username = usernameInput.text;
            string password = passwordInput.text;
            string email = emailInput.text;
            object newUser = new User(username, password, email);

            return newUser;
        }

        public void SetInformationPanel(string newInfo)
        {
            this.infoText.text = newInfo;
        }
    }
}
