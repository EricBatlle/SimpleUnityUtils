using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UIController : Singleton<UIController>
{
    [Header("UIComponents")]
    [SerializeField] private Login login = null;    
    [SerializeField] private Register register = null;    
    [SerializeField] private MainMenu mainMenu = null;
    [SerializeField] private Text debugInformationText = null;

    [Header("Users")]
    [SerializeField] private List<User> registerUsersList = new List<User>();
    [SerializeField] private User currentUser = null;
    public User CurrentUser
    {
        get { return currentUser; }
        set //Not called if change property from editor
        {
            currentUser = value;
            SetDebugInformation();
        }
    }

    private void Start()
    {
        StartUI();
    }

    public void StartUI(Action<string> OnSuccess = null)
    {
        //Close everything in case is open
        CloseMainMenu();
        CloseRegister();
        //Starts UI screen with the Login panel
        OpenLogin();
    }

    private void SetDebugInformation()
    {
        this.debugInformationText.text =
            "DEBUG INFORMATION \n";

        if(this.currentUser != null)
        {
            this.debugInformationText.text +=
            "Username: " + this.currentUser.Username + "\n" +
            "Password: " + this.currentUser.Password + "\n" +
            "Email: " + this.currentUser.Email;
        }
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
    public void OnConfirmLoginButtonClick(string username, string password)
    {
        if(VerifyLogin(username, password))
        {
            CurrentUser = registerUsersList.Single(u => (u.Username == username)&&(u.Password == password));
            CloseLogin();            
            OpenMainMenu();
        }
    }
    public void OnGoRegisterButtonClick()
    {
        CloseLogin();
        OpenRegister();
    }
    private bool VerifyLogin(string username, string password)
    {
        //Check if the username exists and if it match with their password
        if (registerUsersList.Any(u => (u.Username == username) && (u.Password == password)))
        {
            UIController.s_Instance.login.SetInformationPanel("");
            return true;
        }
        else if(registerUsersList.Any(u => u.Username == username))
            this.login.SetInformationPanel("Wrong credentials");
        else
            this.login.SetInformationPanel("This user does not exist");        
       
        return false;
    }
    #endregion
    #region REGISTER
    public void OnConfirmRegisterButtonClick(User user)
    {   
        if(VerifyRegister(user))
        {
            registerUsersList.Add(user);
            CloseRegister();
            OpenLogin();
        }
    }
    public void OnGoBackToLoginFromRegisterButtonClick()
    {
        CloseRegister();
        OpenLogin();
    }
    private bool VerifyRegister(User user)
    {
        //Check if the username or email already exists
        if(registerUsersList.Any(u => u.Username == user.Username))        
            this.register.SetInformationPanel("This username is already taken");
        else if(registerUsersList.Any(u => u.Email == user.Email))        
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
        CurrentUser = null;
    }
    #endregion
    #endregion
}
