using System;
using UnityEngine;

[Serializable]
public class JsonObjectModel
{
    [Serializable]
    public struct JsonStruct
    {
        [SerializeField] public int firstVariable;
        [SerializeField] public int secondVariable;
    }

    [SerializeField] public int intVariable;
    [SerializeField] public float floatVariable;
    [SerializeField] public string stringVariable;
    [SerializeField] public Vector2 vector2Variable = new Vector2(0,0);
    [SerializeField] public JsonStruct jsonStructVariable;
    [SerializeField] public JsonStruct[] arraytVariable;
}
