using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simple_DBImage
{
    /// <summary>
    /// This class makes all the calls to the php files which at the same time deals with DB queries petitions
    /// 
    /// !!!!!WARNING!!!!
    /// THIS SCRIPT REQUIRES THE Simple_WebRequest scripts!
    /// THIS SCRIPT REQUIRES THE Simple_ScreenRecorder scripts!
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
        public void GetImage(Action<string> OnSuccess = null)
        {
            string url = phpFilesServer + "GetImage.php";
            WebController.s_Instance.GetWebRequest(url, OnSuccess);
        }
        #endregion

        #region POST       
        public void PostNewImage(Action<string> OnSuccess = null, byte[] imageBytes = null)
        {
            string url = phpFilesServer + "GenerateNewImage.php";
            WebController.s_Instance.PostWebRequest(url, (result) => { print(result); }, new FormField("fileUpload", imageBytes));
        }        
        #endregion
        
    }
}
