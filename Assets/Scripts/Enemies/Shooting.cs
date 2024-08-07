﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject bulletPrefab; 
    public float fireRate = 5f;
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
                nextFireTime = Time.time + 1f / fireRate;
            }
        }
    }

   
    void Shoot()
    {
        Vector2 direction = (Player.Instance.transform.position - transform.position).normalized;
        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        bullet.transform.right = direction;
    }
}
