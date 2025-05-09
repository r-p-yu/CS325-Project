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
    
    // Start is called before the first frame update
    void Start()
    {
        inRange = false;
    }

    void Update()
    {
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

    public void ToggleShop()
    {

    }
    
}
