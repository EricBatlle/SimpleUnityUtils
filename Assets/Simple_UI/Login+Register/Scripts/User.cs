using System;
using UnityEngine;

[Serializable]
public class User
{
    #region Variables
    [SerializeField] private string username = "username";
    public string Username
    {
        get{return username;}
        set //Not called if change property from editor
        {
            username = value;
            //Do stuff with the new value...
        }
    }
    [SerializeField] private string password = "pass";
    public string Password
    {
        get{return password; }
        set //Not called if change property from editor
        {
            password = value;
            //Do stuff with the new value...
        }
    }
    [SerializeField] private string email = "a@b.c";
    public string Email
    {
        get{return email; }
        set //Not called if change property from editor
        {
            email = value;
            //Do stuff with the new value...
        }
    }
    #endregion

    public User(string username = "username", string password = "password", string email = "a@b.c")
    {
        this.username = username;
        this.password = password;
        this.email = email;
    }
}
