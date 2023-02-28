using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewPortal : MonoBehaviour
{
    [SerializeField] private List<Transform> controlPoints;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private string scenaryTag;
    [SerializeField] private float maxNormalAngle;
    [SerializeField] private float maxDistance;

    float distanceHitWithControlPoint, distancePlayerWithHit;

    public bool isValidPosition(Camera cam)
    {

        foreach(var point in controlPoints){
            if(Physics.Raycast(cam.transform.position, point.position - cam.transform.position, out RaycastHit hitInfo, float.MaxValue, layerMask)){
                if (!hitInfo.transform.gameObject.CompareTag(scenaryTag))
                {
                    Debug.Log("INVALID POSITION: TAG");
                    return false;
                }
                if (Vector3.Angle(hitInfo.normal, point.forward) > maxNormalAngle)
                {
                    Debug.Log("INVALID POSITION: ANGLE");
                    return false;
                }
                if ((hitInfo.point - point.position).magnitude > maxDistance)
                {
                    Debug.Log("INVALID POSITION: DISTANCE");
                    return false;
                }
            }
            else
            {
                Debug.Log("INVALID POSITION: NO RAY");
                return false;
            }
        }

        return true;
    }
}
