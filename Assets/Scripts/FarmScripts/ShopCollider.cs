using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopCollider : MonoBehaviour
{
    public Shop shop;

    public GameObject speechBubblePrefab;
    public string text = "Press E to access the Shop";
    public bool inRange;
    public bool isActive;

    public TempPlayerController playerController;
    public GameObject shopUI;
    
    // Start is called before the first frame update
    void Start()
    {
        inRange = false;      
    }

    void Update()
    {
        //opens the shop UI
        if (inRange && Input.GetKeyDown(KeyCode.E))
        {
            ToggleShop();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        inRange = true;
        speechBubblePrefab.SetActive(true);
    }

    void OnTriggerExit(Collider other)
    {
        inRange=false;
        speechBubblePrefab.SetActive(false);
    }

    //opens the shop UI
    public void ToggleShop()
    {
        if (!isActive) 
        {
            isActive = true;
            shopUI.SetActive(true);
            //Lock player movement and camera
            playerController.canMove = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        else
        {
            isActive = false;
            shopUI.SetActive(false);
            playerController.canMove = true;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        
    }
    
}
