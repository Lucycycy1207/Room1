using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


/// <summary>
/// Monitor player key and mouse press.
/// </summary>
public class PlayerInput : MonoBehaviour
{
    
    private Player player;
    private float horizontal, vertical;

    public BlockManager blockManager;
    /// <summary>
    /// This variable represents position of mouse.
    /// </summary>
    private Vector2 lookTarget;

    public UnityEvent GunPowerShootingOn;
    public UnityEvent GunPowerShootingOff;

    private bool GunPowerMode;
    // Start is called before the first frame update
    void Start()
    {
        GunPowerMode = false;
        player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        lookTarget = Input.mousePosition;

        if (Input.GetMouseButtonDown(0))
        {
            if (GunPowerMode)
            {
                //Debug.Log("player shooting in power mode");
                GunPowerShootingOn?.Invoke();
            }
            else
            {
                player.Shoot();
            }
            
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (GunPowerMode)
            {
                //Debug.Log("player stop shooting in power mode");
                GunPowerShootingOff?.Invoke();
            }
        }
        else if (Input.GetMouseButtonDown(1))
        {
            player.UseNuke();
        }

        else if (Input.GetKeyDown(KeyCode.Q))
        {
            //Debug.Log("pressed Q");
            blockManager.SpawnBlock();
        }

    }

    private void FixedUpdate()
    {
        
        player.Move(new Vector2(horizontal, vertical), lookTarget);
    }

    public void ActiveGunPowerMode()
    {
        GunPowerMode = true;
    }
    public void DeactiveGunPowerMode()
    {
        GunPowerMode = false;
    }

}
