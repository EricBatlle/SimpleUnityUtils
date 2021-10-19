using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class QuadGenerator : MonoBehaviour
{
	public float width = 1;
	public float depth = 1;

	private Mesh mesh = null;
	[DisplayWithoutEdit] public GameObject generatedGO = null;

	public void GenerateQuad()
	{
		Vector3[] vertices = new Vector3[4]
		{
			new Vector3(0, 0, 0),
			new Vector3(width, 0, 0),
			new Vector3(0, 0, depth),
			new Vector3(width, 0, depth)
		};

		int[] triangles = new int[6]
		{
            // lower left triangle
            0, 2, 1,
            // upper right triangle
            2, 3, 1
		};

		Vector3[] normals = new Vector3[4]
		{
			-Vector3.forward,
			-Vector3.forward,
			-Vector3.forward,
			-Vector3.forward
		};

		Vector2[] uv = new Vector2[4]
		{
			new Vector2(0, 0),
			new Vector2(1, 0),
			new Vector2(0, 1),
			new Vector2(1, 1)
		};

		MeshFilter mf = this.GetComponent<MeshFilter>();

		mesh = new Mesh();
		mf.mesh = mesh;
		mesh.Clear();
		mesh.vertices = vertices;
		mesh.triangles = triangles;
		mesh.normals = normals;
		mesh.uv = uv;
		mesh.Optimize();
		mesh.RecalculateNormals();
	}

	public void ClearGenerator()
	{
		this.GetComponent<MeshFilter>().mesh = null;
	}

	public GameObject GenerateNewGOQuad(float width = 1, float depth = 1, string meshName = "GO")
	{
		this.width = width;
		this.depth = depth;
		GenerateQuad();
		generatedGO = new GameObject(meshName);
		generatedGO.AddComponent<MeshFilter>().mesh = mesh;
		generatedGO.AddComponent<MeshRenderer>().material = new Material(Shader.Find("Diffuse"));
		ClearGenerator();
		return generatedGO;
	}

	public void GenerateNewGOQuad()
	{
		GenerateNewGOQuad(this.width, this.depth, "GOGenerated");
	}
}