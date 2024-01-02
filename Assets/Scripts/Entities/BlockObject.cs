using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Abstruct class for Blocks. It's a blocker for player to block damage.
/// </summary>
public abstract class BlockObject : MonoBehaviour, IDamageable
{

    public Health health = new Health();

    public abstract void SpawnBlock();

    public abstract void Destroy();

    /// <summary>
    /// Reduce Block health and check destroy condition.
    /// </summary>
    /// <param name="damage">The amount of damage to object.</param>
    public virtual void GetDamage(float damage)
    {
        health.DeductHealth(damage);
        //Debug.Log("Deduct block health: " + health.GetHealth());
        if (health.GetHealth() <= 0)
        {
            Destroy();
        }
    }
}
