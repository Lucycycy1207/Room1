using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            GameManager.GetInstance().levelManager.SpawnNewPowerUp(this.gameObject.transform.position);
            GameManager.GetInstance().UIManager.UpdatePowerUpHint();
            Destroy(this.gameObject);
        }
    }
}
