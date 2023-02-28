using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TurretAngleDetector : MonoBehaviour
{
    [SerializeField] float maxAngle;
    bool isActive = true;
    bool permanentDeactivated = false;

    [SerializeField] UnityEvent activate;
    [SerializeField] UnityEvent deactivate;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip deactivateClip;


    private void Update()
    {
        if (!permanentDeactivated)
        {
            float angle = Vector3.Angle(transform.up, Vector3.up);
            if (isActive && angle > maxAngle)
            {
                isActive = false;
                deactivate.Invoke();
            }

            if (!isActive && angle < maxAngle)
            {
                isActive = true;
                activate.Invoke();
            }
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("turret") || collision.gameObject.CompareTag("Companion"))
        {
            if (!permanentDeactivated)
            {
                audioSource.clip = deactivateClip;
                audioSource.Play();
                deactivate.Invoke();
                permanentDeactivated = true;
            }

        }
        
    }

}
