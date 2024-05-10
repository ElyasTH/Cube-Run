using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private bool _isGameFinished;

    [Header("Score")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private float scoreIncrementTime = 0.5f;
    private int score;
    private float _elapsedTime;

    private void Awake()
    {
        if (!instance) instance = this;
    }

    private void Update()
    {
        if (!_isGameFinished)
        {
            _elapsedTime += Time.deltaTime;
            if (_elapsedTime >= scoreIncrementTime)
            {
                score++;
                scoreText.text = score.ToString();
                _elapsedTime = 0;
            }
        }
    }

    public void FinishGame()
    {
        _isGameFinished = true;
        SoundManager.instance.STOP_MUSIC();
    }
}
