using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathController : MonoBehaviour
{
    [SerializeField] GameObject canvasDie;
    [SerializeField] GameObject loading;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip deathClip;

    bool death = false;
    private void Awake()
    {
        Time.timeScale = 1;
    }
    public void playerDeath()
    {
        if (!death)
        {
            death = true;
            audioSource.clip = deathClip;
            audioSource.Play();
            canvasDie.SetActive(true);
            Time.timeScale = 0;
        }
    }

    private void Update()
    {
        if (death)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                loading.SetActive(true);
                Time.timeScale = 1;
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);

            }
        }
    }

}
