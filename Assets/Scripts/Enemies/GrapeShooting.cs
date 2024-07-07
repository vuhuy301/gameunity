using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapeShooting : MonoBehaviour
{
    public GameObject bulletPrefab; 
    public float fireRate = 1f; 
    public float attackRange = 10f;

    private Transform player; 
    private float nextFireTime = 0f; 

    void Start()
    {
        player = Player.Instance.transform;
    }

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(player.position, transform.position);

       
        if (distanceToPlayer <= attackRange)
        {
        
            if (Time.time >= nextFireTime)
            {
                Shoot();
                nextFireTime = Time.time + 3f / fireRate;
            }
        }
    }

    void Shoot()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        bullet.transform.right = direction;
    }
}
