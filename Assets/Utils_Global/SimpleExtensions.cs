﻿using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// This class collects all the extensions used to improve or facilitate the use of in-build methods
/// Remember to use this. to use extensions on monobehaviour scripts
/// </summary>
public static class SimpleExtensions
{
    #region MonoBehaviour
    //Invoke
    public static void Invoke(this MonoBehaviour mono, Action action, float delay)
    {
        mono.StartCoroutine(ExecuteAfterTime(action, delay));
    }
    private static IEnumerator ExecuteAfterTime(Action action, float delay)
    {
        yield return new WaitForSeconds(delay);
        action?.Invoke();
    }

    //Invoke Repeating
    public static void InvokeRepeating(this MonoBehaviour mono, Action action, float repeatRate = 1, float initialDelay = 0)
    {
        mono.StartCoroutine(ExecuteRepeatedlyAfterTime(action, repeatRate, initialDelay));
    }
    private static IEnumerator ExecuteRepeatedlyAfterTime(Action action, float repeatRate, float initialDelay)
    {
        yield return new WaitForSeconds(initialDelay);

        while (true)
        {
            action?.Invoke();
            yield return new WaitForSeconds(repeatRate);
        }
    }
    #endregion

    #region Enum
    //Short way to get enum int value as a string
    public static string toValueString<T>(this T enumVar) where T : IComparable, IFormattable, IConvertible
    {
        if (!typeof(T).IsEnum)
            throw new ArgumentException("cast requires enum type");
        return ((int)(object)enumVar).ToString();
    }
    #endregion

    #region Events
    public static void AddListener(this EventTrigger trigger, EventTriggerType eventType, System.Action<PointerEventData> listener)
    {
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = eventType;
        entry.callback.AddListener(data => listener.Invoke((PointerEventData)data));
        trigger.triggers.Add(entry);
    }
    #endregion

    #region Texture/Texture2D
    //Convert Texture2DToSprite
    public static Sprite ConvertTexture2DToSprite(this Texture2D texture)
    {
        return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
    }

    #region Rotate90   
    //Grab Texture2D and return the same texture rotated 90 degrees clockwise
    public static Texture2D RotateTexture90(Texture2D source, bool clockwise, bool affectOriginalResource = false)
    {
        Color32[] colorSource = source.GetPixels32();
        Color32[] colorResult = new Color32[colorSource.Length];

        int count = 0;
        int newWidth = source.height;
        int newHeight = source.width;
        int index = 0;

        for (int i = 0; i < source.width; i++)
        {
            for (int j = 0; j < source.height; j++)
            {
                if (clockwise)
                    index = (source.width * (j + 1)) - (i + 1);
                else
                    index = (source.width * (source.height - j)) - source.width + i;

                colorResult[count] = colorSource[index];
                count++;
            }
        }

        if (affectOriginalResource)
        {
            source.Resize(newWidth, newHeight);
            source.SetPixels32(colorResult);
            source.Apply();
            return source;
        }
        else
        {
            Texture2D rotatedTexture = new Texture2D(newWidth, newHeight);
            rotatedTexture.SetPixels32(colorResult);
            rotatedTexture.Apply();
            return rotatedTexture;
        }

    }
    //Grab Texture and return the same texture rotated 90 degrees clockwise
    public static Texture2D RotateTexture90(Texture originalTexture, bool clockwise, bool affectOriginalResource = false)
    {
        Texture2D source = (Texture2D)originalTexture;
        Color32[] colorSource = source.GetPixels32();
        Color32[] colorResult = new Color32[colorSource.Length];

        int count = 0;
        int newWidth = source.height;
        int newHeight = source.width;
        int index = 0;

        for (int i = 0; i < source.width; i++)
        {
            for (int j = 0; j < source.height; j++)
            {
                if (clockwise)
                    index = (source.width * (j + 1)) - (i + 1);
                else
                    index = (source.width * (source.height - j)) - source.width + i;

                colorResult[count] = colorSource[index];
                count++;
            }
        }

        if (affectOriginalResource)
        {
            source.Resize(newWidth, newHeight);
            source.SetPixels32(colorResult);
            source.Apply();
            return source;
        }
        else
        {
            Texture2D rotatedTexture = new Texture2D(newWidth, newHeight);
            rotatedTexture.SetPixels32(colorResult);
            rotatedTexture.Apply();
            return rotatedTexture;
        }

    }
    #endregion
    
