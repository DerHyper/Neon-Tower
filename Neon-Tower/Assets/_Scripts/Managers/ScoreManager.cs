using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public TMP_Text scoreText;
    public float Score {get; private set;}
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
    private void Start() {
        Score = 0;
    }
    private void FixedUpdate()
    {
        RemoveScore(Time.fixedDeltaTime);
    }

    public void AddScore(int amount)
    {
        Score += amount;
        UpdateScoreText();
    }

    public void RemoveScore(float amount)
    {
        if (Score - amount > 0)
        {
            Score -= amount;
        }
        else
        {
            Score = 0;
        }
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        string newScoreText = "Score: " + ((int)Score).ToString();
        scoreText.text = newScoreText;
    }
}
