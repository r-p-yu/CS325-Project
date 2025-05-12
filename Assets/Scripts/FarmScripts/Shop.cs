using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public int[] merchandise;
    public int[] prices;
    public GameManager gameManager;
    public GameObject notEnoughMoneyText;
    public TextMeshProUGUI[] shopOwnedTexts;
    public AudioManager audioManager;

    void Start()
    {
        gameManager  = FindObjectOfType<GameManager>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    public void UpdateTexts()
    {
        for (int i = 0; i < shopOwnedTexts.Length; i++)
        {
            shopOwnedTexts[i].SetText("Owned: " + gameManager.inventory[i]);
        }
    }

    public void BuyItem(int index)
    {
        if(gameManager.money >= prices[index])
        {
            gameManager.adjustMoney(-1 * prices[index]);
            gameManager.AddToInventory(merchandise[index], 1);
            gameManager.moneyText.SetText(gameManager.money.ToString());
            UpdateTexts();
            audioManager.PlayPurchaseSound();
        }

        else
        {
            StartCoroutine(NotEnoughMoney());
        }
    }

    IEnumerator NotEnoughMoney()
    {
        notEnoughMoneyText.SetActive(true);
        yield return new WaitForSeconds(1);
        notEnoughMoneyText.SetActive(false);
    }
}
