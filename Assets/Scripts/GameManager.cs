using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //variables
    public GameObject restartScreen;
    public bool isGameActive = true;

    //score variables
    public int currentScore = 0;
    private int highScore = 0;
    public TextMeshProUGUI currentScoreText;
    public TextMeshProUGUI highScoreText;

    //audio variables
    private AudioSource gameAudio;
    public AudioClip deathSound;
    public AudioClip explodeSound;

    // Start is called before the first frame update
    void Start()
    {
        gameAudio = GetComponent<AudioSource>();
        isGameActive = true;
        restartScreen.gameObject.SetActive(false);

        // Load the high score from PlayerPrefs
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        UpdateScoreUI();
    }

    //score managing
    void UpdateScoreUI()
    {
        currentScoreText.text = "Score: " + currentScore;
        highScoreText.text = "High Score: " + highScore;
    }

    public void AddScore(int amount)
    {
        gameAudio.PlayOneShot(explodeSound, 1.0f);
        currentScore += amount;
        if (currentScore > highScore)
        {
            highScore = currentScore;
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();
        }
        UpdateScoreUI();
    }

    //restart managing
    public void GameOver()
    {
        gameAudio.PlayOneShot(deathSound, 1.0f);
        Debug.Log("Game Over");
        restartScreen.gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
