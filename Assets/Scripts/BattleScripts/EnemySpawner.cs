using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; //GruntPBR
    public int totalEnemies = 10;
    public float spawnInterval = 4f;
    public float spawnRadius = 20f;

    private int enemiesDefeated = 0;
    private bool stageComplete = false;

    private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(SpawnEnemies());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnEnemies()
    {
        for (int i = 0; i < totalEnemies; i++)
        {
            if (stageComplete) yield break;

            // Spawn in a circle around player (better than full sphere)
            Vector2 randomCircle = Random.insideUnitCircle.normalized * spawnRadius;
            Vector3 spawnPos = player.position + new Vector3(randomCircle.x, 0, randomCircle.y);

            GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
            SetupEnemy(enemy);

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SetupEnemy(GameObject enemy)
    {
        // Add required components if missing
        Enemy enemyScript = enemy.GetComponent<Enemy>() ?? enemy.AddComponent<Enemy>();
        enemyScript.player = player;

        // Add hit detection
        enemy.AddComponent<HitDetector>().spawner = this;

        // Ensure collider exists
        if (!enemy.GetComponent<Collider>())
        {
            enemy.AddComponent<BoxCollider>();
        }
    }

    public void EnemyDefeated()
    {
        enemiesDefeated++;
        Debug.Log($"Enemy defeated! ({enemiesDefeated}/{totalEnemies})");

        if (enemiesDefeated >= totalEnemies)
        {
            stageComplete = true;
            Debug.Log("STAGE COMPLETE! All enemies defeated!");
            // Add your victory logic here
        }
    }
}
