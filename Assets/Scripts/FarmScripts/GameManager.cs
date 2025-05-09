using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int money;
    public TextMeshProUGUI moneyText;

    public int day;

    //inventory holds the quantity as elements, index of the elements map to the itemTable 
    //e.g. inventory = {1, 0, 2} means 1 carrot, 0 eggplants, 2 pumpkins
    static string[] itemTable = { "Carrots", "Eggplants", "Pumpkins" };
    public int[] inventory;
    public TextMeshProUGUI[] ownedTexts;

    void Start()
    {
        day = 0;
        inventory = new int[itemTable.Length];
        
        //just here for testing
        adjustMoney(250);
        AddToInventory(0);
        AddToInventory(2);
        AddToInventory(2);
       
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
