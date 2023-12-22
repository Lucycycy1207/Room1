using UnityEngine;

public abstract class PlayableObject : MonoBehaviour, IDamageable
{
    public Health health = new Health();
    public Weapon weapon;

    public abstract void Move(Vector2 direction, Vector2 target);

    //Add Methods to implement polymorphism
    public virtual void Move(Vector2 direction) { }
    public virtual void Move(float speed) { }

    public abstract void Shoot();
    public abstract void Die();
    public abstract void Attack(float interval);


    /// <summary>
    /// Reduce object health and check die condition.
    /// </summary>
    /// <param name="damage">The amount of damage to object.</param>
    public virtual void GetDamage(float damage)
    {
        health.DeductHealth(damage);
        //Debug.Log("current health: " + health.GetHealth());
        if (health.GetHealth() <= 0)
        {
            Die();
        }
    }
}
