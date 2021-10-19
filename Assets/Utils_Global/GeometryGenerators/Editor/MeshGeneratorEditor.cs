using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MeshGenerator))]
public class MeshGeneratorEditor : Editor
{
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();
		MeshGenerator meshGeneratorScript = (MeshGenerator)target;

		GUILayout.BeginHorizontal();

		GUILayout.BeginVertical();
		if (GUILayout.Button("Build Mesh"))
			meshGeneratorScript.GenerateMesh();
		if (GUILayout.Button("Save Mesh"))
			MeshSaverEditor.SaveMeshInPlace(meshGeneratorScript.GetComponent<MeshFilter>());
		GUILayout.EndVertical();

		GUILayout.BeginVertical();
		if (GUILayout.Button("Build GameObject Mesh"))
			meshGeneratorScript.GenerateNewGOMesh();
		if (GUILayout.Button("Save GO Mesh"))
			MeshSaverEditor.SaveMeshInPlace(meshGeneratorScript.generatedGO.GetComponent<MeshFilter>());
		GUILayout.EndVertical();

		GUILayout.EndHorizontal();

		EditorGUILayout.Space();
		if (GUILayout.Button("Clear Generator"))
			meshGeneratorScript.ClearGenerator();

	}
}