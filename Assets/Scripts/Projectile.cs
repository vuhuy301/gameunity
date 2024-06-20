using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 22f;
    [SerializeField] private GameObject particleOnHitPrefabVFX;
    [SerializeField] private int damage = 1;

    private WeaponInfo weaponInfo;
    private Vector3 startPosition;

    private void Start() {
        startPosition = transform.position;
    }

    private void Update()
    {
        MoveProjectile();
        DetectFireDistance();
    }

    public void UpdateWeaponInfo(WeaponInfo weaponInfo){
        this.weaponInfo = weaponInfo;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(damage);
            enemyHealth.DetecDeath();
            if (particleOnHitPrefabVFX != null)
            {
                Instantiate(particleOnHitPrefabVFX, transform.position, Quaternion.identity);  // Tạo hiệu ứng khi va chạm
            }
            Destroy(gameObject);  // Mũi tên biến mất khi va chạm
        }
    }


    private void DetectFireDistance()
    {
        
    }

    private void MoveProjectile()
    {
        transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);
    }
}
