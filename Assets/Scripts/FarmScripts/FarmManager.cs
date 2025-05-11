using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class FarmManager : MonoBehaviour
{
    private GameManager gameManager;
    public TempPlayerController playerController;

    public GameObject plantUI;
    public bool isPlantUIActive;

    public CropPlot[] plots;
    public TextMeshProUGUI[] cropOwnedTexts;

    public GameObject inventoryUI;
    public TextMeshProUGUI[] inventoryOwnedTexts;
    public bool isInventoryUIActive;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        plantUI.SetActive(true);
        gameManager.FarmSceneSetUp();
        plantUI.SetActive(false);
        isPlantUIActive = false;
        isInventoryUIActive = false;

        //if re-entering scene after a night, puts crops back to how they were before +1 growth stage
        if (gameManager.hasDayPassed)
        {
            for (int i = 0; i < plots.Length; i++)
            {
                int plant = gameManager.plotContents[i][0];
                int growthStage = gameManager.plotContents[i][1];
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
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventoryUI();
        }
    }

    //saves the state of the plots to use when exiting scene
    public void SaveState()
    {
        for(int i = 0; i < plots.Length; i++)
        {
            gameManager.plotContents[i][0] = plots[i].seedNum;
            gameManager.plotContents[i][1] = plots[i].growthStage;
        }
        gameManager.WriteToJSON();
    }

    //plants the proper seed into the plot the player is in range of 
    public void PlantSeed(int item)
    {
        if (gameManager.inventory[item] > 0)
        {          
            gameManager.RemoveFromInventory(item);
            foreach(CropPlot plot in plots)
            {
                if (plot.inRange)
                {
                    plot.Plant(item);
                }
            }
            TogglePlantUI();
        }

    }

    //opens the interface for planting in a plot
    public void TogglePlantUI()
    {
        if (!isPlantUIActive)
        {
            for (int i = 0; i < cropOwnedTexts.Length; i++)
            {
                Debug.Log("test");
                cropOwnedTexts[i].SetText("Owned: " + gameManager.inventory[i]);
            }
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

    public void ToggleInventoryUI()
    {
        if (!isInventoryUIActive)
        {
            isInventoryUIActive = true;
            for(int i = 0; i < inventoryOwnedTexts.Length; i++)
            {
                inventoryOwnedTexts[i].SetText("Owned: " + gameManager.inventory[i]);
            }
            inventoryUI.SetActive(true);
            playerController.canMove = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        else
        {
            isInventoryUIActive = false;
            inventoryUI.SetActive(false);
            playerController.canMove = true;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
