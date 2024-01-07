using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private int currentLevel = 1; // Track current level

    void Start()
    {
        LoadLevel(currentLevel); // Load the initial level
    }

    public void LoadLevel(int level)
    {
        currentLevel = level;
        Debug.Log($"Load new level: {level}");
        // Load level elements based on level data
        // Limit enemy spawns, Player Health, initialize score.

    }

    public int GetCurrLevel()
    {
        return currentLevel;
    }


    void OnTriggerEnter(Collider other)
    {
        // Check for level transition triggers
        // Update currentLevel and call LoadLevel() for the next level
    }

    // Other level progression and management logic
}
