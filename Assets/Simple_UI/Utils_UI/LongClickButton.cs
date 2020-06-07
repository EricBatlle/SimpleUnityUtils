using System;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Remember that this script should be attached to a GameObject that can be raycasted!
/// </summary>
public class LongClickButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private float requiredHoldTime = 2f;    
    private bool pointerDown;
    private float pointerDownTimer;
    private Action OnLongClick = null;
    
    public void OnPointerDown(PointerEventData eventData)
    {
        pointerDown = true;
        Debug.Log("OnPointerDown");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Reset();
        Debug.Log("OnPointerUp");
    }

    private void Update()
    {
        if (pointerDown)
        {
            pointerDownTimer += Time.deltaTime;
            if (pointerDownTimer >= requiredHoldTime)
            {
                OnLongClick?.Invoke();
                Reset();
            }
        }
    }

    private void Reset()
    {
        pointerDown = false;
        pointerDownTimer = 0;
    }    
}