    #endregion

    #region Color/Alpha
    public static float ToNormalizedAlphaValue(this float alphaValue)
    {
        if (alphaValue > 1)
            alphaValue /= 255;
        if (alphaValue < 0)
            alphaValue = 0;
        return alphaValue;
    }

    public static void SetAlpha(this RawImage rawImage, float alphaValue)
    {
        Color auxColor = rawImage.color;
        auxColor.a = alphaValue.ToNormalizedAlphaValue();
        rawImage.color = auxColor;
    }
    public static void SetAlpha(this Image image, float alphaValue)
    {
        Color auxColor = image.color;
        auxColor.a = alphaValue.ToNormalizedAlphaValue();
        image.color = auxColor;
    }
    #endregion

    #region Mathfx
    //Ease in out
    public static float Hermite(float start, float end, float value)
    {
        return Mathf.Lerp(start, end, value * value * (3.0f - 2.0f * value));
    }

    public static Vector2 Hermite(Vector2 start, Vector2 end, float value)
    {
        return new Vector2(Hermite(start.x, end.x, value), Hermite(start.y, end.y, value));
    }

    public static Vector3 Hermite(Vector3 start, Vector3 end, float value)
    {
        return new Vector3(Hermite(start.x, end.x, value), Hermite(start.y, end.y, value), Hermite(start.z, end.z, value));
    }

    //Ease out
    public static float Sinerp(float start, float end, float value)
    {
        return Mathf.Lerp(start, end, Mathf.Sin(value * Mathf.PI * 0.5f));
    }

    public static Vector2 Sinerp(Vector2 start, Vector2 end, float value)
    {
        return new Vector2(Mathf.Lerp(start.x, end.x, Mathf.Sin(value * Mathf.PI * 0.5f)), Mathf.Lerp(start.y, end.y, Mathf.Sin(value * Mathf.PI * 0.5f)));
    }

    public static Vector3 Sinerp(Vector3 start, Vector3 end, float value)
    {
        return new Vector3(Mathf.Lerp(start.x, end.x, Mathf.Sin(value * Mathf.PI * 0.5f)), Mathf.Lerp(start.y, end.y, Mathf.Sin(value * Mathf.PI * 0.5f)), Mathf.Lerp(start.z, end.z, Mathf.Sin(value * Mathf.PI * 0.5f)));
    }
    //Ease in
    public static float Coserp(float start, float end, float value)
    {
        return Mathf.Lerp(start, end, 1.0f - Mathf.Cos(value * Mathf.PI * 0.5f));
    }

    public static Vector2 Coserp(Vector2 start, Vector2 end, float value)
    {
        return new Vector2(Coserp(start.x, end.x, value), Coserp(start.y, end.y, value));
    }

    public static Vector3 Coserp(Vector3 start, Vector3 end, float value)
    {
        return new Vector3(Coserp(start.x, end.x, value), Coserp(start.y, end.y, value), Coserp(start.z, end.z, value));
    }

    //Boing
    public static float Berp(float start, float end, float value)
    {
        value = Mathf.Clamp01(value);
        value = (Mathf.Sin(value * Mathf.PI * (0.2f + 2.5f * value * value * value)) * Mathf.Pow(1f - value, 2.2f) + value) * (1f + (1.2f * (1f - value)));
        return start + (end - start) * value;
    }

    public static Vector2 Berp(Vector2 start, Vector2 end, float value)
    {
        return new Vector2(Berp(start.x, end.x, value), Berp(start.y, end.y, value));
    }

