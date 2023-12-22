using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exploder : Enemy
{
    private float explodeRadius = 1f;
    private float damage;
    
    

    protected override void Start()
    {
        base.Start();
        SetEnemyType(EnemyType.Exploder);
        health = new Health(1, 1, 0);
    }

    protected override void Update()
    {
        base.Update();
        if (target == null)
        {
            return;
        }
        
        if (Vector2.Distance(transform.position, target.position) <= explodeRadius)
        {
            Explode(explodeRadius);
        }
        
    }

    public void Explode(float radius)
    {
        Debug.Log($"explode with radius {radius}");
        
        target.GetComponent<IDamageable>().GetDamage(damage);
        Destroy(gameObject);
    }
    public override void GetDamage(float damage)
    {
        base.GetDamage(damage);
    }
    public void SetExploder(float _explodeRadius, float _damage)
    {
        this.explodeRadius = _explodeRadius;
        this.damage = _damage;
    }
    




}