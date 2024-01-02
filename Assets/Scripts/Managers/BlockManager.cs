using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject basicBlockPref;
    // Start is called before the first frame update
    public void SpawnBasicBlock()
    {
        GameObject target = GameObject.FindWithTag("Player");
        Vector2 offset = new Vector2(2, 2);
        Debug.Log($"Spawn basic Block with position {player.transform.position} and rotation {player.transform.rotation}");
        GameObject newBlock = Instantiate(basicBlockPref, player.transform.Find("BlockSpawnPoint").position, player.transform.rotation);
    }
}