    public static Vector3 Berp(Vector3 start, Vector3 end, float value)
    {
        return new Vector3(Berp(start.x, end.x, value), Berp(start.y, end.y, value), Berp(start.z, end.z, value));
    }

    //Like lerp with ease in ease out
    public static float SmoothStep(float x, float min, float max)
    {
        x = Mathf.Clamp(x, min, max);
        float v1 = (x - min) / (max - min);
        float v2 = (x - min) / (max - min);
        return -2 * v1 * v1 * v1 + 3 * v2 * v2;
    }

    public static Vector2 SmoothStep(Vector2 vec, float min, float max)
    {
        return new Vector2(SmoothStep(vec.x, min, max), SmoothStep(vec.y, min, max));
    }

    public static Vector3 SmoothStep(Vector3 vec, float min, float max)
    {
        return new Vector3(SmoothStep(vec.x, min, max), SmoothStep(vec.y, min, max), SmoothStep(vec.z, min, max));
    }

    public static float Lerp(float start, float end, float value)
    {
        return ((1.0f - value) * start) + (value * end);
    }

    public static Vector3 NearestPoint(Vector3 lineStart, Vector3 lineEnd, Vector3 point)
    {
        Vector3 lineDirection = Vector3.Normalize(lineEnd - lineStart);
        float closestPoint = Vector3.Dot((point - lineStart), lineDirection);
        return lineStart + (closestPoint * lineDirection);
    }

    public static Vector3 NearestPointStrict(Vector3 lineStart, Vector3 lineEnd, Vector3 point)
    {
        Vector3 fullDirection = lineEnd - lineStart;
        Vector3 lineDirection = Vector3.Normalize(fullDirection);
        float closestPoint = Vector3.Dot((point - lineStart), lineDirection);
        return lineStart + (Mathf.Clamp(closestPoint, 0.0f, Vector3.Magnitude(fullDirection)) * lineDirection);
    }

    //Bounce
    public static float Bounce(float x)
    {
        return Mathf.Abs(Mathf.Sin(6.28f * (x + 1f) * (x + 1f)) * (1f - x));
    }

    public static Vector2 Bounce(Vector2 vec)
    {
        return new Vector2(Bounce(vec.x), Bounce(vec.y));
    }

    public static Vector3 Bounce(Vector3 vec)
    {
        return new Vector3(Bounce(vec.x), Bounce(vec.y), Bounce(vec.z));
    }

    // test for value that is near specified float (due to floating point inprecision)
    // all thanks to Opless for this!
    public static bool Approx(float val, float about, float range)
    {
        return ((Mathf.Abs(val - about) < range));
    }

    // test if a Vector3 is close to another Vector3 (due to floating point inprecision)
    // compares the square of the distance to the square of the range as this 
    // avoids calculating a square root which is much slower than squaring the range
    public static bool Approx(Vector3 val, Vector3 about, float range)
    {
        return ((val - about).sqrMagnitude < range * range);
    }

    /*
      * CLerp - Circular Lerp - is like lerp but handles the wraparound from 0 to 360.
      * This is useful when interpolating eulerAngles and the object
      * crosses the 0/360 boundary.  The standard Lerp function causes the object
      * to rotate in the wrong direction and looks stupid. Clerp fixes that.
      */
    public static float Clerp(float start, float end, float value)
    {
        float min = 0.0f;
        float max = 360.0f;
        float half = Mathf.Abs((max - min) / 2.0f);//half the distance between min and max
        float retval = 0.0f;
        float diff = 0.0f;

        if ((end - start) < -half)
        {
            diff = ((max - start) + end) * value;
            retval = start + diff;
        }
        else if ((end - start) > half)
        {
            diff = -((max - end) + start) * value;
            retval = start + diff;
        }
        else retval = start + (end - start) * value;

        // Debug.Log("Start: "  + start + "   End: " + end + "  Value: " + value + "  Half: " + half + "  Diff: " + diff + "  Retval: " + retval);
        return retval;
    }
    #endregion
}