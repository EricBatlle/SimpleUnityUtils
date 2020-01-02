using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[ExecuteInEditMode]
public class BreathEffect : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tmp = null;
    [SerializeField] private float effectSpeed = 0.02f;
    [SerializeField] private float delayEffect = 0.02f;

    private IEnumerator breathCoroutine = null;
    private float glowPower = 0;
    private bool isBreathing = true;
    private static float GLOW_MAX = 1;
    private static float GLOW_MIN = 0;
    
    private void OnEnable()
    {
        breathCoroutine = EffectLoop();
        StartEffect();
    }

    private void OnDisable()
    {
        StopEffect();
        breathCoroutine = null;
    }

    public void StartEffect()
    {
        StartCoroutine(breathCoroutine);
    }
    public void StopEffect()
    {
        StopCoroutine(breathCoroutine);
    }
    
    private IEnumerator EffectLoop()
    {
        while(true)
        {
            glowPower += (isBreathing) ? effectSpeed : -effectSpeed;

            if (isBreathing)            
                //Increment
                isBreathing = (glowPower >= GLOW_MAX) ? false : true;            
            else if (!isBreathing)            
                //Decrement
                isBreathing = (glowPower <= GLOW_MIN) ? true : false;
            
            //Assign glow power
            tmp.fontSharedMaterial.SetFloat(ShaderUtilities.ID_GlowPower, glowPower);
            yield return new WaitForSeconds(delayEffect);
        }
    }
}
