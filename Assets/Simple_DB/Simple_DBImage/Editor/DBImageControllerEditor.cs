using Simple_DBImage;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DBController))]
public class DBControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        DBController dbController = (DBController)target;

        #region Database functionalities        
        if (GUILayout.Button("Clean DB"))
            dbController.CleanDB();
        GUILayout.Space(20f);        
        #endregion
    }

}
