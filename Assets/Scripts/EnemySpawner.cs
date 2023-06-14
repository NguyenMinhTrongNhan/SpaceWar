using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnInterval = 1f;
    public float hexagonMoveDuration = 5f;

    private Vector3 spawnPoint;

    private void Start()
    {
        spawnPoint = transform.position;
        SpawnEnemies();
    }

    private void SpawnEnemies()
    {
        // Tạo 12 phi thuyền
        for (int i = 0; i < 12; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab, spawnPoint, Quaternion.identity);
            EnemyShipController shipController = enemy.GetComponent<EnemyShipController>();

            // Gọi hàm di chuyển theo hình lục giác và xếp lại thành hình 4x3
            shipController.MoveInHexagonPattern(hexagonMoveDuration, i);
        }
    }
}
