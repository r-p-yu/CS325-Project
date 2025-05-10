using System.Collections;
using System.Collections.Generic;
using TMPro;
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
        gameManager = FindObjectOfType<GameManager>();
        plantUI.SetActive(true);
        gameManager.FarmSceneSetUp();
        plantUI.SetActive(false);
        isPlantUIActive = false;

        print(gameManager.plotContents[0, 0] + " " + gameManager.plotContents[0, 1]);
        print(gameManager.plotContents[1, 0] + " " + gameManager.plotContents[1, 1]);
        print(gameManager.plotContents[2, 0] + " " + gameManager.plotContents[2, 1]);
        print(gameManager.plotContents[3, 0] + " " + gameManager.plotContents[3, 1]);

        //if re-entering scene after a night, puts crops back to how they were before +1 growth stage
        if (gameManager.hasDayPassed)
        {
            for (int i = 0; i < plots.Length; i++)
            {
                int plant = gameManager.plotContents[i, 0];
                int growthStage = gameManager.plotContents[i, 1];
                if (plant >= 0)
                {
                    plots[i].Plant(plant);
                    for(int j = 0; j <= growthStage; j++)
                    {
                        plots[i].Grow();
                    }
                }
            }
        }

        //first load, set plot contents to -1
        else
        {
            for(int i = 0; i < plots.Length; i++)
            {
                gameManager.plotContents[i, 0] = -1;
                gameManager.plotContents[i, 1] = -1;
            }
        }
    }

    //saves the state of the plots to use when exiting scene
    public void SaveState()
    {
        for(int i = 0; i < plots.Length; i++)
        {
            gameManager.plotContents[i, 0] = plots[i].cropNum;
            gameManager.plotContents[i, 1] = plots[i].growthStage;
        }
        
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

    //opens the interface for planting in a plot
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
