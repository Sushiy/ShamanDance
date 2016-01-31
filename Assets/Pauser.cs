using UnityEngine;
using System.Collections;

using UnityEngine;
using System.Collections;

public class Pauser : MonoBehaviour
{
    public string buttonString;
    public GameObject pausePanel;

    private bool paused = false;

    void Update()
    {
        PauseInput();
    }

    void PauseInput()
    {
        if (Input.GetButtonDown(buttonString) || Input.GetKeyDown(KeyCode.Return)) {
            if (pausePanel.activeSelf) {
                Unpause();
            }
            else {
                Pause();
            }
        }
    }

    void Pause()
    {

        pausePanel.SetActive(true);
        Time.timeScale = 0;
        paused = true;
    }

    void Unpause()
    {
        Time.timeScale = 1;
        paused = false;
        pausePanel.SetActive(false);
    }
}
