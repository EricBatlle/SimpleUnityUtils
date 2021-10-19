using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class MeshGenerator : MonoBehaviour
{
	[SerializeField] private float width = 1;
	[SerializeField] private float height = 1;
	[SerializeField] private float depth = 1;

	private Mesh mesh = null;
	[DisplayWithoutEdit] public GameObject generatedGO = null;

	public void GenerateMesh()
	{
		Vector3[] vertices = {
            //Front Face
            new Vector3 (0, 0, 0),              //0
            new Vector3 (width, 0, 0),          //1
            new Vector3 (width, height, 0),     //2
            new Vector3 (0, height, 0),         //3
            //Back Face
            new Vector3 (0, height, depth),     //4
            new Vector3 (width, height, depth), //5
            new Vector3 (width, 0, depth),      //6
            new Vector3 (0, 0, depth),          //7
            //Left Face
            new Vector3 (0, 0, 0),              //8=0
            new Vector3 (0, 0, depth),          //9=7
            new Vector3 (0, height, 0),         //10=3
            new Vector3 (0, height, depth),     //11=4
            //Right Face
            new Vector3 (width, 0, 0),          //12=1
            new Vector3 (width, height, 0),     //13=2
            new Vector3 (width, height, depth), //14=5
            new Vector3 (width, 0, depth),      //15=6
            //Top Face
            new Vector3 (width, height, 0),     //16=2
            new Vector3 (0, height, 0),         //17=3
            new Vector3 (0, height, depth),     //18=4
            new Vector3 (width, height, depth), //19=5
            //Bottom Face
            new Vector3 (0, 0, 0),              //20=0
            new Vector3 (width, 0, depth),      //21=6
            new Vector3 (0, 0, depth),          //22=7
            new Vector3 (width, 0, 0),          //23=1
        };

		int[] triangles = {
            //Front Face
            0, 2, 1,
			0, 3, 2,
            //Top Face
            16, 17, 18,
			16, 18, 19,
            //Left Face
            8, 9, 11,
			8, 11, 10,
            //Right Face
            12, 13, 14,
			12, 14, 15,
            //Back Face
            5, 4, 7,
			5, 7, 6,
            //Bottom Face
            20, 21, 22,
			20, 23, 21
		};

		MeshFilter mf = this.GetComponent<MeshFilter>();

		mesh = new Mesh();
		mf.mesh = mesh;
		mesh.Clear();
		mesh.vertices = vertices;
		mesh.triangles = triangles;
		mesh.Optimize();
		mesh.RecalculateNormals();
	}

	public void ClearGenerator()
	{
		this.GetComponent<MeshFilter>().mesh = null;
	}

	public void GenerateNewGOMesh(float width = 1, float height = 1, float depth = 1, string meshName = "GO")
	{
		this.width = width;
		this.height = height;
		this.depth = depth;
		GenerateMesh();
		generatedGO = new GameObject(meshName);
		generatedGO.AddComponent<MeshFilter>().mesh = mesh;
		generatedGO.AddComponent<MeshRenderer>().material = new Material(Shader.Find("Diffuse"));
		ClearGenerator();
	}

	public void GenerateNewGOMesh()
	{
		GenerateNewGOMesh(this.width, this.height, this.depth, "GOGenerated");
	}
}