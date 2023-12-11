using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // player prefab
    public GameObject playerPrefab;

    //UI variables
    public GameObject restartScreen;
    public bool isGameActive = true;
    public int currentScore = 0;
    private int highScore = 0;
    public TextMeshProUGUI currentScoreText;
    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI livesDisplay;

    //audio variables
    private AudioSource gameAudio;
    public AudioClip deathSound;
    public AudioClip explodeSound;

    //player lives
    private int lifeCount = 3;

    private int asteroidCount;

    // Start is called before the first frame update
    void Start()
    {
        gameAudio = GetComponent<AudioSource>();
        isGameActive = true;
        restartScreen.gameObject.SetActive(false);

        // Load the high score from PlayerPrefs
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        UpdateScoreUI();
        livesDisplay.text = "A A A";
    }


    //score managing
    public void AddScore(int amount)
    {
        // adds to the scores
        gameAudio.PlayOneShot(explodeSound, 1.0f);
        currentScore += amount;
        //checks if current score is higher than high score, updates high score if it is
        if (currentScore > highScore)
        {
            highScore = currentScore;
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();
        }
        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        //updates the score displays to show the current scores
        currentScoreText.text = "Score: " + currentScore;
        highScoreText.text = "High Score: " + highScore;
    }

    

    // removes lives when player collides with asteroid, triggers game over when lives run out
    public void updateLifeCount()
    {
        lifeCount--;
        if (lifeCount == 2)
        {
            livesDisplay.text = "A A";
            Instantiate(playerPrefab, playerPrefab.transform.position, playerPrefab.transform.rotation);
        }
        else if (lifeCount == 1)
        {
            livesDisplay.text = "A";
            Instantiate(playerPrefab, playerPrefab.transform.position, playerPrefab.transform.rotation);

        }
        else if (lifeCount == 0)
        {
            livesDisplay.text = " ";
            GameOver();
        }
    }

    //restart managing
    public void GameOver()
    {
        //plays death sound and displays the restart menu
        gameAudio.PlayOneShot(deathSound, 1.0f);
        Debug.Log("Game Over");
        restartScreen.gameObject.SetActive(true);
    }

    //restarts the game
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
