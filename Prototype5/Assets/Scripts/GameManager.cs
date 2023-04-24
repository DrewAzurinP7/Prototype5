using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI gameOverText;
    public List<GameObject> targets;
    public GameObject titleScreen;
    public GameObject pauseScreen;
    public Button restartButton;
    public bool isGameActive;

    private float spawnRate = 1.0f;
    private int score;
    private int lives;
    private bool paused;

    // Start is called before the first frame update
    public void StartGame(int difficulty)
    {
        isGameActive = true;
        score = 0;
        spawnRate = spawnRate / difficulty;

        StartCoroutine(SpawnTarget());
        UpdateScore(0);
        UpdateLives(3);

        titleScreen.gameObject.SetActive(false);
    }

    void ChangePaused()
    {
        if (!paused)
        {
            paused = true;
            pauseScreen.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            paused = false;
            pauseScreen.SetActive(false);
            Time.timeScale = 1;
        }
    }
    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        isGameActive = false;
        restartButton.gameObject.SetActive(true);
    }
    IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void UpdateLives(int livesToChange)
    {
        lives += livesToChange;
        livesText.text = "Lives: " + lives;
        if(lives <= 0)
        {
            GameOver();
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void Update()
    {
        //Check if the user has press the P key
        if (Input.GetKeyDown(KeyCode.P))
        {
            ChangePaused();
        }
    }
}
