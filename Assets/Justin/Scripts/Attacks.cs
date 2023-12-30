using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Attacks : MonoBehaviour
{
    [SerializeField]
    public int AttackSelector;
    [SerializeField]
    public float AttackCountdown;
    [SerializeField]public Transform BulletLauncherMid;
    [SerializeField]public Transform BulletLauncherLeft;
    [SerializeField]public Transform BulletLauncherRight;

    [SerializeField]
    public GameObject Bullet;
    [SerializeField] public GameObject FallastarLeftArm;
    [SerializeField] public GameObject FallastarRightArm;

    [SerializeField]
    public float AttackInterval = 1;
    public float LeftRightAttackInterval;
    public float timer;
    public float LeftRightTimer = 0;

    //[SerializeField]
    // public Bullet Bullet;
    // Start is called before the first frame update
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        AttackCountdown = AttackCountdown + Time.deltaTime;
        if (AttackCountdown >= 5)
        {
            AttackSelector = Random.Range(1, 5);
            Debug.Log(AttackCountdown);
            Debug.Log("Choice of attack: " + AttackSelector);
            AttackCountdown = 0;
            
        }
        if (AttackSelector == 1){BulletSpray(AttackInterval);}
        if (AttackSelector == 2) {SingleWild(AttackInterval);}
        if (AttackSelector == 3) { IntermittentSpray(AttackInterval);}
        if (AttackSelector == 4) { LaserSwipe(); }
      
    }

   
    void IntermittentSpray(float AttackInterval)
    {
        AttackInterval = 1;
        LeftRightAttackInterval = .5f;
        
        if (timer <= AttackInterval) { timer += Time.deltaTime; }
        else
        {
            timer = 0;
            BulletLauncherMid.transform.Rotate(0, 0, Random.Range(-20, 20));
            Instantiate(Bullet, BulletLauncherMid);
        }
        if (LeftRightTimer <= LeftRightAttackInterval) {LeftRightTimer = LeftRightTimer+ Time.deltaTime;}
        else
        {
            LeftRightTimer = 0;
            BulletLauncherLeft.transform.Rotate(0, 0, Random.Range(-20, 20));
            BulletLauncherRight.transform.Rotate(0, 0, Random.Range(-20, 20));
            Instantiate(Bullet, BulletLauncherLeft);Instantiate(Bullet, BulletLauncherRight);
        }
    }

    void SingleWild(float AttackInterval)
    {
        AttackInterval = 0.5f;
        
        if (timer <= AttackInterval) { timer += Time.deltaTime; }
        else 
        {
            timer = 0;
            BulletLauncherMid.transform.Rotate(0, 0, Random.Range(-20, 20));
            Instantiate(Bullet, BulletLauncherMid);
        }
        
    }

    void BulletSpray(float AttackInterval)
    {
       
        if (timer <= AttackInterval)
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer = 0;
            BulletLauncherLeft.transform.Rotate(0, 0, Random.Range(-20,20));
            BulletLauncherRight.transform.Rotate(0, 0, Random.Range(-20, 20));
            BulletLauncherMid.transform.Rotate(0, 0, Random.Range(-20, 20));
            Instantiate(Bullet, BulletLauncherMid);
            Instantiate(Bullet, BulletLauncherLeft);
            Instantiate(Bullet, BulletLauncherRight);
        }
    }
    void LaserSwipe()
    {
        AttackInterval = 2.5f;
        timer += Time.deltaTime;
        if (timer <= AttackInterval)
        {
            FallastarRightArm.transform.Rotate(0, 0, -180 * Time.deltaTime);
            FallastarLeftArm.transform.Rotate(0, 0, 180 * Time.deltaTime);
            
        }
        if (timer >= AttackInterval)
        {
            FallastarRightArm.transform.Rotate(0, 0, 180 * Time.deltaTime);
            FallastarLeftArm.transform.Rotate(0, 0, -180 * Time.deltaTime);
        }
        if (timer >= 5) { timer = 0; }
    }
}
