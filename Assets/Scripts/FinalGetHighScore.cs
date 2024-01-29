using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FinalGetHighScore : MonoBehaviour
{
    [SerializeField] private TMP_Text finalScore;

    private int highScore;
    void Start()
    {
        if (PlayerPrefs.HasKey("HIGHSCORE"))
        {
            highScore = PlayerPrefs.GetInt("HIGHSCORE");
            finalScore.SetText("HIGHSCORE: " + highScore);
        }
    }
}

