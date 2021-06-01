using UnityEngine;

public class PlaneVisualizer : MonoBehaviour
{
	[SerializeField] private Vector3 a = Vector3.right;
	[SerializeField] private Vector3 b = Vector3.up;
	[SerializeField] private Vector3 c = Vector3.forward;

	public void SetPlanePoints(Vector3 a, Vector3 b, Vector3 c)
	{
		this.a = a;
		this.b = b;
		this.c = c;
	}

	private void OnDrawGizmos()
	{
		Plane plane = new Plane(a, b, c);

		// Draw our three input points in world space.
		// b and c are drawn as lollipops from the preceding point,
		// so that you can see the clockwise winding direction.

		Gizmos.color = Color.white;
		Gizmos.DrawWireSphere(a, 0.1f);

		Gizmos.color = Color.gray;
		Gizmos.DrawLine(a, b);
		Gizmos.DrawWireSphere(b, 0.1f);

		Gizmos.color = Color.black;
		Gizmos.DrawLine(b, c);
		Gizmos.DrawWireSphere(c, 0.1f);

		// Draw this object's position, 
		// as a lollipop sticking out from our plane,
		// blue-green if in front (in the positive half-space),
		// and red if behind (negative half-space).           
		Gizmos.color = plane.GetSide(transform.position) ? Color.cyan : Color.red;
		Gizmos.DrawLine(plane.ClosestPointOnPlane(transform.position), transform.position);
		Gizmos.DrawWireSphere(transform.position, 0.2f);

		// Draw plane normal.
		Gizmos.color = Color.yellow;
		var center = (a + b + c) / 3f;
		Gizmos.DrawLine(center, center + plane.normal);

		// Draw planar grid.
		Gizmos.color = Color.blue;
		var matrix = Gizmos.matrix;
		Gizmos.matrix = Matrix4x4.TRS(center, Quaternion.LookRotation(plane.normal), Vector3.one);
		for (int i = -10; i <= 10; i++)
		{
			Gizmos.DrawLine(new Vector3(i, -10, 0), new Vector3(i, 10, 0));
			Gizmos.DrawLine(new Vector3(-10, i, 0), new Vector3(10, i, 0));
		}
		Gizmos.matrix = matrix;
	}
}
