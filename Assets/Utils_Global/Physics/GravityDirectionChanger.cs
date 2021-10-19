using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class GravityDirectionChanger : MonoBehaviour
{
	public enum GravityDirection
	{
		Up, Down, Forward, Backward, None
	}
	public static Dictionary<GravityDirection, Vector3> gravityDirectionDict = new Dictionary<GravityDirection, Vector3>
	{
		{ GravityDirection.Up, Vector3.up },
		{ GravityDirection.Down, Vector3.down },
		{ GravityDirection.Forward, Vector3.forward },
		{ GravityDirection.Backward, Vector3.forward * -1 },
		{ GravityDirection.None, Vector3.zero }
	};

	[SerializeField] private GravityDirection gravityDirection = GravityDirection.None;

	private void FixedUpdate()
	{
		this.GetComponent<Rigidbody>().AddForce(gravityDirectionDict[gravityDirection], ForceMode.Acceleration);
	}
}
