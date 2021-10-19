using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(QuadGenerator))]
public class QuadGeneratorEditor : Editor
{
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();
		QuadGenerator quadGeneratorScript = (QuadGenerator)target;

		GUILayout.BeginHorizontal();

		GUILayout.BeginVertical();
		if (GUILayout.Button("Build Quad"))
			quadGeneratorScript.GenerateQuad();
		if (GUILayout.Button("Save Quad"))
			MeshSaverEditor.SaveMeshInPlace(quadGeneratorScript.GetComponent<MeshFilter>());
		GUILayout.EndVertical();

		GUILayout.BeginVertical();
		if (GUILayout.Button("Build GameObject Quad"))
			quadGeneratorScript.GenerateNewGOQuad();
		if (GUILayout.Button("Save GO Quad"))
			MeshSaverEditor.SaveMeshInPlace(quadGeneratorScript.generatedGO.GetComponent<MeshFilter>());
		GUILayout.EndVertical();

		GUILayout.EndHorizontal();

		EditorGUILayout.Space();
		if (GUILayout.Button("Clear Generator"))
			quadGeneratorScript.ClearGenerator();
	}
}