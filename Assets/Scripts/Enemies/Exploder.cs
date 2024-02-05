using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exploder : Enemy
{
    private float explodeRadius = 1f;
    private float damage;


    [SerializeField] private Animator anim;
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
            anim.SetBool("IsDead", true);
            Explode(explodeRadius);
        }
        
        
    }

    public void Explode(float radius)
    {
        //Debug.Log($"explode with radius {radius}");
        anim.SetBool("IsDead", true);
        target.GetComponent<IDamageable>().GetDamage(damage);
        Destroy(gameObject, 0.6f);
    }
    public override void GetDamage(float damage)
    {
        base.GetDamage(damage);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Block"))
        {
            collision.gameObject.GetComponent<IDamageable>().GetDamage(damage);
            anim.SetBool("IsDead", true);
            Destroy(gameObject, 0.6f);
        }
    }
    public void SetExploder(float _explodeRadius, float _damage)
    {
        this.explodeRadius = _explodeRadius;
        this.damage = _damage;
    }
    




}
