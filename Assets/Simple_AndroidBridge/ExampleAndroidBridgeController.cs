using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExampleAndroidBridgeController : AndroidBridge
{
    [SerializeField] private Button startListeningBtn = null;
    [SerializeField] private Toggle continuousListeningTgle = null;
    [SerializeField] private Text resultsTxt = null;

    void Start()
    {
        this.SetAndroidBridgeName();

        startListeningBtn.onClick.AddListener(StartListening);
        continuousListeningTgle.onValueChanged.AddListener(SetContinuousListening);
    }

    private void StartListening()
    {
        AndroidRunnableCall("StartListening");
    }

    private void SetContinuousListening(bool isContinuous)
    {
        AndroidCall("SetContinuousListening", isContinuous);
    }
    
    protected override void OnGetResults(string[] results)
    {
        resultsTxt.text = "";
        for (int i = 0; i < results.Length; i++)
        {
            resultsTxt.text += results[i] + '\n';
        }
    }
}
