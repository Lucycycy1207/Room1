using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PickupSpawner : PickUp
{
    [SerializeField] private Player player;

    public override void OnPicked(GameObject gameObject)
    {
        base.OnPicked(gameObject);
    }
    public void OnPickedNuke(GameObject gameObject)
    {
        base.OnPicked(gameObject);
        player.AddNuke();
    }
    public void OnPickedGunPower(GameObject gameObject)
    {
        base.OnPicked(gameObject);
        player.AddGunPower();
    }
}
