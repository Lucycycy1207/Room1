using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private int currentLevel; // Track current level
    private int maxLevel;
    private GameObject currChest;
    private int checkedLevel;
    [SerializeField] GameObject levelTrigger;
    [SerializeField] GameObject arrow;
    [SerializeField] GameObject chestPref;
    [SerializeField] GameObject BlockPref;
    private bool NukeEnabled;
    private bool GunPowerEnabled;
    private bool BLockEnabled;
    void Start()
    {
        currentLevel = 1;
        levelTrigger.SetActive(false);
        arrow.SetActive(false);
        LoadLevel(currentLevel); // Load the initial level
        checkedLevel = -1;

        
        GunPowerEnabled = false;
        NukeEnabled = false;
        BLockEnabled = false;


    }
    /// <summary>
    /// return NukeEnabled, GunPowerEnabled, BLockEnabled
    /// </summary>
    /// <returns></returns>
    public bool[] GetPowerUp()
    {
        return new bool[] { NukeEnabled, GunPowerEnabled, BLockEnabled };
    }

    public void setBlockEnabled()
    {
        BLockEnabled = true;
    }
    public void setNukeEnabled()
    {
        NukeEnabled = true;
    }
    public void setGunPowerEnabled()
    {
        GunPowerEnabled = true;
    }

    
    public void LoadLevel(int level)
    {
        currentLevel = level;
        Debug.Log($"Load new level: {level}");
        GameManager.GetInstance().UIManager.UpdateLevel();
        if (level <= 3)
        {
            currChest = Instantiate(chestPref);
            currChest.SetActive(false);
        }
        


        // Load level elements based on level data
        // Limit enemy spawns, Player Health, initialize score.

        //Reset player position to centre
        GameManager.GetInstance().player.GetComponent<Player>().ResetPlayerTranform();
        
        //Destroy all collectables

    }

    public int GetCurrLevel()
    {
        return currentLevel;
    }

    public void SpawnNewPowerUp(Vector2 transformPos)
    {
        Debug.Log("spawn new weapon");
       
        if (currentLevel == 1)
        {
            GameObject GunPowerPref = GameManager.GetInstance().GetGunPowerPrefab();
            Instantiate(GunPowerPref, transformPos, Quaternion.identity);
            GunPowerEnabled = true;
            Debug.Log("GunPowerEnabled ");
        }
        else if (currentLevel == 2)
        {
            GameObject NukePref = GameManager.GetInstance().GetNukePrefab();
            Instantiate(NukePref, transformPos, Quaternion.identity);
            NukeEnabled = true;
            Debug.Log("NukeEnabled ");
        }
        else if (currentLevel == 3)
        {
            Debug.Log("activate blocks");
            Debug.Log("BLockEnabled ");
            Instantiate(BlockPref, transformPos, Quaternion.identity);
        }


    }

private void Update()
{
        if (checkedLevel == currentLevel)
        {
            return;
        }

        //TODO: boss level

        //for enemy levels 1-7
        if (GameManager.GetInstance().EnemyKilledInLevel(currentLevel) <= GameManager.GetInstance().scoreManager.GetScore())
        {
            
            if (currentLevel == maxLevel)
            {
                Debug.Log("all level completed");
                return;
            }
            checkedLevel = currentLevel;

            levelTrigger.SetActive(true);
            arrow.SetActive(true);
            if (currentLevel <= 3)
            {
                currChest.SetActive(true);
            }
            
            Debug.Log($"current level: {currentLevel}, checkedLevel: {checkedLevel}");

            GameManager.GetInstance().restoreLevelScore();
            GameManager.GetInstance().cleanScene();
            
        }
        

        
    }


    public void TriggerLevel()
    {
        // Check for level transition triggers
        // Update currentLevel and call LoadLevel() for the next level
        Debug.Log("touched levelTrigger");
        GameManager.GetInstance().PauseEnemySpawning = false;
        levelTrigger.SetActive(false);
        arrow.SetActive(false);
        LoadLevel(currentLevel + 1);  
    }

    // Other level progression and management logic

    public void SetMaxLevel(int _maxLevel)
    {
        maxLevel = _maxLevel;
    }
    public int GetMaxLevel()
    {
        return maxLevel;
    }
}
