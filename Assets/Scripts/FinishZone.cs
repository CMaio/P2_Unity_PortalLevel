using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishZone : MonoBehaviour
{
    [SerializeField] WinController winController;
    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    private void Awake()
    {
        winController = GameObject.Find("GameController").GetComponent<WinController>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<FPSController>(out FPSController fPSController))
        {
            audioSource.Play();
            winController.win();
        }
    }

}
