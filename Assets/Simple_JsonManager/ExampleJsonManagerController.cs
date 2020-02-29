using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExampleJsonManagerController : MonoBehaviour
{
    [Header("JsonFiles")]
    public TextAsset jsonObjectJSON = null;
    public TextAsset jsonArrayJSON = null;
    [Header("Serialized Variables")]
    public JsonObjectModel jsonObjectModel = null;
    public JsonArrayModel jsonArrayModel = null;
    [Header("UI Elements")]
    public Text textComponent = null;

    public void DeserializeObject()
    {
        this.jsonObjectModel = JsonManager.DeserializeFromJson<JsonObjectModel>(jsonObjectJSON.text);
        textComponent.text = "JsonObject deserialized into jsonObjectModel variable inside ExampleController_JsonManager (You can see it on inspector!)";
    }
    public void DeserializeArray()
    {
        this.jsonArrayModel.jsonObjectModels = JsonManager.DeserializeFromJsonArray<JsonObjectModel>(jsonArrayJSON.text);
        textComponent.text = "JsonArray deserialized into jsonArrayModel variable inside ExampleController_JsonManager (You can see it on inspector!)";
    }
    public void SerializeObject()
    {
        textComponent.text = "JsonObject serialized result is:" + '\n' + '\n';
        textComponent.text += JsonManager.SerializeToJson<JsonObjectModel>(this.jsonObjectModel);
    }
    public void SerializeArray()
    {
        textComponent.text = "JsonArray serialized result is:" + '\n' + '\n';
        textComponent.text += JsonManager.SerializeToJson<JsonArrayModel>(this.jsonArrayModel);
    }
}
