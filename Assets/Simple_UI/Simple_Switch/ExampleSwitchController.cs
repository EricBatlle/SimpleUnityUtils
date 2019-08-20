using UnityEngine;

public class ExampleSwitchController : MonoBehaviour
{
    [SerializeField] private SwitchComponent switchComponent = null;
    [SerializeField] private SwitchComponent switchComponent_big = null;

    private void Start()
    {
        if(switchComponent != null)
            this.switchComponent.OnValueSwapTo = (isEnable) => { print("SMALL switchComponent state is: "+ isEnable); };

        if(switchComponent_big != null)
            this.switchComponent_big.OnValueSwapTo = (isEnable) => { print("BIG switchComponent state is: "+isEnable); };
    }    
}
