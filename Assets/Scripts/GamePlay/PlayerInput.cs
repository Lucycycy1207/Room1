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

    /// <summary>
    /// This variable represents position of mouse.
    /// </summary>
    private Vector2 lookTarget;
    
    // Start is called before the first frame update
    void Start()
    {
        
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
            player.Shoot();
        }
        else if (Input.GetMouseButtonDown(1))
        {
            player.UseNuke();
        }

    }

    private void FixedUpdate()
    {
        player.Move(new Vector2(horizontal, vertical), lookTarget);
    }

}
