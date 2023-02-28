using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DeathZone : MonoBehaviour
{
    [SerializeField] DeathController deathController;

    private void Awake()
    {
        deathController = GameObject.Find("GameController").GetComponent<DeathController>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<FPSController>(out FPSController fPSController))
        {
            deathController.playerDeath();
        }
    }

}
