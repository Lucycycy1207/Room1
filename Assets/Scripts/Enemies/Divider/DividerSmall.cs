using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DividerSmall : Enemy
{
    private float attackRange;
    private float shootingRate;

    private Bullet bulletPrefab;


    private float timer = 0;

    [SerializeField] private Animator anim;


    protected override void Start()
    {
        base.Start();//define target
        health = new Health(1, 1, 0);

    }

    protected override void Update()
    {

        base.Update();//move object
        if (target == null)
        {
            return;
        }


        if (Vector2.Distance(transform.position, target.position) < attackRange)
        {
            Attack(shootingRate);
        }

    }

    public override void Move(Vector2 targetPosition)
    {
        base.Move(targetPosition);
        if (Vector2.Distance(transform.position, target.position) > attackRange)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
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
            Shoot();
        }

    }
    public override void Shoot()//Vector3 direction, float speed
    {
        weapon.Shoot(bulletPrefab, this, "Player");
    }

    public void SetDividerSmall(float _attackRange, float _shootingRate, Bullet _bulletPrefab)
    {

        this.attackRange = _attackRange;
        this.shootingRate = _shootingRate;
        this.bulletPrefab = _bulletPrefab;
    }

    public override void Die()
    {
        Destroy(this.gameObject,0.6f);
        anim.SetBool("isDead", true);

    }

}
