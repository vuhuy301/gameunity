using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealth player = collision.GetComponent<PlayerHealth>(); 
            if (player != null)
            {
                player.IncreaseHealth(1); 
            }

         
            Destroy(gameObject);
        }
    }
}
