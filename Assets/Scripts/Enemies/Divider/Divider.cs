using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Divider : Enemy
{
    private float attackRange;
    private float shootingRate;

    [SerializeField] private GameObject DividerSmallPref;
    private GameObject tempEnemy;
    private Weapon DividerSmallGun = new Weapon("DividerSmallGun", 1f, 5f);

    private Bullet bulletPrefab;

    private Vector3 screenPoint;
    Camera mainCamera;

    private float timer = 0;

    //Camera mainCamera;
    private bool inScene = false;

    protected override void Start()
    {
        base.Start();//define target
        mainCamera = Camera.main;
        health = new Health(1, 1, 0);
        SetEnemyType(EnemyType.Divider);
        //mainCamera = Camera.main;

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

    public void SetDivider(float _attackRange, float _shootingRate, Bullet _bulletPrefab)
    {
        this.attackRange = _attackRange;
        this.shootingRate = _shootingRate;
        this.bulletPrefab = _bulletPrefab;
    }

    public override void Die()
    {

        //store data
        Transform DividerTransform = this.transform;

        // death BigEnemy
        Destroy(this.gameObject);




        // split
        //Debug.Log("should be split");
        float PosOffset = 0.5f;
        
        InitiateDividerSmallEnemy(DividerTransform, PosOffset);
        InitiateDividerSmallEnemy(DividerTransform, -PosOffset);





    }

    private void InitiateDividerSmallEnemy(Transform DividerTransform,float PosOffset)
    {
        tempEnemy = Instantiate(DividerSmallPref);
        tempEnemy.transform.position = DividerTransform.position+DividerTransform.right*PosOffset;
        tempEnemy.GetComponent<DividerSmall>().SetDividerSmall(attackRange, shootingRate, bulletPrefab);
        tempEnemy.GetComponent<DividerSmall>().weapon = DividerSmallGun;
    }
}
