using UnityEngine;
[ExecuteInEditMode]
public class ClippingPlane : MonoBehaviour
{
    [Header("Clip unique object")]
    public Renderer uniqueRenderer = null;
    public bool clippChilds = false;
    [Header("Clip all material-shared objects")]
    [Tooltip("If it's not affecting to previous unique object, try to play and unplay scene")]
    public Material material = null;

    private Plane plane;
    private MaterialPropertyBlock propBlock = null;

    private void Awake()
    {
        /* 
        * this will be used only if uniqueRenderer != null        
        * but as is decleared on awake and uniqueRenderer could be modified on the inspector
        * it's called anyway
        */
        propBlock = new MaterialPropertyBlock();
    }

    private void Update()
    {
        SetClippingPlane();
    }

    private void SetClippingPlane()
    {
        //create plane
        Plane plane = new Plane(transform.up, transform.position);
        //transfer values from plane to vector4
        Vector4 planeRepresentation = new Vector4(plane.normal.x, plane.normal.y, plane.normal.z, plane.distance);

        if (uniqueRenderer != null)
        {
            if (clippChilds)
            {
                foreach (Transform child in uniqueRenderer.transform)
                {
                    if (child.gameObject.activeSelf)
                        SetPlaneVectorAsPropertyBlock(child.gameObject.GetComponent<Renderer>(), planeRepresentation);
                }
            }
            SetPlaneVectorAsPropertyBlock(uniqueRenderer, planeRepresentation);
        }
        else if (material != null)
        {
            material.SetVector("_Plane", planeRepresentation);
        }
    }

    public void SetPlaneVectorAsPropertyBlock(Renderer renderer, Vector4 planeRepresentation)
    {
        if (propBlock != null)
        {
            renderer.GetPropertyBlock(propBlock);
            propBlock.SetVector("_Plane", planeRepresentation);
            renderer.SetPropertyBlock(propBlock);
        }
    }

    #region DRAW GIZMO-PLANE
    private void OnDrawGizmos()
    {
        plane = new Plane(transform.up, transform.position);
        Vector3 position = transform.position;
        Vector3 normal = plane.normal;
        DrawPlane(position, normal);
    }

    void DrawPlane(Vector3 position, Vector3 normal)
    {
        Vector3 v3;
        if (normal.normalized != Vector3.forward)
            v3 = Vector3.Cross(normal, Vector3.forward).normalized * normal.magnitude;
        else
            v3 = Vector3.Cross(normal, Vector3.up).normalized * normal.magnitude; ;

        Vector3 corner0 = position + v3;
        Vector3 corner2 = position - v3;
        Quaternion q = Quaternion.AngleAxis(90.0f, normal);
        v3 = q * v3;
        Vector3 corner1 = position + v3;
        Vector3 corner3 = position - v3;
        Gizmos.color = Color.green;
        Gizmos.DrawLine(corner0, corner2);
        Gizmos.DrawLine(corner1, corner3);
        Gizmos.DrawLine(corner0, corner1);
        Gizmos.DrawLine(corner1, corner2);
        Gizmos.DrawLine(corner2, corner3);
        Gizmos.DrawLine(corner3, corner0);
        Gizmos.color = Color.red;
        Gizmos.DrawRay(position, normal);
    }
    #endregion    
}
