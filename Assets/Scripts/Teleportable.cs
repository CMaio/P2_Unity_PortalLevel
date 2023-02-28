using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleportable : MonoBehaviour
{
    public bool isActive = true; 
    [SerializeField] private float teleportOffset;
    private bool isTeleporting = false;
    private Vector3 teleportPosition;
    private Vector3 teleportForward;
    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip teleportClip;
    private void OnTriggerEnter(Collider other)
    {

        if(other.gameObject.TryGetComponent(out PortalCamera portal) && isActive)
        {
            isTeleporting = true;
            if(this.TryGetComponent<Rigidbody>(out Rigidbody l_Rigidbody))
            {
                Vector3 l_Velocity = portal.virtualPortal.transform.InverseTransformDirection(l_Rigidbody.velocity);
                l_Rigidbody.velocity = portal.otherPortal.transform.TransformDirection(l_Velocity);
                transform.localScale *= (portal.otherPortal.framePortal.transform.localScale.x / portal.framePortal.transform.localScale.x);
            }
            Vector3 l_Position = portal.virtualPortal.InverseTransformPoint(transform.position);
            Vector3 l_Direction = portal.virtualPortal.InverseTransformDirection(transform.forward);

            teleportForward = portal.otherPortal.transform.TransformDirection(l_Direction);
            teleportPosition = portal.otherPortal.transform.TransformPoint(l_Position) + portal.otherPortal.transform.forward * teleportOffset;
            audioSource.clip = teleportClip;
            audioSource.Play();

        }
    }
    private void LateUpdate()
    {
        if (isTeleporting){
            isTeleporting = false;
            if(TryGetComponent(out CharacterController charController))
            {
                charController.enabled = false;
            }
            transform.position = teleportPosition;
            transform.forward = teleportForward;
            if(TryGetComponent(out FPSController fpsController))
            {
                fpsController.recalcualteYawAndPitch();
            }
            if (TryGetComponent(out CharacterController characterController))
            {
                characterController.enabled = true;
            }
        }
    }

}
