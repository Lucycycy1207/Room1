using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiralShooter : Enemy
{
    public float moveSpeed = 5.0f;
    public float rotationSpeed = 30f; // Adjust the speed as needed
    private Bullet bulletPrefab;
    //public Transform bulletSpawnPoint;
    public Transform[] targetLocations;

    private float timeSinceLastShot = 0f;
    private float timeBetweenShots = 0.2f; // Adjust the time between shots as needed
    private int currentTargetIndex = 0;

    Camera mainCamera;

    protected override void Start()
    {
     
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
        //transform.position = Vector3.MoveTowards(transform.position, targetLocations[currentTargetIndex].position, moveSpeed * Time.deltaTime);

        //if (Vector3.Distance(transform.position, targetLocations[currentTargetIndex].position) < 0.1f)
        //{
        //    // Switch to the next target location
        //    currentTargetIndex = (currentTargetIndex + 1) % targetLocations.Length;
        //}

    }

    public override void Shoot()//Vector3 direction, float speed
    {
        weapon.Shoot(bulletPrefab, this, "Player");
    }

    public void SetSpiralShooter(float _timeBetweenShots, Bullet _bulletPrefab)
    {
        timeBetweenShots = _timeBetweenShots;//0.1f
        bulletPrefab = _bulletPrefab;
    }
}


