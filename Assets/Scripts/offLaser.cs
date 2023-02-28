using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class offLaser : MonoBehaviour
{
    [SerializeField] LineRenderer laserRenderer;
    [SerializeField] TurretLaser turretLaser;
    bool isOn = true;

    void Update()
    {
        laserRenderer.enabled = isOn;
        turretLaser.enabled = isOn;
        isOn = false;
    }


    public void isOnLaser()
    {
        isOn = true;
    }
}
