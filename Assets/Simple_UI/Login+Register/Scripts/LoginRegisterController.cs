using System;
using UnityEngine;

namespace Simple_UI
{
    public abstract class LoginRegisterController : Singleton<LoginRegisterController>
    {
        [Header("UIComponents")]
        [SerializeField] protected Login login = null;
        [SerializeField] protected Register register = null;
        [SerializeField] protected MainMenu mainMenu = null;

        public void StartUI(Action<string> OnSuccess = null)
        {
            //Close everything in case is open
            CloseMainMenu();
            CloseRegister();
            //Starts UI screen with the Login panel
            OpenLogin();
        }

        #region OpenClose UI components
        #region LOGIN
        public void OpenLogin()
        {
            if (!login.gameObject.activeSelf)
                login.gameObject.SetActive(true);
        }
        public void CloseLogin()
        {
            if (login.gameObject.activeSelf)
                login.gameObject.SetActive(false);
        }
        #endregion
        #region REGISTER
        public void OpenRegister()
        {
            if (!register.gameObject.activeSelf)
                register.gameObject.SetActive(true);
        }
        public void CloseRegister()
        {
            if (register.gameObject.activeSelf)
                register.gameObject.SetActive(false);
        }
        #endregion
        #region MAINMENU
        public void OpenMainMenu()
        {
            if (!mainMenu.gameObject.activeSelf)
                mainMenu.gameObject.SetActive(true);
        }
        public void CloseMainMenu()
        {
            if (mainMenu.gameObject.activeSelf)
                mainMenu.gameObject.SetActive(false);
        }
        #endregion
        #endregion

        #region OnClickUIEventsNavigation
        #region LOGIN
        public void OnGoLoginLater()
        {
            CloseLogin();
            OpenMainMenu();
        }        
        public void OnGoRegisterButtonClick()
        {
            CloseLogin();
            OpenRegister();
        }
        //Abstract Implementations
        abstract public void OnConfirmLoginButtonClickImpl(params string[] loginParams);
        abstract protected bool VerifyLoginImpl(params string[] loginParams);
        #endregion
        #region REGISTER                
        public void OnGoBackToLoginFromRegisterButtonClick()
        {
            CloseRegister();
            OpenLogin();
        }
        //Abstract Implementations
        abstract public void OnConfirmRegisterButtonClickImpl(object user);
        abstract protected bool VerifyRegisterImpl(object user);
        #endregion
        #region MAINMENU
        abstract public void OnGoBackToLoginFromMainMenuButtonClickImpl();        
        #endregion
        #endregion
    }
}
