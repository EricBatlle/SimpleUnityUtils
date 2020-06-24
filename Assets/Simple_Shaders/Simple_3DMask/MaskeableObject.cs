using UnityEngine;

/// <summary>
/// Sets the RenderQueue of an object's materials on Awake. This will instance 
/// the materials, so the script won't interfere with other renderers that
/// reference the same materials.
/// </summary>
[AddComponentMenu("Rendering/SetRenderQueue")]
[ExecuteInEditMode]
public class MaskeableObject : MonoBehaviour
{
    [SerializeField]
    protected int[] m_queues = new int[] { 4020 };

    protected void Awake()
    {
        Material[] materials = GetComponent<Renderer>().sharedMaterials;
        for (int i = 0; i < materials.Length && i < m_queues.Length; ++i)
        {
            materials[i].renderQueue = m_queues[i];
        }
    }
}