using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockManager : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject basicBlockPref;
    [SerializeField] private GameObject[] blockDisplay;

    private int blockNumInHold = 3;
    private int maxBlock = 3;
    private float cooldownDuration = 3.0f; // Cooldown duration in seconds
    private float cooldownTimer = 0.0f; // Timer to track cooldown
    private bool isCooldownActive = false;

    private static BlockManager instance;
    private bool startActive;

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

    private void Start()
    {
        for (int i = 0; i < blockDisplay.Length; i++)
        {
            blockDisplay[i].SetActive(false);
        }
        startActive = false;


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
        bool BlockEnabled = GameManager.GetInstance().levelManager.GetPowerUp()[2];
        if (BlockEnabled)
        {

            if (!startActive)
            {
                startActive = true;
                //Debug.Log("visible the block display");
                for (int i = 0; i < blockDisplay.Length; i++)
                {
                    blockDisplay[i].SetActive(true);
                }

            }
        }
        


    }

    public void SpawnBlock()
    {
        
        if (blockNumInHold == 0 || !startActive)
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
        //Debug.Log($"Spawn basic Block with position {player.transform.position} and rotation {player.transform.rotation}");
        GameObject newBlock = Instantiate(basicBlockPref, player.transform.Find("BlockSpawnPoint").position, player.transform.rotation);
    }
    public void AddBlock()
    {
        //Debug.Log("addblock");
        if (blockNumInHold == 3)
        {
            return;
        }
        //Debug.Log("enable Block for player");
        blockDisplay[blockNumInHold].GetComponent<Image>().enabled = true;
        blockNumInHold++;
    }


    public void UpdateRemainBlock()
    {
        //Debug.Log("UpdateRemainBlock");
        blockDisplay[blockNumInHold-1].GetComponent<Image>().enabled = false;
        blockNumInHold--;
        
    }

    public void RemoveBlockInScene()
    {
        //if blocks destroyed in the scene
        isCooldownActive = true;//start the cooldown process to generate new for player to use
        blockInScene -= 1;
    }

    public void ResetBlock()
    {
        //Debug.Log("reset block");
        isCooldownActive = true;//start the cooldown process to generate new for player to use
        blockInScene = 0;
        blockNumInHold = 3;
        for (int i = 0; i < blockDisplay.Length; i++)
        {
            blockDisplay[i].GetComponent<Image>().enabled = true;
        }
    }


}
