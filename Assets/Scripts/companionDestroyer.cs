using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class companionDestroyer : MonoBehaviour
{
    [SerializeField] private LevelController levelController;

    void Awake()
    {
        levelController = GameObject.Find("GameController").GetComponent<LevelController>();
    }


    public void destroyer()
    {
        levelController.companionDestroyed();
        Destroy(this.gameObject);
    }
}
