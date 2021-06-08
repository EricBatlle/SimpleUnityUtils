using System.Runtime.InteropServices;
using UnityEngine;

public class ConfineCursorToMainDisplay : MonoBehaviour
{
	[SerializeField] private bool enableConfinement = true;

	private int displayIndexToBind = 0;
	private MousePosition mousePosition;

	#region user32.dll methods and structs
	[DllImport("user32.dll")]
	[return: MarshalAs(UnmanagedType.Bool)]
	private static extern bool GetCursorPos(out MousePosition lpMousePosition);

	[DllImport("user32.dll")]
	private static extern bool SetCursorPos(int X, int Y);

	[StructLayout(LayoutKind.Sequential)]
	public struct MousePosition
	{
		public int x;
		public int y;

		public override string ToString()
		{
			return "[" + x + ", " + y + "]";
		}
	}
	#endregion

	// Start is called before the first frame update
	private void Start()
	{
		//Disable this script if there are not more displays
		if (Display.displays.Length < 2)
		{
			this.gameObject.GetComponent<ConfineCursorToMainDisplay>().enabled = false;
		}
	}

	private void Update()
	{
		//if cursor is in another display, center the cursor to confined display
		if (enableConfinement && GetCursorDisplay() != displayIndexToBind)
		{
			CenterCursor();
		}
	}

	private void CenterCursor()
	{
		int bindedScreenWidth = Display.displays[displayIndexToBind].renderingWidth;
		int bindedScreenHeight = Display.displays[displayIndexToBind].renderingHeight;
		SetCursorPos(bindedScreenWidth / 2, bindedScreenHeight / 2);
	}

	private int GetCursorDisplay()
	{
		GetCursorPos(out mousePosition);
		int sx = mousePosition.x;
		int sy = mousePosition.y;
		Vector3 r = Display.RelativeMouseAt(new Vector3(sx, sy));

		int displayIndex = (int)r.z;

		return displayIndex;
	}
}
