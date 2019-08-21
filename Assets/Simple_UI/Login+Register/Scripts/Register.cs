using UnityEngine;
using UnityEngine.UI;

namespace Simple_UI
{
    public class Register : MonoBehaviour
    {
        [Header("Register Fields")]
        [SerializeField] private InputField usernameInput = null;
        [SerializeField] private InputField passwordInput = null;
        [SerializeField] private InputField emailInput = null;

        [Header("Display Information")]
        [SerializeField] private Text infoText = null;

        [Header("Buttons")]
        [SerializeField] private Button confirmRegisterButton = null;
        [SerializeField] private Button backButton = null;

        private void Awake()
        {
            confirmRegisterButton.onClick.AddListener(() => { UIController.s_Instance.OnConfirmRegisterButtonClick(GetRegisterUserData()); });
            backButton.onClick.AddListener(UIController.s_Instance.OnGoBackToLoginFromRegisterButtonClick);
        }

        private User GetRegisterUserData()
        {
            string username = usernameInput.text;
            string password = passwordInput.text;
            string email = emailInput.text;
            User newUser = new User(username, password, email);

            return newUser;
        }

        public void SetInformationPanel(string newInfo)
        {
            this.infoText.text = newInfo;
        }
    }
}
