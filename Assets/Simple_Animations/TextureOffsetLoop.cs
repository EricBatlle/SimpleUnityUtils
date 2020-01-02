using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureOffsetLoop : MonoBehaviour
{
    public bool GoLoop
    {
        get { return goLoop; }
        set
        {
            goLoop = value;
            if (value == true)
                StartCoroutine(LoopCoroutine);
            else if (value == false)
                StopCoroutine(LoopCoroutine);
        }
    }
    [SerializeField] private bool goLoop = false;
    [SerializeField] private float offsetIncrement = 5f;
    private Material material;
    private IEnumerator LoopCoroutine = null;

    private void Awake()
    {
        material = this.GetComponent<MeshRenderer>().material;
        LoopCoroutine = OffsetCoroutine();
        StartCoroutine(LoopCoroutine);
    }    

    private void IncrementOffsetY()
    {
        material.mainTextureOffset = new Vector2(material.mainTextureOffset.x, material.mainTextureOffset.y + offsetIncrement);
    }

    private IEnumerator OffsetCoroutine()
    {
        while(goLoop)
        {
            IncrementOffsetY();
            yield return null;
        }
    }
}
