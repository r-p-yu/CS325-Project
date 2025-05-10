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
            farmManager.SaveState();
            gameManager.hasDayPassed = true;
            SceneManager.LoadScene("TestScene");
        }
        if (inRange && Input.GetKeyDown(KeyCode.R))
        {
            gameManager.adjustMoney(1000);
        }
        if (inRange && Input.GetKeyDown(KeyCode.F))
        {
            print(gameManager.plotContents[0, 0] + " " + gameManager.plotContents[0, 1]);
            print(gameManager.plotContents[1, 0] + " " + gameManager.plotContents[1, 1]);
            print(gameManager.plotContents[2, 0] + " " + gameManager.plotContents[2, 1]);
            print(gameManager.plotContents[3, 0] + " " + gameManager.plotContents[3, 1]);
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
