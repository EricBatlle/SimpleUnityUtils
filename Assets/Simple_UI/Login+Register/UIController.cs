using System;
using UnityEngine;

namespace Simple_UI
{
    public class UIController : Singleton<UIController>
    {
        [Header("UIComponents")]
        [SerializeField] private Login login = null;    
        [SerializeField] private Register register = null;    
        [SerializeField] private MainMenu mainMenu = null;
    
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
            if(!login.gameObject.activeSelf)
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
        public virtual void OnConfirmLoginButtonClick(string username, string password)
        {
            if(VerifyLogin(username, password))
            {
                ExampleLoginRegisterController.s_Instance.SetCurrentUser(username,password);            
                CloseLogin();            
                OpenMainMenu();
            }
        }
        public void OnGoRegisterButtonClick()
        {
            CloseLogin();
            OpenRegister();
        }
        protected virtual bool VerifyLogin(string username, string password)
        {
            //Check if the username exists and if it match with their password
            if (ExampleLoginRegisterController.s_Instance.VerifyIfUsernameMatchPassword(username, password))
            {
                login.SetInformationPanel("");
                return true;
            }
            else if (ExampleLoginRegisterController.s_Instance.VerifyIfUsernameExists(username))
                this.login.SetInformationPanel("Wrong credentials");
            else
                this.login.SetInformationPanel("This user does not exist");        
       
            return false;
        }
        #endregion
        #region REGISTER
        public virtual void OnConfirmRegisterButtonClick(User user)
        {   
            if(VerifyRegister(user))
            {
                ExampleLoginRegisterController.s_Instance.RegisterNewUser(user);
                CloseRegister();
                OpenLogin();
            }
        }
        public void OnGoBackToLoginFromRegisterButtonClick()
        {
            CloseRegister();
            OpenLogin();
        }
        protected virtual bool VerifyRegister(User user)
        {
            //Check if the username or email already exists
            if(ExampleLoginRegisterController.s_Instance.VerifyIfUsernameExists(user.Username))        
                this.register.SetInformationPanel("This username is already taken");
            else if(ExampleLoginRegisterController.s_Instance.VerifyIfEmailExists(user.Email))        
                this.register.SetInformationPanel("This email is already taken");
            else
            {
                this.login.SetInformationPanel("");
                return true;
            }
        
            return false;
        }
        #endregion
        #region MAINMENU
        public void OnGoBackToLoginFromMainMenuButtonClick()
        {
            CloseMainMenu();
            OpenLogin();
            ExampleLoginRegisterController.s_Instance.SetCurrentUser(null,null);
            //CurrentUser = null;
        }
        #endregion
        #endregion
    }
}
