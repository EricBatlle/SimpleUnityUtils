using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Simple_UI
{
    public class ExampleLoginRegisterController : Singleton<ExampleLoginRegisterController>
    {
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
        [Header("Debug Info")]
        [SerializeField] private Text debugInformationText = null;

        private void Start()
        {
            UIController.s_Instance.StartUI();
        }

        public void SetCurrentUser(string username, string password)
        {
            try
            {
                CurrentUser = registerUsersList.Single(u => (u.Username == username) && (u.Password == password));
            }
            catch(InvalidOperationException)
            {
                CurrentUser = null;
            }
        }
        public void RegisterNewUser(User user)
        {
            registerUsersList.Add(user);
        }

        #region Login/Register Verify
        public bool VerifyIfUsernameMatchPassword(string username, string password)
        {
            return registerUsersList.Any(u => (u.Username == username) && (u.Password == password));
        }
        public bool VerifyIfUsernameExists(string username)
        {
            return registerUsersList.Any(u => u.Username == username);
        }
        public bool VerifyIfEmailExists(string email)
        {
            return registerUsersList.Any(u => u.Email == email);
        }
        #endregion

        private void SetDebugInformation()
        {
            this.debugInformationText.text = "DEBUG INFORMATION \n";

            if (this.currentUser != null)
            {
                this.debugInformationText.text +=
                "Username: " + this.currentUser.Username + "\n" +
                "Password: " + this.currentUser.Password + "\n" +
                "Email: " + this.currentUser.Email;
            }
        }
    }
}
