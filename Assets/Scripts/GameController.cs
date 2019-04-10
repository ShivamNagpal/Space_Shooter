using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public GameObject[] hazards;
    public Vector3 spawnValues;
    public int hazardCount;
    public float startWait;
    public float spawnWait;
    public float waveWait;
    public Text scoreText;
    public Text restartText;
    public Text gameOverText;
    public GameObject pauseMenu;
    private bool gameOver;
    private bool restart;
    private static bool paused;
    public static bool Paused
    {
        get
        {
            return paused;
        }
    }
    private int score;
    public static GameController instance;

    private void Awake()
    {
        if (GameController.instance == null)
        {
            GameController.instance = this;
        }
        Time.timeScale = 1;
    }

    private void Start()
    {
        score = 0;
        UpdateScore();
        restartText.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(false);
        pauseMenu.gameObject.SetActive(false);
        gameOver = false;
        paused = false;
        restart = false;

        StartCoroutine(SpawnWaves());
    }

    private void Update()
    {
        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public static void Resume()
    {
        GameController.instance.pauseMenu.gameObject.SetActive(false);
        Time.timeScale = 1;
        paused = false;
    }

    public static void Pause()
    {
        Time.timeScale = 0;
        GameController.instance.pauseMenu.gameObject.SetActive(true);
        paused = true;
    }

    private IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-1 * spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);

            if (gameOver)
            {
                restart = true;
                restartText.gameObject.SetActive(true);
                break;
            }
        }
    }

    public void AddScore(int scoreValue)
    {
        score += scoreValue;
        UpdateScore();
    }

    private void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
        gameOver = true;
        gameOverText.gameObject.SetActive(true);
    }
}
