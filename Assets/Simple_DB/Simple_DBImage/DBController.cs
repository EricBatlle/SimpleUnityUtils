using System;
using UnityEngine;

namespace Simple_DBImage
{
    /// <summary>
    /// This class makes all the calls to the php files which at the same time deals with DB queries petitions
    /// 
    /// !!!!!WARNING!!!!
    /// THIS SCRIPT REQUIRES THE Simple_WebRequest scripts!
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
        //Get the last image saved on the DB
        public void GetImage(Action<string> OnSuccess = null)
        {
            string url = phpFilesServer + "GetImage.php";
            WebController.s_Instance.GetWebRequest(url, OnSuccess);
        }
        public void GetAllImages(Action<string> OnSuccess = null)
        {
            string url = phpFilesServer + "GetAllImages.php";
            WebController.s_Instance.GetWebRequest(url, OnSuccess);
        }

        #endregion

        #region POST       
        //Post new image to the DB
        public void PostNewImage(Action<string> OnSuccess = null, byte[] imageBytes = null)
        {
            string url = phpFilesServer + "GenerateNewImage.php";
            WebController.s_Instance.PostWebRequest(url, OnSuccess, new FormField("fileUpload", imageBytes));
        }        
        #endregion        
    }
}
