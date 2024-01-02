using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BasicBlock : BlockObject
{
    private float maxHealth;
    [SerializeField] private GameObject BasicBlockPref;

    private void Start()
    {
        health = new Health(100,100,0);
    }
    public override void SpawnBlock()
    {
        //GameObject target = GameObject.FindWithTag("Player");
        //Vector2 offset = new Vector2(2,2);
        //Debug.Log($"Spawn basic Block with position {target.transform.position} and rotation {target.transform.rotation}");
        //Instantiate(BasicBlockPref, target.transform.Find("BlockSpawnPoint").position, target.transform.rotation);
    }

    public override void Destroy()
    {
        Debug.Log("block dies");
        Destroy(gameObject);
    }

    public void SetBasicBlock(float _maxHealth)
    {
        maxHealth = _maxHealth;
    }
}

