using UnityEngine;
using UnityEngine.UI;

namespace Simple_DBImage
{
    /// <summary>
    /// This class is the controller of the DBImage example
    /// That example shows how to load and show images to and from DB
    /// 
    /// !!!!!WARNING!!!!
    /// THIS SCRIPT REQUIRES THE Simple_WebRequest scripts!
    /// THIS SCRIPT REQUIRES THE Simple_ScreenRecorder scripts!
    /// </summary>
    public class ExampleDBImageController : Singleton<ExampleDBImageController>
    {
        [SerializeField] private Button takeScreenshotBtn = null;
        [SerializeField] private Button seeLastImageBtn = null;
        [SerializeField] private RawImage displayImage = null;
        [Header("Debug")]
        [SerializeField] private Text debugInformationTxt = null;

        private void Awake()
        {
            //When the ScreenRecorder takes the screenshot, call SaveImgOnDB method
            ScreenRecorder.s_Instance.OnTakeScreenshot = SaveImgOnDB;

            takeScreenshotBtn.onClick.AddListener(ScreenRecorder.s_Instance.TakeScreenshot);
            seeLastImageBtn.onClick.AddListener(SeeLastImageSavedOnDB);
        }
        
        private void SeeLastImageSavedOnDB()
        {
            DBController.s_Instance.GetImage((result) => 
            {                
                if (WebResponse.isResultOk(result))
                {
                    debugInformationTxt.text = "Displaying Image";
                    string imageDataResult = WebResponse.GetResponseInfo(result);
                    byte[] imageBytes = System.Convert.FromBase64String(imageDataResult);
                    Texture2D tex = new Texture2D(2, 2);
                    tex.LoadImage(imageBytes);
                    displayImage.texture = tex;
                }
                else
                    debugInformationTxt.text = "There is no Image saved yet on DB";
            });
        }

        private void SaveImgOnDB(byte[] imageData)
        {
            DBController.s_Instance.PostNewImage((result) => 
            {
                if (WebResponse.isResultOk(result))
                    debugInformationTxt.text = "Image Saved on DB";
                else
                    debugInformationTxt.text = result;

            }, imageData);
        }
    }
}
