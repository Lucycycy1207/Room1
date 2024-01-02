using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Player: PlayableObject
{
    [Header("Player Entities")]
    private string nickName;
    [SerializeField] private Camera cam;
    [SerializeField] private float speed;

    [Header("Weapon Entities")]
    [SerializeField] private float weaponDamage = 1;
    [SerializeField] private float bulletSpeed = 10.0f;
    [SerializeField] private Bullet bulletPrefab;
    private Health playerHealth;

    [Header("Other Scripts")]
    [SerializeField] private PickupSpawner pickUpSpawner;
    [SerializeField] private GunPower gunPower;

    [Header("PickUps")]
    [SerializeField] private GameObject[] nukeDisplay;
    [SerializeField] private GameObject GunPowerImg;
    private int nukeNum = 0;
    private int maxNuke = 3;

    private int gunPowerMode = 0;

    private float shootTimer = 0;
    private float shootinterval;
    private bool OnShootingPower = false;




    private Rigidbody2D playerRB;
    public float GetHealth()
    {
        return playerHealth.GetHealth();
    }

    public override void Shoot()//Vector3 direction, float speed
    {
        weapon.Shoot(bulletPrefab, this, "Enemy");
    }


    public override void Die()
    {
        Debug.Log("Player Died");
        
        Destroy(this.gameObject);
    }

    private void Start()
    {
        playerHealth = new Health(100f, 100f, 0.5f);
        GameManager.GetInstance().UIManager.UpdateHealth();
        //health = new Health(100f, 100f, 0.5f);
        playerRB = GetComponent<Rigidbody2D>();
        Debug.Log("Player health value is " + playerHealth.GetHealth());
        //Set Player Weapon
        weapon = new Weapon("Player Weapon", weaponDamage, bulletSpeed);
        GunPowerImg.SetActive(false);
        
    }

    public void SetGunPowerShootingOn()
    {
        OnShootingPower = true;
    }
    public void SetGunPowerShootingOff()
    {
        OnShootingPower = false;

    }

    private float shootingRate = 0.1f;
    private float timer = 0;
    private void Update()
    {
        if (gunPowerMode == 1)
        {
            if (shootTimer <= shootinterval)
            {
                timer += Time.deltaTime;
                shootTimer += Time.deltaTime;
                //
                //
                if (OnShootingPower)
                {
                    if (timer > shootingRate)
                    {
                        Shoot();
                        timer = 0;
                    }
                        
                }
                
            }
            else
            {
                this.gameObject.GetComponent<PlayerInput>().DeactiveGunPowerMode();
                gunPowerMode = 0;
                gunPower.ResetGunPowerImg();
                GunPowerImg.SetActive(false);
                shootTimer = 0;
            }
            
        }

        playerHealth.RegenerateHealth();
        GameManager.GetInstance().UIManager.UpdateHealth();

    }

    /// <summary>
    /// Control object movement.
    /// </summary>
    /// <param name="direction">Direction of the movement by player input</param>
    /// <param name="target">Current mouse position</param>
    public override void Move(Vector2 direction, Vector2 target)
    {
        //Debug.Log("player direction: " + direction);

        playerRB.velocity = direction * speed * Time.deltaTime;
        var playerScreenPos = cam.WorldToScreenPoint(transform.position);//convert pos of player to screen pos

        target.x -= playerScreenPos.x;
        target.y -= playerScreenPos.y;

        float angle = Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg; // radians to degrees

        transform.rotation = Quaternion.Euler(0, 0, angle);//degrees to quaternion

        //TODO: Target and Rotation
    }

    public override void Attack(float interval)
    {
    }

    public override void GetDamage(float damage)
    {
        //Debug.Log("Player Damaged: " + damage);

        
        playerHealth.DeductHealth(damage);
        if (playerHealth.GetHealth() <= 0)
        {
            Die();
        }
    }

    public void AddNuke()
    {
        if (nukeNum >= maxNuke)
        {
            return;
        }
        //Add a Nuke to the player here!
        Debug.Log("add Nuke to player");
        nukeDisplay[nukeNum].GetComponent<Renderer>().enabled = true;
        nukeNum++;
    }

    /// <summary>
    /// Terminate All Enemy In Scene.
    /// </summary>
    public void UseNuke()
    {
        if (nukeNum == 0)
        {
            return;
        }
        nukeDisplay[nukeNum-1].GetComponent<Renderer>().enabled = false;
        nukeNum--;
        //Destroy Bullets, enemies
        GameManager.GetInstance().DestroyEntities();
    }

    public void AddGunPower()
    {
        //Add a GunPower to the player here!
        Debug.Log("add GunPower to player");
        gunPowerMode = 1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Nuke"))
        {
            Debug.Log("Collide with Nuke");
            pickUpSpawner.OnPickedNuke(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("GunPower"))
        {
            Debug.Log("Collide with GunPower");
            GunPowerImg.SetActive(true);
            gunPower.ResetGunPowerImg();
            shootTimer = 0;
            shootinterval = GunPowerImg.GetComponent<GunPower>().GetUseTime();
            pickUpSpawner.OnPickedGunPower(collision.gameObject);

            this.GetComponent<PlayerInput>().ActiveGunPowerMode();
        }

        


    }
}
