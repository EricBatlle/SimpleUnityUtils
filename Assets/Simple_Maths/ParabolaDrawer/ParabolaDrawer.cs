using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ParabolaDrawer : MonoBehaviour
{
	[Header("Parabola main points")]
	[SerializeField] private GameObject initialPointGO = null;
	[SerializeField] private GameObject finalPointGO = null;
	[SerializeField] private GameObject betweenPointGO = null;

	[Tooltip("Less separation for major precision")]
	[SerializeField] private float pointsSeparationDistance = 0.5f; //Parabola curvature precision
	[SerializeField] private List<Vector3> parabolaPoints = new List<Vector3>();

	private GameObject parabolaParentGO = null;
	private GameObject pointsParentGO = null;

	[ContextMenu("Draw Parabola")]
	public void Draw()
	{
		RemoveAll3DPoints();
		GenerateParabolaPoints();

		parabolaParentGO = new GameObject("Parabola");
		parabolaParentGO.transform.SetParent(this.transform);
		pointsParentGO = new GameObject("Points");
		pointsParentGO.transform.SetParent(parabolaParentGO.transform);

		foreach (Vector3 parabolaPointPosition in parabolaPoints)
		{
			GameObject go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
			go.name = $"Point_{parabolaPointPosition.x}_{parabolaPointPosition.y}_{parabolaPointPosition.z}";
			go.transform.position = parabolaPointPosition;
			go.transform.SetParent(pointsParentGO.transform);
		}
	}

	public List<Vector3> GetParabolaPoints()
	{
		return parabolaPoints;
	}
	public List<Vector3> GetParabolaPoints(Vector3 trajectoryInitialPosition, Vector3 trajectoryAnotherPosition, Vector3 trajectoryFinalPosition)
	{
		initialPointGO.transform.position = trajectoryInitialPosition;
		finalPointGO.transform.position = trajectoryFinalPosition;
		betweenPointGO.transform.position = trajectoryAnotherPosition;
		return GenerateParabolaPoints();
	}

	private List<Vector3> GenerateParabolaPoints()
	{
		parabolaPoints.Clear();

		Vector3 initialPosition = initialPointGO.transform.position;
		Vector3 finalPosition = finalPointGO.transform.position;
		Vector3 betweenPosition = betweenPointGO.transform.position;

		Plane plane = new Plane(finalPosition, betweenPosition, initialPosition);
		Vector3 centroid = (initialPosition + betweenPosition + initialPosition) / 3f;

		//Plane Vectors
		Vector3 a = finalPosition - initialPosition;
		Vector3 b = betweenPosition - initialPosition;

		//Vector p (normal)
		Vector3 p = plane.normal;

		//Vector m (perpendicular to a, ortogonal to p)
		Vector3 m = Vector3.Cross(p, a);

		//Normalized Vectors
		Vector3 u = Vector3.Normalize(a);
		Vector3 v = Vector3.Normalize(m);

		//Positions represented on the new coordinates (Vector2)
		Vector2 finalPositionNew = new Vector2(Vector3.Dot(a, u), Vector3.Dot(a, v));
		Vector2 betweenPositionNew = new Vector2(Vector3.Dot(b, u), Vector3.Dot(b, v));
		Vector2 initialPositionNew = new Vector2(0, 0); //center of coordinate axis

		//Now we have 3 positions on the bidimensional axis for the parabola, try to solve the parabola equation
		Vector3 abc = TripleQuadraticEquationsTripleVariableSolver(initialPositionNew, finalPositionNew, betweenPositionNew);

		// vp = ax^2+bx+c
		float vp = 0;
		for (float up = 0; up < finalPositionNew.x; up += pointsSeparationDistance)
		{
			vp = abc.x * up * up + abc.y * up + abc.z;
			//Do the transform backwards
			float x = initialPosition.x + up * u.x + vp * v.x;
			float y = initialPosition.y + up * u.y + vp * v.y;
			float z = initialPosition.z + up * u.z + vp * v.z;
			parabolaPoints.Add(new Vector3(x, y, z));
		}

		return parabolaPoints;
	}

	//Given 3 positions to match into cuadratic equations (y=ax^2+bx+c), return the value of the 3 coeficient variables (a,b,c)
	private Vector3 TripleQuadraticEquationsTripleVariableSolver(Vector2 initialPosition, Vector2 finalPosition, Vector2 betweenPosition)
	{
		Vector2 p1 = initialPosition;
		Vector2 p2 = finalPosition;
		Vector2 p3 = betweenPosition;
		float a = p1.x * p1.x;
		float b = p1.x;
		float c = 1;
		float d = -p1.y;

		float p = p2.x * p2.x;
		float q = p2.x;
		float r = 1;
		float s = -p2.y;

		float g = p3.x * p3.x;
		float h = p3.x;
		float k = 1;
		float l = -p3.y;

		//https://www.mindstick.com/blog/11223/logic-to-solve-two-or-three-variable-equation-using-c-sharp-language
		float U, V;
		float W, Y, Z;
		U = a * (q * k - r * h) - b * (p * k - r * g) + c * (p * h - q * g);
		V = b * (-r * l + s * k) + c * (q * l - s * h) + d * (-q * k + r * h);

		W = V / U;

		V = a * (r * l - s * k) - c * (p * l - s * g) + d * (p * k - r * g);
		Y = V / U;

		V = -a * (q * l - s * h) + b * (p * l - s * g) - d * (p * h - q * g);
		Z = V / U;

		return new Vector3(W, Y, Z);
	}

	private void RemoveAll3DPoints()
	{
		if (parabolaParentGO != null && pointsParentGO != null)
		{
			//Array to hold all child obj
			GameObject[] allChildren = new GameObject[parabolaParentGO.transform.childCount];

			//Find all child obj and store to that array
			int i = 0;
			foreach (Transform child in parabolaParentGO.transform)
			{
				allChildren[i] = child.gameObject;
				i += 1;
			}

			//Now destroy them
			foreach (GameObject child in allChildren)
			{
				DestroyImmediate(child.gameObject);
			}

			DestroyImmediate(parabolaParentGO);
		}
	}
}
