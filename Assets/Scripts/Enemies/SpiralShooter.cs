using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiralShooter : Enemy
{
    private Bullet bulletPrefab;
    private float rotationSpeed;
    private float timeSinceLastShot;
    private float timeBetweenShots; 
    
    Camera mainCamera;

    [SerializeField] private Animator anim;

    protected override void Start()
    {

        //base.Start();
        mainCamera = Camera.main;
        health = new Health(1, 1, 0);
        SetEnemyType(EnemyType.SpiralShooter);

    }

    protected override void Update()
    {

  
        transform.Rotate(Vector3.forward, -rotationSpeed * Time.deltaTime);

        timeSinceLastShot += Time.deltaTime;
        Debug.Log($"timeSinceLastShot:{timeSinceLastShot}, timeBetweenShots: {timeBetweenShots}");

        if (timeSinceLastShot >= timeBetweenShots)
        {
            Shoot();
            timeSinceLastShot = 0f;
        }

    }

    public override void Shoot()//Vector3 direction, float speed
    {
        weapon.Shoot(bulletPrefab, this, "Player");
    }

    public void SetSpiralShooter(float _rotationSpeed,float _timeSinceLastShot,float _timeBetweenShots, Bullet _bulletPrefab)
    {
        rotationSpeed = _rotationSpeed;
        timeSinceLastShot = _timeSinceLastShot;
        timeBetweenShots = _timeBetweenShots;
        bulletPrefab = _bulletPrefab;

    }

    public override void Die()
    {
        Destroy(this.gameObject, 0.6f);
        anim.SetBool("isDead", true);
    }
}

