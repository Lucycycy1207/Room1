using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGun : Enemy
{
    private float attackRange;
    private float shootingRate;

    private Bullet bulletPrefab;

    //[SerializeField] public float shootingTime;
    //[SerializeField]  public float shootingCoolDown;
    private Vector3 screenPoint;
    Camera mainCamera;

    private float timer = 0;

    //Camera mainCamera;
    private bool inScene = false;

    [SerializeField] private Animator anim;
    protected override void Start()
    {
        base.Start();//define target
        mainCamera = Camera.main;
        health = new Health(1, 1, 0);
        SetEnemyType(EnemyType.MachineGun);
        //mainCamera = Camera.main;
        
    }

    protected override void Update()
    {

        base.Update();//move object
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
        if (inScene == true) {
            if (target == null)
            {
                return;
            }
            if (Vector2.Distance(transform.position, target.position) < attackRange)
            {
                Attack(shootingRate);
            }
        }
    }

    public override void Move(Vector2 targetPosition)
    {
        base.Move(targetPosition);
        if (Vector2.Distance(transform.position, target.position) > attackRange || !inScene)
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

    public void SetMachineGun(float _attackRange, float _shootingRate, Bullet _bulletPrefab)
    {
        this.attackRange = _attackRange;
        this.shootingRate = _shootingRate;
        this.bulletPrefab = _bulletPrefab;
    }

    public override void Die()
    {
        anim.SetBool("IsDead", true);
        base.Die();
    }
}
