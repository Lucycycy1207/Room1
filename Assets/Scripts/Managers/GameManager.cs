using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    //Singleton Implementation

    private static GameManager instance;

    [Header("Game Entities")]
    [SerializeField] private Transform[] spawnPositions;
    [SerializeField] private GameObject[] enemyPrefab;
    [SerializeField] private Transform enemyContainer;
    [SerializeField] private GameObject nukePrefab;
    [SerializeField] private GameObject gunPowerPrefab;

    [Header("Game Variables")]
    [SerializeField] private float enemySpawnRate;
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] float nukeSpawnProb = 0.2f;
    [SerializeField] float gunPowerSpawnProb = 0.2f;


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
    [Header("Managers")]
    public ScoreManager scoreManager;
    public UIManager UIManager;


    private GameObject tempEnemy;
    private bool isEnemySpawning;

    private Weapon ShooterWeapon = new Weapon("Shooter", 40f, 10f);
    private Weapon MachineGunWeapon = new Weapon("MachineGun", 2f, 3f);
    private Weapon BoomerWeapon = new Weapon("BoomerGun", 5f, 2f);
    private Weapon SpiralWeapon = new Weapon("SpiralGun", 5f, 2f);
    [SerializeField]
    private Player player;

    public float GetPlayerHealth()
    {
        return player.GetHealth();
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
        StartCoroutine(EnemySpawner());
    }

    // Update is called once per frame
    void Update()
    {
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

        //set enemy to meleeEnemy
        if (tempEnemyType == 0)
        {
            tempEnemy.GetComponent<MeleeEnemy>().SetMeleeEnemy(MeleeAttackRange, MeleeAttackTime, MeleeDamage);
        }
        else if (tempEnemyType == 1)
        {
            tempEnemy.GetComponent<Exploder>().SetExploder(ExplodeRange, ExplodeDamage);
        }
        else if (tempEnemyType == 2)
        {
            tempEnemy.GetComponent<Shooter>().SetShooter(ShootRange, ShootRate, bulletPrefab);
            tempEnemy.GetComponent<Shooter>().weapon = ShooterWeapon;
        }
        else if (tempEnemyType == 3)
        {
            tempEnemy.GetComponent<MachineGun>().SetMachineGun(machineGunRange, machineGunRate, bulletPrefab);
            tempEnemy.GetComponent<MachineGun>().weapon = MachineGunWeapon;
        }

        else if (tempEnemyType == 4)
        {
            tempEnemy.GetComponent<Boomer>().SetBoomer(boomRadius, boomDamage, bulletPrefab);
            tempEnemy.GetComponent<Boomer>().weapon = BoomerWeapon;
        }

        else if (tempEnemyType == 5)
        {
            tempEnemy.GetComponent<SpiralShooter>().SetSpiralShooter(0.2f, bulletPrefab);
            tempEnemy.GetComponent<SpiralShooter>().weapon = SpiralWeapon;
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
