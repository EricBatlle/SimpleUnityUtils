using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Code is commented to avoid installing cinemachine package on this repo
/// </summary>
public class CreateVCamLookAtObject : MonoBehaviour
{
//    [SerializeField] private float desiredDistance = 10f;
//    [SerializeField] private CinemachineVirtualCamera vcam = null;
//    [SerializeField] private GameObject lookAtObject = null;

//    [ContextMenu("Create in front LookAtObject")]
//    public void CreateLookAtObject()
//    {
//        if (vcam != null)
//        {
//            Vector3 lookAtPoint = vcam.State.RawPosition + (vcam.State.RawOrientation * Vector3.forward) * desiredDistance;
//            lookAtObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
//            lookAtObject.name = "LookAtObject";
//            lookAtObject.transform.position = lookAtPoint;

//            vcam.LookAt = lookAtObject.transform;
//        }
//    }

//    private void Update()
//    {
//#if UNITY_EDITOR 
//        if (!EditorApplication.isPlaying)
//            if (lookAtObject != null && vcam != null)
//                lookAtObject.transform.position = vcam.State.RawPosition + (vcam.State.RawOrientation * Vector3.forward) * desiredDistance;
//#endif
//    }
}
