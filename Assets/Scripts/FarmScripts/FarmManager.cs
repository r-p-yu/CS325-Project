using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class FarmManager : MonoBehaviour
{
    public CropPlot[] plots;
    public GameManager gameManager;
    public GameObject plantUI;
    public TempPlayerController playerController;

    public bool isPlantUIActive;

    // Start is called before the first frame update
    void Start()
    {
        isPlantUIActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //plants the proper seed into the plot the player is in range of 
    public void plantSeed(int item)
    {
        if (gameManager.inventory[item] > 0)
        {
            foreach(CropPlot plot in plots)
            {
                if (plot.inRange)
                {
                    plot.Plant(item);
                    break;
                }
            }
            gameManager.RemoveFromInventory(item);
            TogglePlantUI();
        }
    }

    public void TogglePlantUI()
    {
        if (!isPlantUIActive)
        {
            isPlantUIActive = true;
            plantUI.SetActive(true);
            //Lock player movement and camera
            playerController.canMove = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        else
        {
            isPlantUIActive = false;
            plantUI.SetActive(false);
            playerController.canMove = true;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
