using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : Enemy
{

    private float attackRange;
    private float shootingRate;

    private Bullet bulletPrefab;

    private GameObject laser;
    private GameObject square;
    private float timer = 0;

    private bool InScene = false;
    private Camera mainCamera;
    private Vector3 screenPoint;

    protected override void Start()
    {
        mainCamera = Camera.main;
        SetEnemyType(EnemyType.Shooter);
        base.Start();
        laser = gameObject.transform.GetChild(0).gameObject;
        square = gameObject.transform.GetChild(1).gameObject;   
        laser.GetComponent<LaserController>().SetUpLaser(target, this.transform);
        laser.SetActive(false);
        health = new Health(1, 1, 0);
    }

    protected override void Update()
    {
        base.Update();//move object
        if (target == null)
        {
            return;
        }
        screenPoint = mainCamera.WorldToViewportPoint(this.transform.position);

        if (screenPoint.x >= 0 && screenPoint.x <= 1 &&
            screenPoint.y >= 0 && screenPoint.y <= 1 &&
            screenPoint.z > 0)
        {
            InScene = true;
        }
        if (InScene)
        {
            if (Vector2.Distance(transform.position, target.position) < attackRange
                && square.GetComponent<Renderer>().isVisible)
            {

                laser.SetActive(true);
                Attack(shootingRate);
            }
            else
            {
                laser.SetActive(false);
            }
        }
        
    }

    public override void Move(Vector2 direction)
    {
        base.Move(direction);

        if (Vector2.Distance(transform.position, target.position) > attackRange || !InScene)
        {
            transform.Translate(Vector2.right * 1 * Time.deltaTime);
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

    public override void GetDamage(float damage)
    {
        base.GetDamage(damage);
        if (health.GetHealth() <= 0)
        {
            Destroy(laser);
        }
    }

    public void SetShooter(float _attackRange, float _shootingRate, Bullet _bulletPrefab)
    {
        this.attackRange = _attackRange;
        this.shootingRate = _shootingRate;
        this.bulletPrefab = _bulletPrefab;
        
    }


}

