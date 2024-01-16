using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour
{
    //need updated player position, need enemyposition
    private Transform player;
    private Transform enemy;

    private LineRenderer lineRenderer;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    public void SetUpLaser(Transform player, Transform enemy)
    {
        //Debug.Log("set up laser");
        this.player = player;
        this.enemy = enemy;
    }

    private void Update()
    {
        if (this.player != null && this.enemy != null)
        {
            lineRenderer.SetPosition(0, player.position);
            lineRenderer.SetPosition(1, enemy.position);
        }
        

        
    }
}
