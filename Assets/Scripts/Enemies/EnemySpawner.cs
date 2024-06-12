using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs; 
    public float timeBetweenSpawns = 1f; 
    public int totalEnemiesToSpawn = 10;
    public float spawnRadius = 5f; 

    private int enemiesSpawned = 0;
    private float timeSinceLastSpawn = 0f;

    private void Update()
    {
        
        if (enemiesSpawned < totalEnemiesToSpawn)
        {
           
            timeSinceLastSpawn += Time.deltaTime;

            
            if (timeSinceLastSpawn >= timeBetweenSpawns)
            {
                
                SpawnEnemy();

                
                timeSinceLastSpawn = 0f;
                enemiesSpawned++;
            }
        }
    }

    private void SpawnEnemy()
    {
        
        GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

       
        Vector2 randomPosition = Random.insideUnitCircle * spawnRadius;

        Instantiate(enemyPrefab, (Vector2)transform.position + randomPosition, Quaternion.identity);
    }
}
