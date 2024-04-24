using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // Prefab del enemigo a spawnear
    public int numberOfEnemies = 10; // Número total de enemigos a spawnear
    public float spawnRadius = 10f; // Radio en el que se spawnearán los enemigos
    public float spawnInterval = 4f; // Intervalo entre cada spawn

    void Start()
    {
        InvokeRepeating("SpawnEnemy", 0f, spawnInterval);
    }

    void SpawnEnemy()
    {
        if (numberOfEnemies > 0)
        {
            Vector3 randomPos = transform.position + Random.insideUnitSphere * spawnRadius;
            Instantiate(enemyPrefab, randomPos, Quaternion.identity);
            numberOfEnemies--;
        }
        else
        {
            // Si ya se han spawnado todos los enemigos, cancela la invocación repetida.
            CancelInvoke("SpawnEnemy");
        }
    }
}
