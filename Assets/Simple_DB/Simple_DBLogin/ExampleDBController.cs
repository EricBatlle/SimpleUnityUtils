using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Simple_DB
{
    /// <summary>
    /// Example Controller to illustrate how Simple_DBLogin works
    /// </summary>
    public class ExampleDBController : Singleton<ExampleDBController>
    {
        [Header("Users")]
        [SerializeField] private UserDB currentUser = null;
        [SerializeField] private List<string> registeredUsernameList = new List<string>();
        public UserDB CurrentUser
        {
            get { return currentUser; }
            set //Not called if change property from editor
            {
                currentUser = value;
                SetDebugInformation();
            }
        }
        [Header("Recursive Petitions Example")]
        [SerializeField] private Text usernamesText = null;
        [SerializeField] private Button mainMenuShowAllUsernamesButton = null;
        [Header("Debug Info")]
        [SerializeField] private Text debugInformationText = null;

        private void Start()
        {
            mainMenuShowAllUsernamesButton.onClick.AddListener(ShowAllUsernames);
            UIController.s_Instance.StartUI();
        }

        #region Login/Register
        public void SetCurrentUser(UserDB newUser)
        {
            CurrentUser = newUser;
        }
        //No RegisterNewUser as it is registered on the DB
        #endregion

        #region Just to show how recursive petitions works
        public void AddUsernameToList(UserDB user)
        {
            registeredUsernameList.Add(user.Username);
        }
        public void ShowAllUsernames()
        {
            usernamesText.text = "";
            registeredUsernameList.Clear();
            DBController.s_Instance.GetAllUsersUsername((result)=> 
            {
                foreach (string username in registeredUsernameList)
                    usernamesText.text += username+"\n";
            });
        }
        #endregion
        private void SetDebugInformation()
        {
            this.debugInformationText.text =
                "DEBUG INFORMATION \n";

            if (this.currentUser != null)
            {
                this.debugInformationText.text +=
                "ID: " + this.currentUser.UserID + "\n" +
                "Username: " + this.currentUser.Username + "\n" +
                "Password: " + this.currentUser.Password + "\n" +
                "Email: " + this.currentUser.Email;
            }
        }
    }
}
