using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Boomer : Enemy
{
    private float BoomRadius = 2f;
    private float damage;
    private Bullet bulletPrefab;
    private int numberOfBullets = 8;

    private Vector3 screenPoint;
    Camera mainCamera;
    private bool inScene = false;

    [SerializeField] private Animator anim;

    protected override void Start()
    {
        base.Start();
        SetEnemyType(EnemyType.Boomer);
        health = new Health(1, 1, 0);
        mainCamera = Camera.main;
    }
    
    protected override void Update()
    {
        base.Update();
        if (target == null)
        {
            return;
        }
        //check if enemy in Scene
        screenPoint = mainCamera.WorldToViewportPoint(this.transform.position);

        if (screenPoint.x >= 0 && screenPoint.x <= 1 &&
            screenPoint.y >= 0 && screenPoint.y <= 1 &&
            screenPoint.z > 0)
        {
            // The object is within the camera's view
            inScene = true;
        }
        if (inScene)
        {
            if (Vector2.Distance(transform.position, target.position) <= BoomRadius)
            {
                anim.SetBool("isDead", true);
                Boom();    
            }
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
        anim.SetBool("isDead", true);
        Destroy(gameObject);
        
    }

    public override void GetDamage(float damage)
    {
        anim.SetBool("isDead", true);
        Boom();
        base.GetDamage(damage);
    }

    public void SetBoomer(float _BoomRadius, float _damage, Bullet _bulletPrefab)
    {
        //Debug.Log("set boomer");
        this.BoomRadius = _BoomRadius;
        this.damage = _damage;
        this.bulletPrefab = _bulletPrefab;
    }

}
