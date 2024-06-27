using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 22f;
    [SerializeField] private GameObject particleOnHitPrefabVFX;
    [SerializeField] private int damage = 1;
    [SerializeField] private bool isEnemy = false;
    [SerializeField] private float range = 10f;

    
    private Vector3 startPosition;

    private void Start() {
        startPosition = transform.position;
    }

    private void Update()
    {
        MoveProjectile();
        DetectFireDistance();
    }

    public void UpdateProjectTitleRange(float range)
    {
        this.range = range;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
        PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth>();
        if (enemyHealth != null && !isEnemy)
        {
            enemyHealth.TakeDamage(damage);
            enemyHealth.DetecDeath();
            if (particleOnHitPrefabVFX != null)
            {
                Instantiate(particleOnHitPrefabVFX, transform.position, Quaternion.identity);  // Tạo hiệu ứng khi va chạm
            }
            Destroy(gameObject);  // Mũi tên biến mất khi va chạm
        }
        if(playerHealth != null && isEnemy) {
            playerHealth.TakeDamage(1, transform);
            Destroy(gameObject);
        }
    }


    private void DetectFireDistance()
    {
        if (Vector3.Distance(transform.position, startPosition) > range)
        {
            Destroy(gameObject);
        }
    }

    private void MoveProjectile()
    {
        transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);
    }
}
