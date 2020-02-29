using System;
using System.Collections;
using UnityEngine;
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

    #region Color
    public static void SetAlpha(this RawImage rawImage, float alphaValue)
    {
        if (alphaValue > 1)
            alphaValue /= 255;
        if (alphaValue < 0)
            alphaValue = 0;

        Color auxColor = rawImage.color;
        auxColor.a = alphaValue;
        rawImage.color = auxColor;
    }
    public static void SetAlpha(this Image image, float alphaValue)
    {
        if (alphaValue > 1)
            alphaValue /= 255;
        if (alphaValue < 0)
            alphaValue = 0;

        Color auxColor = image.color;
        auxColor.a = alphaValue;
        image.color = auxColor;
    }
    #endregion
}