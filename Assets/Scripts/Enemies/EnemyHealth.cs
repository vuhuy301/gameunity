using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int startingHealth = 3;
    [SerializeField] private GameObject deathVFXPrefab;
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private GameObject healthPrefab;

    private int currentHealth;

    private Knockback knockback;

    private Flash flash;

    private void Awake()
    {
        flash = GetComponent<Flash>();  
        knockback = GetComponent<Knockback>();
    }
    private void Start()
    {
        currentHealth = startingHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        knockback.GetKnockBack(Player.Instance.transform, 2f);
        StartCoroutine(flash.FlashRoutine());
        StartCoroutine(CheckDetectDeathRoutine());
    }

    private IEnumerator CheckDetectDeathRoutine()
    {
        yield return new WaitForSeconds(flash.GetRestoreMatTime());
        DetecDeath();
    }


    public void DetecDeath()
    {
        if(currentHealth <= 0) {
            Instantiate(deathVFXPrefab, transform.position, Quaternion.identity);
            int dropChance = Random.Range(0, 3);

            if (dropChance == 0)
            {
                Instantiate(healthPrefab, transform.position, Quaternion.identity);
            }
            else if (dropChance == 1)
            {
                Instantiate(arrowPrefab, transform.position, Quaternion.identity);
            }
            PlayScore.Instance.AddScore(1);
            Destroy(gameObject);
            
        }
    }
}
