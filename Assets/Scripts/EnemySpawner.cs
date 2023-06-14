using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject EnemyPoint;
    float maxSpawnRateInSeconds = 5f;
    private void Start()
    {
        Invoke("SpawnEnemy", maxSpawnRateInSeconds );
        InvokeRepeating("IncreaseSpawnRate", 0f, 30f);
    }
    private void Update()
    {
        
    }
    void SpawnEnemy()
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        //
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        //
        GameObject anEnemy = (GameObject)Instantiate(EnemyPoint);
        anEnemy.transform.position = new Vector2(Random.Range(min.x, max.x), max.y);
        //
        ScheduleNextEnemySpawn();
    }
    void ScheduleNextEnemySpawn()
    {
        float spawnInSeconds;
        if (maxSpawnRateInSeconds > 1f)
        {
            spawnInSeconds = Random.Range(1f, maxSpawnRateInSeconds);
        }
        else
            spawnInSeconds = 1f;
        Invoke("SpawnEnemy", spawnInSeconds);
    }
    //
    void IncreaseSpawnRate()
    {
        if (maxSpawnRateInSeconds > 1f)
            maxSpawnRateInSeconds--;
        if (maxSpawnRateInSeconds == 1f)
            CancelInvoke("IncreaseSpawnRate");
    }
}
