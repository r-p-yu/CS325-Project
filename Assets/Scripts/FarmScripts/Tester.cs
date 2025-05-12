using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tester : MonoBehaviour
{
    // Start is called before the first frame update
    public bool inRange;
    public FarmManager farmManager;
    public GameManager gameManager;
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        inRange = false;
    }

    void Update()
    {
        //simulates ending a day
        if (inRange && Input.GetKeyDown(KeyCode.E))
        {
            gameManager.hasDayPassed = true;
            farmManager.SaveState();        
            SceneManager.LoadScene("TestScene");
        }
        if (inRange && Input.GetKeyDown(KeyCode.R))
        {
            gameManager.adjustMoney(1000);
        }
        if(inRange && Input.GetKeyDown(KeyCode.F))
        {
            gameManager.ResetJSON();
        }
        if(inRange && Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log(farmManager.plots[0].seedNum + " " + farmManager.plots[0].growthStage);
            Debug.Log(farmManager.plots[1].seedNum + " " + farmManager.plots[1].growthStage);
            Debug.Log(farmManager.plots[2].seedNum + " " + farmManager.plots[2].growthStage);
            Debug.Log(farmManager.plots[3].seedNum + " " + farmManager.plots[3].growthStage);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        inRange = true;
    }

    void OnTriggerExit(Collider other)
    {
        inRange = false;
    }
}
