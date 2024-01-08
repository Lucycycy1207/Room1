using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    //Singleton Implementation

    private static GameManager instance;

    [Header("Game Levels")]
    [SerializeField] private int[] enemyKilledPerLevel;
    [SerializeField] private int[] enemyNumPerLevel;

    [Header("Game Entities")]
    [SerializeField] private Transform[] spawnPositions;
    [SerializeField] private GameObject[] enemyPrefab;
    [SerializeField] private Transform enemyContainer;
    [SerializeField] private GameObject nukePrefab;
    [SerializeField] private GameObject gunPowerPrefab;
    [SerializeField] private GameObject healthKitPrefab;

    [Header("Game Variables")]
    [SerializeField] private float enemySpawnRate;
    [SerializeField] private Bullet bulletPrefab;
    
    [SerializeField] float nukeSpawnProb = 0.1f;
    [SerializeField] float gunPowerSpawnProb = 0.1f;
    [SerializeField] float healthKitSpawnProb = 0.05f;


    [Header("Melee Variables")]
    [SerializeField] private float MeleeDamage = 2f;
    [SerializeField] private float MeleeAttackRange = 2f;
    [SerializeField] private float MeleeAttackTime = 0.2f;

    [Header("Exloder Variables")]
    [SerializeField] private float ExplodeRange = 1f;
    [SerializeField] private float ExplodeDamage = 40f;

    [Header("Shooter Variables")]
    [SerializeField] private float ShootRange = 10f;
    [SerializeField] private float ShootRate = 2f;

    [Header("MachineGun Variables")]
    [SerializeField] private float machineGunRange = 6f;
    [SerializeField] private float machineGunRate = 0.5f;

    [Header("Boomer Varaibles")]
    [SerializeField] private float boomRadius = 6;
    [SerializeField] private float boomDamage = 5f;

    [Header("Divider Variables")]
    [SerializeField] private float dividerAttackRange = 20f;
    [SerializeField] private float dividerAttackTime = 1f;

    [Header("Managers")]
    public ScoreManager scoreManager;
    public UIManager UIManager;
    //public LevelManager levelManager;   


    private GameObject tempEnemy;
    private bool isEnemySpawning;

    private Weapon ShooterWeapon = new Weapon("Shooter", 40f, 10f);
    private Weapon MachineGunWeapon = new Weapon("MachineGun", 2f, 3f);
    private Weapon BoomerWeapon = new Weapon("BoomerGun", 5f, 2f);
    private Weapon SpiralWeapon = new Weapon("SpiralGun", 1f, 2f);
    private Weapon DividerGun = new Weapon("DividerGun", 1f, 5f);

    [SerializeField]
    private Player player;
    private int totalLevels;

    public float GetPlayerHealth()
    {
        return player.health.GetHealth();
    }


    public GameObject GetNukePrefab()
    {
        return nukePrefab;
    }

    public float GetNukeSpawnProb()
    {
        return nukeSpawnProb;
    }

    public GameObject GetGunPowerPrefab()
    {
        return gunPowerPrefab;
    }
    
    public float GetGunPowerSpawnProb()
    {
        return gunPowerSpawnProb;
    }

    public GameObject GetHealthKitPrefab()
    {
        return healthKitPrefab;
    }
    public float GetHealthKitSpawnProb()
    {
        return healthKitSpawnProb;
    }



    public static GameManager GetInstance()
    {
        return instance;
    }
    
    private void SetSingleton()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }

        instance = this;
    }

    private void Awake()
    {
        SetSingleton();
    }

    // Start is called before the first frame update
    void Start()
    {
        isEnemySpawning = true;
        totalLevels = enemyPrefab.Length;
        StartCoroutine(EnemySpawner());
    }

    // Update is called once per frame
    void Update()
    {
        //Check if complete current level;
        //int currLevel = levelManager.GetCurrLevel();
        
        //if (currLevel == totalLevels)
        //{
        //    return;
        //}

        //if (enemyKilledPerLevel[currLevel-1] <= scoreManager.GetScore())
        //{
        //    levelManager.LoadLevel(currLevel + 1);
        //}
        //Make enemy spawn
        GetEnemySpawn();
    }

    private void CreateEnemy()
    {
        int tempEnemyType = Random.Range(0, enemyPrefab.Length);
        tempEnemy = Instantiate(enemyPrefab[tempEnemyType]);
        tempEnemy.transform.SetParent(enemyContainer);
        tempEnemy.transform.position = spawnPositions[Random.Range(0, spawnPositions.Length)].position;
        //Debug.Log("tempEnemyType: " + tempEnemyType);

        //

        switch (tempEnemyType) 
        {
            //set enemy to meleeEnemy
            case 0:
            {
                tempEnemy.GetComponent<MeleeEnemy>().SetMeleeEnemy(MeleeAttackRange, MeleeAttackTime, MeleeDamage);
                break;
            }
            case 1:
            {
                tempEnemy.GetComponent<Exploder>().SetExploder(ExplodeRange, ExplodeDamage);
                break;
            }
            case 2:
            {
                tempEnemy.GetComponent<Shooter>().SetShooter(ShootRange, ShootRate, bulletPrefab);
                tempEnemy.GetComponent<Shooter>().weapon = ShooterWeapon;
                break;
            }
            case 3:
            {
                tempEnemy.GetComponent<MachineGun>().SetMachineGun(machineGunRange, machineGunRate, bulletPrefab);
                tempEnemy.GetComponent<MachineGun>().weapon = MachineGunWeapon;
                break;
            }

            case 4:
            {
                tempEnemy.GetComponent<Boomer>().SetBoomer(boomRadius, boomDamage, bulletPrefab);
                tempEnemy.GetComponent<Boomer>().weapon = BoomerWeapon;
                break;
            }
            case 5:
            {
                tempEnemy.GetComponent<Divider>().SetDivider(dividerAttackRange, dividerAttackTime, bulletPrefab);
                tempEnemy.GetComponent<Divider>().weapon = DividerGun;
                break;
            }
            //case 6:
            //    {
            //        tempEnemy.GetComponent<SpiralShooter>().SetSpiralShooter(0.1f, bulletPrefab);
            //        tempEnemy.GetComponent<SpiralShooter>().weapon = SpiralWeapon;
            //        break;
            //    }



        }

    }


    /// <summary>
    /// Press X to spawn new enemy.
    /// </summary>
    private void GetEnemySpawn()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            CreateEnemy();
        }
    }

    IEnumerator EnemySpawner()
    {
        while (isEnemySpawning)
        {
            yield return new WaitForSeconds(1.0f / enemySpawnRate);
            if (player != null)
                CreateEnemy();
        }
    }

    /// <summary>
    /// Destroy all entities in the scene. (Enemies, bullets)
    /// </summary>
    public void DestroyEntities()
    {

        var EnemyList = FindObjectsOfType<Enemy>();
        Debug.Log(EnemyList + " : " + EnemyList.Length);
 

        for (int i = 0; i < EnemyList.Length; i++)
        {
            EnemyList[i].Die();
        }


        var bulletList = FindObjectsOfType<Bullet>();

        for (int i = 0; i < bulletList.Length; i++)
        {
            Destroy(bulletList[i].gameObject);
        }


    }


}
