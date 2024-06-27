using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject bulletPrefab; // Prefab đạn
    public Transform firePoint; // Điểm xuất phát của đạn
    public float bulletSpeed = 10f; // Tốc độ của đạn
    public float fireRate = 2f; // Tốc độ bắn đạn

    private Transform player; // Vị trí của player
    private float nextFireTime = 0f; // Thời gian tiếp theo quái có thể bắn
    void Start()
    {
        void Update()
        {
            // Kiểm tra nếu đến thời gian bắn tiếp theo
            if (Time.time >= nextFireTime)
            {
                Shoot();
                nextFireTime = Time.time + 1f / fireRate;
            }
        }
    }

    // Update is called once per frame
    void Shoot()
    {
        
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);


        Vector2 direction = (Player.Instance.transform.position - firePoint.position).normalized;

       
        bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
    }
}
