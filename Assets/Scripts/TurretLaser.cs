using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretLaser : MonoBehaviour
{
    [SerializeField] LineRenderer laserRenderer;
    [SerializeField] LayerMask layerMask;
    [SerializeField] bool isActive = true;
    [SerializeField] float maxLaserDist;
    private DeathController deathController;
    private PortalButton portalButtonLaser = null;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip destroyWithLaserClip;
    public void updateState(bool isActive)
    {
        this.isActive = isActive; 
        laserRenderer.enabled = isActive;
    }
    private void Awake()
    {
        deathController = GameObject.Find("GameController").GetComponent<DeathController>();
    }

    private void Update()
    {
        if (isActive)
        {
            Ray r = new Ray(laserRenderer.transform.position, laserRenderer.transform.forward);
            if(Physics.Raycast(r,out RaycastHit hitInfo, maxLaserDist, layerMask))
            {

                laserRenderer.SetPosition(1, Vector3.forward * hitInfo.distance);

                if (hitInfo.transform.gameObject.CompareTag("Player")) deathController.playerDeath();

                if (hitInfo.transform.gameObject.CompareTag("turret"))
                {
                    audioSource.clip = destroyWithLaserClip;
                    audioSource.Play();
                    Destroy(hitInfo.transform.gameObject);
                    return;
                }

                if (hitInfo.transform.gameObject.TryGetComponent(out TurretLaser laser)){
                    if(hitInfo.transform.gameObject.TryGetComponent(out offLaser laserOff))
                    {
                        laserOff.isOnLaser();
                    }
                    laser.updateState(isActive);
                }

                if(hitInfo.transform.gameObject.TryGetComponent(out PortalButton but))
                {
                    but.openDoor();
                    portalButtonLaser = but;
                }

            }else{
                laserRenderer.SetPosition(1, Vector3.forward * maxLaserDist);
            }
        }
    }
}
