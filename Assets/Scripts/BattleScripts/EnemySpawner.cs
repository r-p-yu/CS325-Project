using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; //GruntHP
    public int totalEnemies = 10;
    public float spawnInterval = 2f;
    public float spawnRadius = 20f;

    private int enemiesDefeated = 0;
    private bool stageComplete = false;

    private Transform player;

    [Header("Wave Complete Settings")]
    public Text waveCompleteText;
    public TextMeshProUGUI waveSummaryText; 
    public float textDisplayTime = 7f; // How long text stays visible

    private PlayerController playerController; // Reference to player movement script

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        if (player != null) playerController = player.GetComponent<PlayerController>();
        if (waveCompleteText != null) waveCompleteText.gameObject.SetActive(false);
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
            StartCoroutine(WaveComplete());
        }
    }

    IEnumerator WaveComplete()
    {
        if (playerController != null) playerController.canMove = false;

        int moneyEarned = enemiesDefeated * 10;
        GameManager.instance.adjustMoney(moneyEarned);

        if (waveCompleteText != null)
        {
            waveCompleteText.text = "WAVE DEFEATED";
            waveCompleteText.gameObject.SetActive(true);
        }

        if (waveSummaryText != null)
        {
            waveSummaryText.text = $"You killed {enemiesDefeated} enemies\nand earned {moneyEarned} coins!";
            waveSummaryText.gameObject.SetActive(true);
        }

        yield return new WaitForSeconds(textDisplayTime);

        SceneManager.LoadScene("FarmScene");
    }
}
