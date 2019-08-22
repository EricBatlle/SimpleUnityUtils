using System;
using UnityEngine;

namespace Simple_UI
{
    public class UIController : LoginRegisterController
    {
        #region LOGIN
        public override void OnConfirmLoginButtonClickImpl(string username, string password)
        {
            if (VerifyLoginImpl(username, password))
            {
                ExampleLoginRegisterController.s_Instance.SetCurrentUser(username, password);
                CloseLogin();
                OpenMainMenu();
            }
        }

        protected override bool VerifyLoginImpl(string username, string password)
        {
            //Check if the username exists and if it match with their password
            if (ExampleLoginRegisterController.s_Instance.VerifyIfUsernameMatchPassword(username, password))
            {
                base.login.SetInformationPanel("");
                return true;
            }
            else if (ExampleLoginRegisterController.s_Instance.VerifyIfUsernameExists(username))
                base.login.SetInformationPanel("Wrong credentials");
            else
                base.login.SetInformationPanel("This user does not exist");

            return false;
        }
        #endregion

        #region REGISTER
        public override void OnConfirmRegisterButtonClickImpl(object user)
        {
            if (VerifyRegisterImpl(user))
            {
                ExampleLoginRegisterController.s_Instance.RegisterNewUser((User)user);
                CloseRegister();
                OpenLogin();
            }
        }

        protected override bool VerifyRegisterImpl(object user)
        {
            User userCasted = (User)user;
            //Check if the username or email already exists
            if (ExampleLoginRegisterController.s_Instance.VerifyIfUsernameExists(userCasted.Username))
                base.register.SetInformationPanel("This username is already taken");
            else if (ExampleLoginRegisterController.s_Instance.VerifyIfEmailExists(userCasted.Email))
                base.register.SetInformationPanel("This email is already taken");
            else
            {
                base.login.SetInformationPanel("");
                return true;
            }

            return false;
        }
        #endregion

        #region MAINMENU
        public override void OnGoBackToLoginFromMainMenuButtonClickImpl()
        {
            CloseMainMenu();
            OpenLogin();
            ExampleLoginRegisterController.s_Instance.SetCurrentUser(null, null);
            //CurrentUser = null;
        }
        #endregion
    }
}
