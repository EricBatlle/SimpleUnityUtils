using UnityEngine;

public class ExampleParabolaDrawerController : MonoBehaviour
{
	[SerializeField] private ParabolaDrawer parabolaDrawer = null;

	private void Update()
	{
		parabolaDrawer.Draw();
	}
}
