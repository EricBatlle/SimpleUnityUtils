namespace Simple_DB
{
    /// <summary>
    /// This class extends the original UIController to modify verification steps...
    /// ...so it works with DB verifications
    /// 
    /// !!!!!WARNING!!!!
    /// THIS SCRIPT REQUIRES THE Simple_UI/Simple_Login+Register scripts!
    /// </summary>
    public class UIController : Simple_UI.LoginRegisterController
    {
        #region LOGIN
        public override void OnConfirmLoginButtonClickImpl(params string[] loginParams)
        {
            string username = loginParams[0];
            string password = loginParams[1];
            DBController.s_Instance.PostLoginVerify((result) =>
            {
            if (VerifyLoginImpl(result))
            {
                    string newUserDBString = WebResponse.GetResponseInfo(result);
                    UserDB newUserDB = JsonManager.DeserializeFromJson<UserDB>(newUserDBString);
                    ExampleDBController.s_Instance.SetCurrentUser(newUserDB);
                    CloseLogin();
                    OpenMainMenu();
                }
            }, username, password);

        }

        protected override bool VerifyLoginImpl(params string[] loginParams)
        {
            string result = loginParams[0];
            if (WebResponse.isResultOk(result))
            {
                base.login.SetInformationPanel("");
                return true;
            }
            else if (WebResponse.isEqualTo(WebResponse.ERROR_LOGIN_UNEXISTANT_USERNAME, result))
                base.login.SetInformationPanel("This user does not exist");
            else if (WebResponse.isEqualTo(WebResponse.ERROR_LOGIN_WRONG_CREDENTIALS, result))
                base.login.SetInformationPanel("Wrong credentials");

            return false;
        }        
        #endregion

        #region REGISTER        
        public override void OnConfirmRegisterButtonClickImpl(object user)
        {            
            DBController.s_Instance.PostNewUser((result) =>
            {
                if (VerifyRegisterImpl(result))
                {
                    string newUserDBJson = WebResponse.GetResponseInfo(result);
                    UserDB newUserDB = JsonManager.DeserializeFromJson<UserDB>(newUserDBJson);                    
                    CloseRegister();
                    OpenLogin();
                }
            }, (User)user);                     
        }

        protected override bool VerifyRegisterImpl(object resultObject)
        {
            string result = (string)resultObject;
            if (WebResponse.isResultOk(result))
            {
                base.login.SetInformationPanel("");
                return true;
            }
            else if (WebResponse.isEqualTo(WebResponse.ERROR_REGISTER_DUPLICATE_USERNAME, result))
                base.register.SetInformationPanel("This username is already taken");
            else
                base.register.SetInformationPanel("Error on register");

            return false;
        }
        #endregion

        #region MAINMENU
        public override void OnGoBackToLoginFromMainMenuButtonClickImpl()
        {
            CloseMainMenu();
            ExampleDBController.s_Instance.SetCurrentUser(null);
            OpenLogin();
        }
        #endregion
    }
}
