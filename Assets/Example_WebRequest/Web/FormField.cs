using System;
using UnityEngine;

/// <summary>
/// This class is serialized and sended through POST like form variable
/// </summary>
[Serializable]
public class FormField
{
    private bool m_isBinaryField = false;
    [SerializeField] public string fieldName = "fieldName";
    [SerializeField] public string value = "value";
    [SerializeField] public byte[] valueInBytes = null;

    #region Constructors
    //Constructor for non-binary Forms
    public FormField(string fieldName = "fieldName", string value = "value")
    {
        this.m_isBinaryField = false;

        this.fieldName = fieldName;
        this.value = value;
        this.valueInBytes = null; //could be, System.Text.Encoding.UTF8.GetBytes(value);, but performance
    }
    //Constructor for binary Forms
    public FormField(string fieldName = "fieldNAME", byte[] valueInBytes = null)
    {
        this.m_isBinaryField = true;

        this.fieldName = fieldName;
        this.value = null; // could be, System.Text.Encoding.UTF8.GetString(valueInBytes);, but performance
        this.valueInBytes = valueInBytes;
    }
    #endregion
    //Returns if the form is binary or not
    public bool IsBinaryField()
    {
        return m_isBinaryField;
    }
}