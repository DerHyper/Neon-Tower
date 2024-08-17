using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public TMP_Text scoreText;
    private float _score = 0;
    public static ScoreManager Instance { get; private set; }

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    private void FixedUpdate()
    {
        RemoveScore(Time.fixedDeltaTime);
    }

    public void AddScore(int amount)
    {
        _score += amount;
        UpdateScoreText();
    }

    public void RemoveScore(float amount)
    {
        if (_score - amount > 0)
        {
            _score -= amount;
        }
        else
        {
            _score = 0;
        }
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        string newScoreText = "Score: " + ((int)_score).ToString();
        scoreText.text = newScoreText;
    }
}
