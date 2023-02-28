using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class melterObjects : MonoBehaviour
{
    [SerializeField] private LevelController levelController;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip destroyObjectsClip;
    public void Awake()
    {
        levelController = GameObject.Find("GameController").GetComponent<LevelController>();
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Companion")
        {
            audioSource.clip = destroyObjectsClip;
            audioSource.Play();
            levelController.companionDestroyed();
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "turret")
        {
            audioSource.clip = destroyObjectsClip;
            audioSource.Play();
            Destroy(collision.gameObject);
        }
    }
}
