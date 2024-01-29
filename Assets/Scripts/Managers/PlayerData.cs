using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerData : MonoBehaviour
{
    private void Start()
    {
        int maxLevel = GameManager.GetInstance().levelManager.GetMaxLevel();
        PlayerPrefs.SetInt("LEVELSCORE", 0);
    }

    /// <summary>
    /// called after completing each level.
    /// </summary>
    /// <param name="level"></param>
    /// <param name="score"></param>
    public void UpdateHistoryLevelScore(int score)
    {
        PlayerPrefs.SetInt("LEVELSCORE", score);
        //Debug.Log("update history: " + score);
    }

    public int GetHistoryScore()
    {
        return PlayerPrefs.GetInt("LEVELSCORE");
    }
}
