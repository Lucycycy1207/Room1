using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy
{
    private float attackRange;
    private float attackTime;
    private float damage;

    private float timer = 0;

    protected override void Start()
    {
        base.Start();
        health = new Health(1, 1, 0);
        SetEnemyType(EnemyType.Melee);
    }

    protected override void Update()
    {
        base.Update();
        if (target == null)
        {
            return;
        }
        
        if (Vector2.Distance(transform.position, target.position) < attackRange)
        {
            Attack(attackTime);
        }
    }

   
    public override void Attack(float interval)
    {
        base.Attack(interval);
        if (timer <= interval)
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer = 0;
            target.GetComponent<IDamageable>().GetDamage(damage);
            //target.GetComponent<IDamageable>().GetDamage(wepon.GetDamage())
        }

    }

    public override void GetDamage(float damage)
    {
        base.GetDamage(damage);
    }

    public void SetMeleeEnemy(float _attackRange, float _attackTime, float _damage)
    {
        this.attackRange = _attackRange;
        this.attackTime = _attackTime;
        this.damage = _damage;
    }
}
