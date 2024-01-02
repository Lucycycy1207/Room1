using UnityEngine;

public class Enemy: PlayableObject
{
    private string enemyName;
    /// <summary>
    /// Enemy moving speed;
    /// </summary>
    [SerializeField] protected float speed;
    
    
    private EnemyType enemyType;

    /// <summary>
    /// The transform of the object that enemy will attack to.
    /// </summary>
    protected Transform target;

    

    protected virtual void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        
    }


    protected virtual void Update()
    {
        if (target != null)
        {
           
            Move(target.position);
        }
        else
        {
            Move(speed);
        }
    }
    public override void Shoot()
    {
        Debug.Log($"Shooting a bullet towards");
        //Debug.Log($"Shooting a bullet towards {direction} with a speed of {speed}");
    }

    public override void Die()
    {
        //Debug.Log("Enemy Died");
        
        //generate Nuke

        float randomValue = Random.Range(0f, 1f);
        GameObject NukePref = GameManager.GetInstance().GetNukePrefab();
        GameObject GunPowerPref = GameManager.GetInstance().GetGunPowerPrefab();
        
        float NukeProb = GameManager.GetInstance().GetNukeSpawnProb();
        float GunPowerProb = GameManager.GetInstance().GetGunPowerSpawnProb();
        //Debug.Log(NukeProb + GunPowerProb);
        if (randomValue < NukeProb)
        {
            Instantiate(NukePref, transform.position, Quaternion.identity);
        }
        //if (randomValue < NukeProb + GunPowerProb)
        else
        {
            Instantiate(GunPowerPref, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);


    }

    public override void Attack(float interval)
    {
        //Debug.Log($"Enemy attacking with a {interval} interval");
    }


    public void SetEnemyType(EnemyType _enemyType)
    {
        enemyType = _enemyType;
    }

    public override void Move(Vector2 direction, Vector2 target)
    {
    }

    /// <summary>
    /// Currently move to right only.
    /// </summary>
    /// <param name="speed">Moving speed</param>
    public override void Move(float speed) // only move to right?
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    /// <summary>
    /// Rotate Enemy and move to right.
    /// </summary>
    /// <param name="targetPosition">Target position.</param>

    public override void Move(Vector2 targetPosition)
    {
        targetPosition.x -= transform.position.x;
        targetPosition.y -= transform.position.y;

        float angle = Mathf.Atan2(targetPosition.y, targetPosition.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        if (enemyType == EnemyType.Melee || enemyType == EnemyType.Exploder || enemyType == EnemyType.Boomer)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
        
    }


    public override void GetDamage(float damage)
    {
        base.GetDamage(damage);
    }

}
