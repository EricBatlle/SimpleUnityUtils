using UnityEngine;

public static class PlaneUtility
{
	// Mimics Debug.DrawLine, drawing a plane containing the 3 provided worldspace points,
	// with the visualization centered on the centroid of the triangle they form.
	public static Plane DrawPlane(Vector3 a, Vector3 b, Vector3 c, float size,
		Color color, float duration = 0f, bool depthTest = true)
	{

		var plane = new Plane(a, b, c);
		var centroid = (a + b + c) / 3f;

		DrawPlaneAtPoint(plane, centroid, size, color, duration, depthTest);
		return plane;
	}

	// Draws the portion of the plane closest to the provided point, 
	// with an altitude line colour-coding whether the point is in front (cyan)
	// or behind (red) the provided plane.
	public static void DrawPlaneNearPoint(Plane plane, Vector3 point, float size, Color color, float duration = 0f, bool depthTest = true)
	{
		var closest = plane.ClosestPointOnPlane(point);
		Color side = plane.GetSide(point) ? Color.cyan : Color.red;
		Debug.DrawLine(point, closest, side, duration, depthTest);

		DrawPlaneAtPoint(plane, closest, size, color, duration, depthTest);
	}

	// Non-public method to do the heavy lifting of drawing the grid of a given plane segment.
	private static void DrawPlaneAtPoint(Plane plane, Vector3 center, float size, Color color, float duration, bool depthTest)
	{
		var basis = Quaternion.LookRotation(plane.normal);
		var scale = Vector3.one * size / 10f;

		var right = Vector3.Scale(basis * Vector3.right, scale);
		var up = Vector3.Scale(basis * Vector3.up, scale);

		for (int i = -5; i <= 5; i++)
		{
			Debug.DrawLine(center + right * i - up * 5, center + right * i + up * 5, color, duration, depthTest);
			Debug.DrawLine(center + up * i - right * 5, center + up * i + right * 5, color, duration, depthTest);
		}
	}
}
