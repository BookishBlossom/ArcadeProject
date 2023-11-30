using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private int score;
    public TextMeshProUGUI scoreText;
    public GameObject restartScreen;
    public bool isGameActive = true;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        UpdateScore(0);
        isGameActive = true;
        restartScreen.gameObject.SetActive(false);
    }

    public void UpdateScore(int ScoreToAdd)
    {
        score += ScoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
        restartScreen.gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
