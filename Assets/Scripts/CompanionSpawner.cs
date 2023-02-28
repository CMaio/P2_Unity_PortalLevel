using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanionSpawner : MonoBehaviour
{
    [SerializeField] private GameObject objectToSpawn;
    [SerializeField] private Transform transformToSpawn;
    [SerializeField] private Vector3 multiSpawnOffset;
    [SerializeField] private LevelController levelController;

    public void Awake()
    {
        levelController = GameObject.Find("GameController").GetComponent<LevelController>();
    }

    public void spawn(int amount)
    {
        for(int i=0;i<amount; i++)
        {
            if (!levelController.isCompanionSpawned())
            {
                Instantiate(objectToSpawn, transform.position+multiSpawnOffset*i, transform.rotation);
                levelController.companionAlreadySpawned();
            }
        }
    }
}
