using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private float scoreIncrementTime;
    private int score;
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
    }
}
