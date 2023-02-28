using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityGun : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    [SerializeField] float maxDistance;
    [SerializeField] Transform attachPosition;
    [SerializeField] float moveSpeed;
    [SerializeField] private float trhowForce = 1000;
    Rigidbody takenObject;
    Vector3 initialPosition;
    Quaternion initialRotation;
    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip takingClip;
    [SerializeField] private AudioClip trhowClip;
    [SerializeField] private AudioClip letDownClip;
    enum Status { taking, taken, withoutObject }
    Status currentStatus = Status.withoutObject;
    void Update()
    {
        if(takenObject == null)
        {
            currentStatus = Status.withoutObject;
        }
        if (Input.GetMouseButtonDown(0) && currentStatus == Status.withoutObject)
        {
            takenObject = gravityShoot();
        }
        if (takenObject != null)
        {
            if (takenObject != null)
            {
                takenObject.isKinematic = true;
                switch (currentStatus)
                {
                    case Status.taking:
                        updateTaking();
                        break;
                    case Status.taken:
                        updateTaken();
                        break;
                }
            }
            if (Input.GetMouseButtonDown(0) && currentStatus == Status.taken)
            {
                detachObject(trhowForce);
                currentStatus = Status.withoutObject;
                audioSource.clip = trhowClip;
                audioSource.Play();
            }
            if(Input.GetMouseButtonDown(1) && currentStatus == Status.taken)
            {
                detachObject(0);
                currentStatus = Status.withoutObject;
                audioSource.clip = letDownClip;
                audioSource.Play();
            }
        }
    }
    private Rigidbody gravityShoot()
    {
        Ray r = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.0f));
        RaycastHit hitInfo;
        if (Physics.Raycast(r, out hitInfo, maxDistance))
        {
            if (hitInfo.transform.gameObject.CompareTag("Refraction")) return null;

            Rigidbody rb = hitInfo.rigidbody;
            if (rb == null)
            {
                return null;
            }
            rb.isKinematic = true;
            if(rb.gameObject.TryGetComponent(out Teleportable tp)){
                tp.isActive = false;
            }
            initialPosition = rb.transform.position;
            initialRotation = rb.transform.rotation;
            currentStatus = Status.taking;
            audioSource.clip = takingClip;
            audioSource.Play();
            return rb;
        }
        return null;
    }
    private void detachObject(float force)
    {
        if (takenObject.gameObject.TryGetComponent(out Teleportable tp))
        {
            tp.isActive = true;
        }
        takenObject.isKinematic = false;
        takenObject.AddForce(attachPosition.forward * force);
        takenObject = null;
    }
    private void updateTaking()
    {
        takenObject.MovePosition(takenObject.position
            + (attachPosition.position - takenObject.position).normalized
            * moveSpeed * Time.deltaTime);

        takenObject.rotation = Quaternion.Lerp(initialRotation, attachPosition.rotation,
            (takenObject.position - initialPosition).magnitude
            / (attachPosition.position - initialPosition).magnitude);

        if ((attachPosition.position - takenObject.position).magnitude < 0.2f)
            currentStatus = Status.taken;

    }
    private void updateTaken()
    {
        takenObject.transform.position = attachPosition.position;
        takenObject.transform.rotation = attachPosition.rotation;
    }
}
