using UnityEngine;

public class MaterialBlockChanger : MonoBehaviour
{
	private MaterialPropertyBlock block;
	private Renderer goRenderer = null;

	private void Awake()
	{
		block = new MaterialPropertyBlock();
		goRenderer = this.GetComponent<Renderer>();
		goRenderer.GetPropertyBlock(block);
	}

	public void SetTexture(Texture newTexture)
	{
		goRenderer.GetPropertyBlock(block);
		block.SetTexture("_MainTex", newTexture);
		goRenderer.SetPropertyBlock(block);
	}
}
