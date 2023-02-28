using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] bool companionSpawned = false;
    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip DoorClip;

    public void OpenDoor(GameObject doorOpen)
    {
        doorOpen.transform.position = new Vector3(doorOpen.transform.position.x, doorOpen.transform.position.y + 5, doorOpen.transform.position.z);
        audioSource.clip = DoorClip;
        audioSource.Play();
    }
    public void CloseDoor(GameObject doorClose)
    {
        doorClose.transform.position = new Vector3(doorClose.transform.position.x, doorClose.transform.position.y - 5, doorClose.transform.position.z);
        audioSource.clip = DoorClip;
        audioSource.Play();
    }


    public bool isCompanionSpawned()
    {
        audioSource.clip = DoorClip;
        audioSource.Play();
        return companionSpawned;
    }

    public void companionAlreadySpawned()
    {
        companionSpawned = true;
    }

    public void companionDestroyed()
    {
        companionSpawned = false;
    }
}
