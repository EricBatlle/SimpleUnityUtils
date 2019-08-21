using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlexibleGridLayoutGroup : MonoBehaviour
{
    [SerializeField] private int rows = 0;
    [SerializeField] private int cols = 0;
    private RectTransform parentRect = null;
    private GridLayoutGroup gridLayout = null;

    //This behaviour should be on START/ONENABLE, can't be AWAKE or UI will broke
    void OnEnable()
    {
        //Reescalate gridLayout size
        parentRect = gameObject.GetComponent<RectTransform>();
        gridLayout = gameObject.GetComponent<GridLayoutGroup>();
        gridLayout.cellSize = new Vector2(parentRect.rect.width / cols, parentRect.rect.height / rows);
    }
}
