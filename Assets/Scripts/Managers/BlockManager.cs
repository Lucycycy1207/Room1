using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockManager : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject basicBlockPref;
    [SerializeField] private Image[] blockDisplay;

    private int blockNum = 3;
    private int maxBlock = 3;
    private float cooldownDuration = 3.0f; // Cooldown duration in seconds
    private float cooldownTimer = 0.0f; // Timer to track cooldown
    private bool isCooldownActive = false;

    private static BlockManager instance;

    private int blockInScene = 0;
    public static BlockManager GetInstance()
    {
        return instance;
    }
    private void SetSingleton()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }

        instance = this;
    }

    private void Awake()
    {
        SetSingleton();
    }

    private void Update()
    {
        //Some blocks in used/ after press Q
        if (isCooldownActive)
        {
            cooldownTimer += Time.deltaTime;

            //add Block to player after coolDown duration
            if (cooldownTimer >= cooldownDuration)
            {
                AddBlock();
                cooldownTimer = 0;
                isCooldownActive = false;
            }
        }
        

    }

    public void SpawnBlock()
    {
        if (blockNum == 0)
        {
            return;
        }
        SpawnBasicBlock();
        blockInScene += 1;
        UpdateRemainBlock();
    }

    public void SpawnBasicBlock()
    {
        GameObject target = GameObject.FindWithTag("Player");
        Vector2 offset = new Vector2(2, 2);
        Debug.Log($"Spawn basic Block with position {player.transform.position} and rotation {player.transform.rotation}");
        GameObject newBlock = Instantiate(basicBlockPref, player.transform.Find("BlockSpawnPoint").position, player.transform.rotation);
    }
    public void AddBlock()
    {
        if (blockNum == 3)
        {
            return;
        }
        Debug.Log("enable Block for player");
        blockDisplay[blockNum].GetComponent<Image>().enabled = true;
        blockNum++;
    }


    public void UpdateRemainBlock()
    {
        Debug.Log("UpdateRemainBlock");
        blockDisplay[blockNum-1].GetComponent<Image>().enabled = false;
        blockNum--;
        
    }

    public void RemoveBlockInScene()
    {
        //if blocks destroyed in the scene
        isCooldownActive = true;//start the cooldown process to generate new for player to use
        blockInScene -= 1;
    }


}
