using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Increase player health when object getdamaged or picked up.
/// </summary>
public class HealthPickup : PickUp, IDamageable
{
    public void GetDamage(float damage)
    {
        //Increase Player Health
    }

    public override void OnPicked(GameObject gameObject)
    {
        base.OnPicked(gameObject);

        //Increase Player Health here!

    }
}
