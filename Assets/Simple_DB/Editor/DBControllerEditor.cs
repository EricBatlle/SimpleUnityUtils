using Simple_DB;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DBController))]
public class DBControllerEditor : Editor
{
    [Header("Post New User")]
    [SerializeField] public string username = "defaultUserName";
    [SerializeField] public string password = "defaultPassword";
    [SerializeField] public string email = "default@email.com";

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        DBController dbController = (DBController)target;

        #region Database functionalities 
        //Post New User
        username = EditorGUILayout.TextField("Username:", username);
        password = EditorGUILayout.TextField("Password:", password);
        email = EditorGUILayout.TextField("Email:", email);

        if (GUILayout.Button("PostUser"))
            dbController.PostNewUser((result) => { Debug.Log("Posted New User: " + result); }, new User(username, password, email));
        GUILayout.Space(20f);

        if (GUILayout.Button("Clean DB"))
            dbController.CleanDB();
        GUILayout.Space(20f);        
        #endregion
    }

}
