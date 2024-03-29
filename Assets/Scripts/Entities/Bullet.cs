using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float speed;

    private string targetTag;

    public void SetBullet(float _damage, string _targetTag, float _speed = 10.0f)
    {
        this.damage = _damage;
        this.speed = _speed;
        this.targetTag = _targetTag;
    }

    private void Update()
    {
        Move();
    }

    public float GetSpeed()
    {
        return this.speed;
    }

    private void Move()
    {
        //Debug.Log($"Bullet moving to do a damage of {damage}");
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    /// <summary>
    /// Damage damageble object and destroy bullet
    /// </summary>
    /// <param name="damageble">The object damageble component.</param>
    private void Damage(IDamageable damageble)
    {
        
        if (damageble != null)
        {
            damageble.GetDamage(damage);

            if ((damageble.GetType().Name != "BasicBlock") && (damageble.GetType().Name != "Player"))
            {
                GameManager.GetInstance().scoreManager.IncrementScore();
                //Debug.Log($"Damaged Something: " + damageble.GetType().Name);

            }
                
            
            
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Bullet collided with " + collision.gameObject.name);
        //Check the target
        if (!(collision.gameObject.CompareTag(targetTag) || collision.gameObject.CompareTag("Block")))
            return;

        
        //Using interface
        IDamageable damageable = collision.GetComponent<IDamageable>();
        Damage(damageable);
    }
}
