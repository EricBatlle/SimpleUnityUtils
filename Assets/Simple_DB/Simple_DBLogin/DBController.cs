using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simple_DB
{
    /// <summary>
    /// This class makes all the calls to the php files which at the same time deals with DB queries petitions
    /// </summary>
    public class DBController : Singleton<DBController>
    {
        //[SerializeField] private static string phpFilesDirectory = "D:/xampp/htdocs/myProjectOrWhateverfoldername/";
        [SerializeField] private static string phpFilesServer = "http://rucker/Simple_DB/";
        
        #region GET
        //Deletes the content of the tables, not the tables
        public void CleanDB(Action<string> OnSuccess = null)
        {
            string url = phpFilesServer + "CleanDB.php";
            WebController.s_Instance.GetWebRequest(url, OnSuccess);
        }
        public void GetAllUsers(Action<string> OnSuccess = null)
        {
            string url = phpFilesServer + "GetAllUsers.php";
            WebController.s_Instance.GetWebRequest(url, OnSuccess);
        }
        public void GetUserInfoFromID(Action<string> OnSuccess = null, int userID = 0)
        {
            string url = phpFilesServer + "GetUserInfoFromID.php";
            WebController.s_Instance.PostWebRequest(url, OnSuccess,
                new FormField("userID", userID.ToString()));
        }        
        #endregion

        #region POST
        public void PostNewUser(Action<string> OnSuccess = null, User newUser = null)
        {
            string newUserString = JsonManager.SerializeToJson<User>(newUser);
            string url = phpFilesServer + "GenerateNewUser.php";
            WebController.s_Instance.PostWebRequest(url, OnSuccess, 
                new FormField("newUser", newUserString));
        }
        
        public void PostLoginVerify(Action<string> OnSuccess = null, string newUsername = "defaultUsername", string newPassword = "defaultPassword")
        {
            string url = phpFilesServer + "LoginVerify.php";
            WebController.s_Instance.PostWebRequest(url, OnSuccess,
                new FormField("newUsername", newUsername),
                new FormField("newPassword", newPassword));
        }
        #endregion

        #region RecursivePetitions
        #region GET_Recursive
        public void GetAllUsersUsername(Action<string> OnMultipleActionsEnd = null, List<UserDB> usersList = null)
        {
            //Little non-sense example to show how Recursive petitions works

            usersList = new List<UserDB>();
            //First get all users
            this.GetAllUsers((allUsersResult) =>
            {
                if (WebResponse.isResultOk(allUsersResult))
                {
                    //Convert it from array to List to follow the method signature
                    string multipleUsersString = WebResponse.GetResponseInfo(allUsersResult);
                    UserDB[] usersArray = JsonManager.DeserializeFromJsonArray<UserDB>(multipleUsersString);

                    foreach (UserDB user in usersArray)
                        usersList.Add(user);

                    //HERE STARTS THE RECURSIVE PETITIONS
                    bool isAllOk = true;
                    int i = 0;
                    foreach (UserDB user in usersList)
                    {
                        this.GetUserInfoFromID((userInfoResult) =>
                        {
                            if (WebResponse.isResultOk(userInfoResult))
                            {
                                string userString = WebResponse.GetResponseInfo(userInfoResult);
                                UserDB auxUserDB = JsonManager.DeserializeFromJson<UserDB>(userString);

                                ExampleDBController.s_Instance.AddUsernameToList(auxUserDB);
                            }
                            else
                                isAllOk = false;

                            //Check if it's the last petition, and dispatch the end event of the global result
                            i++;
                            if (i == usersList.Count)
                            {
                                string generalResult = (isAllOk) ? WebResponse.OK : WebResponse.ERROR;
                                OnMultipleActionsEnd(generalResult);
                            }
                        }, user.UserID);
                    }
                }
            });
        }
        #endregion

        #region POST_Recursive

        #endregion
        #endregion

    }
}
