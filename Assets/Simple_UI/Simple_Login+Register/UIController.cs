using System;
using UnityEngine;

namespace Simple_UI
{
    /// <summary>
    /// This class implements LognRegisterController methods so it works with DB verifications
    /// </summary>
    public class UIController : LoginRegisterController
    {
        #region LOGIN
        public override void OnConfirmLoginButtonClickImpl(params string[] loginParams)
        {
            string username = loginParams[0];
            string password = loginParams[1];

            if (VerifyLoginImpl(username, password))
            {
                ExampleLoginRegisterController.s_Instance.SetCurrentUser(username, password);
                CloseLogin();
                OpenMainMenu();
            }
        }

        protected override bool VerifyLoginImpl(params string[] loginParams)
        {
            string username = loginParams[0];
            string password = loginParams[1];

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
