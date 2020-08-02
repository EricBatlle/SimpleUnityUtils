using UnityEngine;
using UnityEngine.UI;

public static class UIExtensions
{
    //Get mouse position in world space coordinates using canvas Screen Space - Camera
    public static Vector3 GetMouseWorldPositionInScreenSpaceCamera(this Canvas cameraSpaceCanvas, Camera camera = null)
    {
        var screenPoint = Input.mousePosition;
        screenPoint.z = cameraSpaceCanvas.planeDistance;
        if (camera == null)
            return Camera.main.ScreenToWorldPoint(screenPoint);
        else
            return camera.ScreenToWorldPoint(screenPoint);
    }

    //Get bounds of rectTransform in screenSpace coordinates
    public static Rect GetScreenSpaceRectTransformBoundaries(this RectTransform rt, Camera camera)
    {
        // getting the world corners
        var corners = new Vector3[4];
        rt.GetWorldCorners(corners);

        // getting the screen corners
        for (var i = 0; i < corners.Length; i++)
            corners[i] = camera.WorldToScreenPoint(corners[i]);

        // getting the top left position of the transform
        var position = (Vector2)corners[1];
        // inverting the y axis values, making the top left corner = 0.
        position.y = Screen.height - position.y;
        // calculate the size, width and height, in pixle format
        var size = corners[2] - corners[0];

        return new Rect(position, size);
    }

    public static bool IsPointInRectTransform(this RectTransform rt, Vector2 point)
    {
        // Get the rectangular bounding box of your UI element
        Rect rect = rt.rect;

        // Get the left, right, top, and bottom boundaries of the rect
        float leftSide = rt.anchoredPosition.x - rect.width / 2;
        float rightSide = rt.anchoredPosition.x + rect.width / 2;
        float topSide = rt.anchoredPosition.y + rect.height / 2;
        float bottomSide = rt.anchoredPosition.y - rect.height / 2;

        //Debug.Log(leftSide + ", " + rightSide + ", " + topSide + ", " + bottomSide);

        // Check to see if the point is in the calculated bounds
        if (point.x >= leftSide &&
            point.x <= rightSide &&
            point.y >= bottomSide &&
            point.y <= topSide)
        {
            return true;
        }
        return false;
    }

    //Check if Rect fully contains another rect
    public static bool Contains(this Rect rect, Rect anotherRect)
    {
        if ((anotherRect.xMax - rect.xMax) > 0 ||
            (anotherRect.xMin - rect.xMin) < 0 ||
            (anotherRect.yMax - rect.yMax) > 0 ||
            (anotherRect.yMin - rect.yMin) < 0)
        {
            return false;
        }
        else
            return true;
    }

    #region Shorter way to get/set RectTranform Height and Width from canvas and rectTransform itself
    //Get W/H from Canvas/RT
    public static float LocalSpaceHeight(this Canvas canvas)
    {
        return canvas.GetComponent<RectTransform>().rect.height;
    }
    public static float LocalSpaceWidth(this Canvas canvas)
    {
        return canvas.GetComponent<RectTransform>().rect.width;
    }
    public static float LocalSpaceHeight(this RectTransform rectTransform)
    {
        return rectTransform.rect.height;
    }
    public static float LocalSpaceWidth(this RectTransform rectTransform)
    {
        return rectTransform.rect.width;
    }
    //Set W/H to Canvas/RT
    public static void SetLocalSpaceHeight(this Canvas canvas, float height)
    {
        canvas.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
    }
    public static void SetLocalSpaceWidth(this Canvas canvas, float width)
    {
        canvas.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
    }
    public static void SetLocalSpaceHeight(this RectTransform rectTransform, float height)
    {
        rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
    }
    public static void SetLocalSpaceWidth(this RectTransform rectTransform, float width)
    {
        rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
    }
    #endregion

    #region Set/GetRectTransform Left-Right-Top-Bottom values
    public static void SetLeft(this RectTransform rt, float left)
    {
        rt.offsetMin = new Vector2(left, rt.offsetMin.y);
    }
    public static float GetLeft(this RectTransform rt)
    {
        return rt.offsetMin.x;
    }

    public static void SetRight(this RectTransform rt, float right)
    {
        rt.offsetMax = new Vector2(-right, rt.offsetMax.y);
    }
    public static float GetRight(this RectTransform rt)
    {
        return -rt.offsetMax.x;
    }

    public static void SetTop(this RectTransform rt, float top)
    {
        rt.offsetMax = new Vector2(rt.offsetMax.x, -top);
    }
    public static float GetTop(this RectTransform rt)
    {
        return -rt.offsetMax.y;
    }

    public static void SetBottom(this RectTransform rt, float bottom)
    {
        rt.offsetMin = new Vector2(rt.offsetMin.x, bottom);
    }
    public static float GetBottom(this RectTransform rt)
    {
        return rt.offsetMin.y;
    }
    #endregion

    #region ScrollRect
    public static void ScrollToTop(this ScrollRect scrollRect)
    {
        scrollRect.normalizedPosition = new Vector2(0, 1);
    }
    public static void ScrollToBottom(this ScrollRect scrollRect)
    {
        scrollRect.normalizedPosition = new Vector2(0, 0);
    }
    #endregion

}
