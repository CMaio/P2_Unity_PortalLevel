using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinController : MonoBehaviour
{
    [SerializeField] GameObject canvasWin;
    private bool playerWin = false;
    [SerializeField] GameObject loading;
    public void win()
    {
        playerWin = true;
        canvasWin.SetActive(true);
        Time.timeScale = 0;
    }

    private void Update()
    {
        if (playerWin)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                loading.SetActive(true);
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                Time.timeScale = 1;
            }
        }
    }
}
