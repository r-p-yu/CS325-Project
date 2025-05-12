using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleTrigger : MonoBehaviour
{
    public bool inRange;
    public FarmManager farmManager;
    public GameObject speechBubblePrefab;

    void Start()
    {
        inRange = false;
    }

    void Update()
    {
        //Progresses the game to the next battle stage
        if (inRange && Input.GetKeyDown(KeyCode.E))
        {
            farmManager.SaveState();
            SceneManager.LoadScene("BattleScene");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        inRange = true;
        speechBubblePrefab.SetActive(true);
    }

    void OnTriggerExit(Collider other)
    {
        inRange = false;
        speechBubblePrefab.SetActive(false);
    }
}
