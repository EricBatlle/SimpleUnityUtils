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
        [Header("See First Image")]
        [SerializeField] private Button seeFirstImageBtn = null;
        [SerializeField] private RawImage displayImage = null;
        [Header("See All Images")]
        [SerializeField] private Button seeAllImagesBtn = null;
        [SerializeField] private GameObject displayImagePrefab = null;
        [SerializeField] private GameObject seeAllImagesLayout = null;
        [Header("Debug")]
        [SerializeField] private Text debugInformationTxt = null;

        private void Awake()
        {
            //When the ScreenRecorder takes the screenshot, call SaveImgOnDB method
            ScreenRecorder.s_Instance.OnTakeScreenshot = SaveImgOnDB;

            takeScreenshotBtn.onClick.AddListener(ScreenRecorder.s_Instance.TakeScreenshot);
            seeFirstImageBtn.onClick.AddListener(SeeFirstImageSavedOnDB);
            seeAllImagesBtn.onClick.AddListener(SeeAllImagesSavedOnDB);
        }
        
        private void SeeFirstImageSavedOnDB()
        {
            DBController.s_Instance.GetImage((result) => 
            {                
                if (WebResponse.isResultOk(result))
                {
                    debugInformationTxt.text = "Displaying Image";
                    //Deserialize image
                    string imageDataResult = WebResponse.GetResponseInfo(result);
                    ImageDB imageDB = JsonManager.DeserializeFromJson<ImageDB>(imageDataResult);

                    //Create texture and assign it to the displayImage
                    Texture2D tex = new Texture2D(2, 2);
                    tex.LoadImage(imageDB.ImageDataBytes);                    
                    displayImage.texture = tex;
                }
                else
                    debugInformationTxt.text = "There is no Image saved yet on DB";
            });
        }

        private void SeeAllImagesSavedOnDB()
        {
            DBController.s_Instance.GetAllImages((result) => 
            {
                if(WebResponse.isResultOk(result))
                {
                    debugInformationTxt.text = "Displaying All Images";
                    //Deserialize all images
                    string imagesDataResult = WebResponse.GetResponseInfo(result);
                    ImageDB[] imagesDB = JsonManager.DeserializeFromJsonArray<ImageDB>(imagesDataResult);
                    
                    //Clean the matrix of displayImages
                    foreach(Transform child in seeAllImagesLayout.transform)
                    {
                        Destroy(child.gameObject);
                    }
                    //Create the matrix of displayImages
                    foreach (ImageDB imageDB in imagesDB)
                    {
                        //Create texture 
                        Texture2D tex = new Texture2D(2, 2);
                        tex.LoadImage(imageDB.ImageDataBytes);
                        //Create displayImage and assign the texture
                        GameObject newDisplayImage = Instantiate(displayImagePrefab);
                        newDisplayImage.GetComponent<RawImage>().texture = tex;
                        //Assign displayImage to the imagesLayout
                        newDisplayImage.transform.SetParent(this.seeAllImagesLayout.transform);
                    }
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
