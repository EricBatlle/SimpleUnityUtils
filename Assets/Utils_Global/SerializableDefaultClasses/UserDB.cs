using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Simple_DB
{
    [Serializable]
    public class UserDB : User
    {
        [SerializeField] private int userID = 0;
        public int UserID
        {
            get { return userID; }
            set //Not called if change property from editor
            {
                userID = value;
                //Do stuff with the new value...
            }
        }
        public UserDB(int userID, string username = "username", string password = "password", string email = "a@b.c") : base(username = "username", password = "password", email = "a@b.c")
        {
            this.userID = userID;
        }
    }
}
