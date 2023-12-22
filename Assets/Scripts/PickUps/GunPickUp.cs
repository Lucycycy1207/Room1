using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPickUp : PickUp
{
    public override void OnPicked(GameObject gameObject)
    {
        base.OnPicked(gameObject);
        
        //Add a gun to the player here!
    }
}
