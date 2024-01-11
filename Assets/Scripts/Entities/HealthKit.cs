using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthKit : MonoBehaviour
{
    [SerializeField] private float HealthAdd = 50;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.CompareTag("Player"))
        {
            //add health to player
            collision.gameObject.GetComponent<Player>().health.AddHealth(HealthAdd);
            Destroy(this.gameObject);
        }
    }
}
