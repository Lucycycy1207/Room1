using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Boomer : Enemy
{
    private float BoomRadius = 4f;
    private float damage;
    private Bullet bulletPrefab;
    private int numberOfBullets = 8;


    protected override void Start()
    {
        base.Start();
        SetEnemyType(EnemyType.Boomer);
        health = new Health(1, 1, 0);
    }

    protected override void Update()
    {
        base.Update();
        //Debug.Log($"position: {transform.position}, target: {target.position}");
        //Debug.Log($"distance: {Vector2.Distance(transform.position, target.position)}");
        if (Vector2.Distance(transform.position, target.position) <= BoomRadius)
        { 
            Boom();
        }
        
    }

    private void Boom()
    {
        float angleStep = 360.0f / numberOfBullets;
        Bullet [] newBullet = new Bullet[numberOfBullets];
        for (int i = 0; i < numberOfBullets; i++)
        {
            float angle = i * angleStep;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            newBullet[i] = GameObject.Instantiate(bulletPrefab, this.transform.position, rotation);
            newBullet[i].SetBullet(damage, "Player", bulletPrefab.GetSpeed());
            GameObject.Destroy(newBullet[i].gameObject, 5.0f); // Destroy each bullet after 5 seconds
            
        }

        Destroy(gameObject);
    }

    public override void GetDamage(float damage)
    {
        Boom();
        base.GetDamage(damage);
    }

    public void SetBoomer(float _BoomRadius, float _damage, Bullet _bulletPrefab)
    {
        Debug.Log("set boomer");
        this.BoomRadius = _BoomRadius;
        this.damage = _damage;
        this.bulletPrefab = _bulletPrefab;
    }

}
