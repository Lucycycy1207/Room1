using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LaserArms : MonoBehaviour
{
   

    [SerializeField]private float damage;
    private float speed;
    public string targetTag;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void SetBullet(float _damage, string _targetTag, float _speed = 10.0f)
    {
        this.damage = _damage;
        this.speed = _speed;
        this.targetTag = _targetTag;
    }

 private void Update()
    {
     
    }

    private void Damage(IDamageable damageble)
    {

        if (damageble != null)
        {
            damageble.GetDamage(damage);
        }
    }

    // Update is called once per frame
   
        private void OnTriggerEnter2D(Collider2D collision)
        {

            //Check the target
            if (!collision.gameObject.CompareTag(targetTag))
                return;

            //Debug.Log("Bullet collided with " + collision.gameObject.name);
            //Using interface
            IDamageable damageable = collision.GetComponent<IDamageable>();
            Damage(damageable);
        }
    
}
