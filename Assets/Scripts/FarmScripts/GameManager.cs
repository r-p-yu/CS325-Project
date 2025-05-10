using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int day = 0;

    //stores player money, inventory, and the item table
    //each index of the player inventory maps to an item on the item table, the value at the index is the quantity
    public int money;
    public static string[] itemTable = { "Carrots", "Eggplants", "Pumpkins" };
    public int[] inventory;

    //flag to set when a day passes
    //used to know when FarmManager has to grow crops 
    public bool hasDayPassed;

    //holds which plots were growing which item and what growth stage they were on
    //row = plot number, col1 = item, col2 = growth stage
    public int[,] plotContents;
    //inventory holds the quantity as elements, index of the elements map to the itemTable 
    //e.g. inventory = {1, 0, 2} means 1 carrot, 0 eggplants, 2 pumpkins
    public TextMeshProUGUI[] ownedTexts;
    public TextMeshProUGUI moneyText;

    void Awake()
    {
        if(instance == null)
        {
            //makes this instance of GameManager persist through scenes
            instance = this;
            DontDestroyOnLoad(gameObject);

            money = 0;
            inventory = new int[itemTable.Length];
            hasDayPassed = false;
            plotContents = new int[4, 2]; 
        }
        else
        {
            //destroys any duplicates of GameManager made. Shouldn't be necessary if scene hierarchies set properly
            Destroy(gameObject);
        }

    }
    public void FarmSceneSetUp()
    {
        if(moneyText == null)
        {
            moneyText = GameObject.Find("HUDPanel").GetComponentInChildren<TextMeshProUGUI>();
        }
        else
        {
            moneyText.SetText("" + money);
        }

        if (ownedTexts[0] == null)
        {
            for (int i = 0; i < ownedTexts.Length; i++)
            {
                ownedTexts[i] = GameObject.Find("OwnedText" + (i + 1)).GetComponent<TextMeshProUGUI>();
            }
        }
        else
        {
            for (int i = 0; i < ownedTexts.Length; i++)
            {
                ownedTexts[i].SetText("Owned: " + inventory[i]);
            }
        }
        
    }

    public void AddToInventory(int item)
    {
        inventory[item]++;
        ownedTexts[item].SetText("Owned: " + inventory[item]);
    }

    public void RemoveFromInventory(int item)
    {
        inventory[item]--;
        ownedTexts[item].SetText("Owned: " + inventory[item]);
    }

    public void adjustMoney(int val)
    {
        money += val;
        moneyText.SetText("" + money);
    }
}
