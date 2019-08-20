using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderLerpComponent : MonoBehaviour
{
    private Slider sliderComp;
    private float actualValue = 0;
    private float interpolation = 0.5f;

    private void Start()
    {
        sliderComp = GetComponent<Slider>();
    }

    private void FixedUpdate()
    {
        if (actualValue != sliderComp.value)        
            sliderComp.value = Mathf.Lerp(sliderComp.value, actualValue, interpolation);        
    }

    public void LerpToValue(float value)
    {
        actualValue = value;
    }

    public float GetMaxValue()
    {
        return sliderComp.maxValue;
    }
    public float GetMinValue()
    {
        return sliderComp.minValue;
    }
}
