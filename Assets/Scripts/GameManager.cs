using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private float scoreIncrementTime;
    [SerializeField] private SoundManager soundManager;

    [Header("Menu")]
    [SerializeField] private GameObject menu;
    [SerializeField] private TextMeshProUGUI menuScoreText;
    [SerializeField] private TextMeshProUGUI menuHighScoreText;


    private int score;
    private int highScore;
    private float _elapsedTime;

    private bool isGameFinished;
    void Update()
    {
        if (!isGameFinished)
        {
            _elapsedTime += Time.deltaTime;
            if (_elapsedTime >= scoreIncrementTime)
            {
                _elapsedTime = 0;
                score++;
                scoreText.text = score.ToString();
            }
        }
    }

    public void FinishGame()
    {
        isGameFinished = true;
        soundManager.PLAY_DEATH_SOUND();
        soundManager.STOP_MUSIC();

        SaveHighScore();
        StartCoroutine(ShowMenu());
    }

    private IEnumerator ShowMenu()
    {
        yield return new WaitForSeconds(2);
        menuScoreText.text = score.ToString();
        menuHighScoreText.text = highScore.ToString();
        menu.SetActive(true);
    }

    private void SaveHighScore()
    {
        if (PlayerPrefs.HasKey("HighScore"))
        {
            int hs = PlayerPrefs.GetInt("HighScore");
            if (score > hs)
                PlayerPrefs.SetInt("HighScore", score);
        }
        else
            PlayerPrefs.SetInt("HighScore", score);

        highScore = PlayerPrefs.GetInt("HighScore");
    }
    
    public void PlayAgain()
    {
        SceneManager.LoadScene(0);
    }
}
