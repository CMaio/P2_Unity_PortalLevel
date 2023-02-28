using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalCamera : MonoBehaviour
{

    [SerializeField] public PortalCamera otherPortal;
    [SerializeField] private Transform playerCameraTransform;
    [SerializeField] public Transform virtualPortal;
    [SerializeField] private Transform portalCamera;
    [SerializeField] private float nearClipOffset;
    public GameObject framePortal;

    private void Update()
    {
        
        Vector3 local_position = 
            virtualPortal.InverseTransformPoint(playerCameraTransform.position);
        otherPortal.portalCamera.transform.position = 
            otherPortal.transform.TransformPoint(local_position);
        
        
         Vector3 local_direction =
            virtualPortal.InverseTransformDirection(playerCameraTransform.forward);
        otherPortal.portalCamera.transform.forward =
            otherPortal.transform.TransformDirection(local_direction);
        

        float playerDist = (playerCameraTransform.position - transform.position).magnitude;

        otherPortal.portalCamera.GetComponent<Camera>().nearClipPlane = playerDist + nearClipOffset;

    }
    public Transform getVirtualPortal()
    {
        return virtualPortal;
    }
    public PortalCamera getOtherPortal()
    {
        return otherPortal;
    }

    public void changeOffset(int add)
    {
        nearClipOffset = add + 2;
    }


}
