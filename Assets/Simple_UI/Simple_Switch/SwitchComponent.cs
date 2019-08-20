using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchComponent : MonoBehaviour
{
    public Action<bool> OnValueSwapTo = null;

    private Slider slider = null;    
    [SerializeField] private bool isEnable = true;
    [SerializeField] private Color enableColor = Color.white;
    [SerializeField] private Color disableColor = Color.black;
    private float newValue = 0;

    private void Awake()
    {
        slider = GetComponent<Slider>();
        slider.handleRect.gameObject.GetComponent<Button>().onClick.AddListener(OnKnotPressed);
    }

    private void OnKnotPressed()
    {
        newValue = (slider.value == 0) ? 1 : 0;
        isEnable = (newValue == 0) ? true : false;
        slider.value = newValue;
        ChangeStyle();

        OnValueSwapTo?.Invoke(isEnable);//Trigger all actions stacked on that event
    }

    private void ChangeStyle()
    {
        slider.fillRect.gameObject.GetComponent<Image>().color = (isEnable) ? enableColor : disableColor;
        slider.handleRect.gameObject.GetComponent<Image>().color = (isEnable) ? enableColor : disableColor;
    }    
}
