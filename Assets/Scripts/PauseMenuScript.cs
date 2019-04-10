using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    public void Resume()
    {
        GameController.Resume();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
        GameController.Resume();
    }
}
