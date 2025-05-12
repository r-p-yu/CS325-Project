using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public PlayerData playerData;
    private string playerDataPath;
    public static GameManager instance;

    public int day = 0;

    //stores player money, inventory, and the item table
    //each index of the player inventory maps to an item on the item table, the value at the index is the quantity
    // Stores player money, inventory, and the item table
    public int money;
    public static string[] itemTable = { "Carrot Seeds", "Eggplant Seeds", "Pumpkin Seeds", "Carrots", "Eggplants", "Pumpkins" };
    public int[] inventory;
    public TextMeshProUGUI moneyText;

    public bool hasDayPassed;

    //holds which plots were growing which item and what growth stage they were on
    //row = plot number, col1 = item, col2 = growth stage
    public int[][] plotContents;

    public int playerHealth = 3;
    private float damageCooldown = 0f;
    public float invincibilityDuration = 1.5f;

    public GameObject gameOverUI;
    public Text gameOverText;
    public float gameOverDelay = 5f;

    public bool isInGame = false;

    void Awake()
    {
        //game manager first instantiated
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            playerDataPath = Path.Combine(Application.persistentDataPath, "playerData.json");
            ResetPlayerData();

            //if JSON already exists
            if (File.Exists(playerDataPath))
            {
                LoadFromJSON();
            }
            else
            {
                LoadFromPlayerData();             
                WriteToJSON();
            }
            
        }
        //no duplicates of game manager wanted
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (damageCooldown > 0f)
        {
            damageCooldown -= Time.deltaTime;
        }
    }

    //resets the JSON to default values
    public void ResetJSON()
    {
        ResetPlayerData();

        string json = JsonUtility.ToJson(playerData, true);
        File.WriteAllText(playerDataPath, json);
    }

    //resets player data
    public void ResetPlayerData()
    {
        playerData = new PlayerData
        {
            money = 0,
            inventory = new int[6],
            plot0 = new int[] { -1, -1 },
            plot1 = new int[] { -1, -1 },
            plot2 = new int[] { -1, -1 },
            plot3 = new int[] { -1, -1 },
            hasDayPassed = false
        };
    }

    //reads player data from the JSON file, updates playerdata, and sets GameManagers local fields from player data
    public void LoadFromJSON()
    {
        if (File.Exists(playerDataPath))
        {
            string json = File.ReadAllText(playerDataPath);
            playerData = JsonUtility.FromJson<PlayerData>(json);
            LoadFromPlayerData();
        }
        else
        {
            WriteToJSON();
        }
    }
    //updates player data then writes to the JSON file
    public void WriteToJSON()
    {
        updatePlayerData();
        //bool flag is for pretty print, turn off after debugging
        string json = JsonUtility.ToJson(playerData, true);
        File.WriteAllText(playerDataPath, json);

    }

    //updates playerData fields
    public void updatePlayerData()
    {
        playerData.inventory = inventory;
        playerData.hasDayPassed = hasDayPassed;
        playerData.plot0 = plotContents[0];
        playerData.plot1 = plotContents[1];
        playerData.plot2 = plotContents[2];
        playerData.plot3 = plotContents[3];
    }

    //updates game manager fields from playerdata
    public void LoadFromPlayerData()
    {
        inventory = playerData.inventory;
        hasDayPassed = playerData.hasDayPassed;
        plotContents = new int[4][];
        plotContents[0] = playerData.plot0;
        plotContents[1] = playerData.plot1;
        plotContents[2] = playerData.plot2;
        plotContents[3] = playerData.plot3; 
    }

    //sets UI elements in FarmScene 
    public void FarmSceneSetUp()
    {
        LoadFromJSON();
        GameObject moneyObj = GameObject.Find("MoneyText");
        moneyText = moneyObj.GetComponent<TextMeshProUGUI>();
        moneyText.SetText(money.ToString());
    }

    public void StartGame()
    {
        isInGame = true;
        money = 150;
        ResetHealth(3);
    }

    //input: item's index in the item table. Not very scalable but works for now 
    public void AddToInventory(int item, int quantity)
    {
        inventory[item] += quantity;
    }

    //input: item's index in the item table. Not very scalable but works for now
    public void RemoveFromInventory(int item)
    {
        inventory[item]--;
    }

    //input: input: how much to change the value of money 
    public void adjustMoney(int val)
    {
        money += val;
    }

    public void TakeDamage(int amount)
    {
        Debug.Log($"Player took {amount} damage. Health now: {playerHealth}");
        if (damageCooldown > 0f)
        {
            return;
        }
        playerHealth -= amount;
        playerHealth = Mathf.Max(playerHealth, 0);

        damageCooldown = invincibilityDuration;

        if (playerHealth <= 0)
        {
            StartCoroutine(HandleGameOver());
        }
    }

    IEnumerator HandleGameOver()
    {
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);
        }
        if (gameOverText != null)
        {
            gameOverText.text = "GAME OVER";
        }
        yield return new WaitForSeconds(gameOverDelay);

        SceneManager.LoadScene("MainMenuScene"); 
    }

    public void Heal(int amount)
    {
        playerHealth += amount;
    }

    public void ResetHealth(int value = 3)
    {
        playerHealth = value;
    }

}
