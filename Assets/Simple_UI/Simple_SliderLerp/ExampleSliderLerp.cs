using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExampleSliderLerp : MonoBehaviour
{
    [Header("Slider Simple Example")]
    [SerializeField] private SliderLerpComponent sliderLerp = null;
    [SerializeField] private Button buttonMax = null;
    [SerializeField] private Button buttonMin = null;
    [Header("Slider Menu Example")]
    [SerializeField] private SliderLerpComponent sliderLerpMenu = null;
    [SerializeField] private List<Button> menuButtons = new List<Button>();

    private void Start()
    {
        for(int i = 0; i < menuButtons.Count; i++)        
            menuButtons[i].onClick.AddListener(()=> { sliderLerpMenu.LerpToValue((float)i); });

        buttonMax.onClick.AddListener(() => {sliderLerp.LerpToValue(sliderLerp.GetMaxValue());});
        buttonMin.onClick.AddListener(() => {sliderLerp.LerpToValue(sliderLerp.GetMinValue());});
    }
}
