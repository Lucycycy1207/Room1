using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviour
{
    public UnityEvent OnScoreUpdated;
    private int score;

    private int highScore;

    public int GetHighScore()
    {
        return highScore;
    }

    public int GetScore()
    {
        return score;
        
    }

    public void IncrementScore()
    {
        score++;
        SetHighScore();
        Debug.Log("increment score: " + score);
        OnScoreUpdated?.Invoke();
    }

    public void SetScore(int value)
    {
        score = value;
        Debug.Log("set score: " + score);
        OnScoreUpdated?.Invoke();
    }

    private void SetHighScore()
    {
        if (score >= highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HIGHSCORE", highScore);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("HIGHSCORE"))
        {
            highScore = PlayerPrefs.GetInt("HIGHSCORE");
        }
        else
        {
            PlayerPrefs.SetInt("HIGHSCORE", highScore);
        }
        OnScoreUpdated?.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
