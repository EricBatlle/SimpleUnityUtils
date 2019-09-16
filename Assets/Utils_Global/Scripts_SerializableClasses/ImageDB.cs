using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ImageDB
{
    #region Variables
    [SerializeField] private int imageID = 0;
    public int ImageID
    {
        get { return imageID; }
        set //Not called if change property from editor
        {
            imageID = value;
            //Do stuff with the new value...
        }
    }
    [SerializeField] private string name = "username";
    public string Name
    {
        get { return name; }
        set //Not called if change property from editor
        {
            name = value;
            //Do stuff with the new value...
        }
    }
    [SerializeField] private string imageData = null;
    public string ImageData
    {
        get { return imageData; }
        set //Not called if change property from editor
        {
            imageData = value;
            //Do stuff with the new value...
        }
    }
    [SerializeField] private byte[] imageDataBytes = null;
    public byte[] ImageDataBytes
    {
        get { return System.Convert.FromBase64String(imageData); }
        set //Not called if change property from editor
        {
            imageDataBytes = value;
            //Do stuff with the new value...
        }
    }

    #endregion

    public ImageDB(int imageID = 0, string name = "imageName", string imageDataString = null)
    {
        this.imageID = imageID;
        this.name = name;
        this.imageData = imageDataString;
    }
}
