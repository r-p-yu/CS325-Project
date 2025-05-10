using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CropPlot : MonoBehaviour
{
    //growthStage = current stage of growth the crop in this plot is in. -1 if no plant currently growing
    //fullGrowthTime = time it takes for the type of crop planted to fully grow
    //seeded = boolean whether or not something is currently planted
    public int growthStage = -1;
    public int fullGrowthTime = -1;
    public bool seeded = false;

    //position vectors to instantiate the models
    Vector3 position;
    Vector3 xVector = new Vector3(5, 0, 0);
    Vector3 zVector = new Vector3(0, 0, 2.5f);

    //cropNum = what crop is planted
    //growthStageModels = holds all of the different models for the plants at various growth stages
    //                    not very modular but I couldn't figure out how to get around it
    //modelListOffset = number of indexes to offset to start the crop at the proper position of the list
    //instantiatesPlants = an array of the currently instantiated plants. Stored for easy deletion after every growth/harvest
    public int cropNum = -1;
    public GameObject[] growthStageModels;
    public int modelListOffset;
    public GameObject[] instantiatedPlants;

    //if the player is in range of the plot
    public bool inRange = false;

    public GameObject speechBubblePrefab;
    public FarmManager farmManager;

    //position used in other script's start functions so it needs to be set prior 
    void Awake()
    {
        position = transform.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        //opens the planting UI
        if (inRange && !seeded && Input.GetKeyDown(KeyCode.E))
        {
            farmManager.TogglePlantUI();
        }
    }


    public void OnTriggerEnter(Collider other)
    {
        inRange = true;
        speechBubblePrefab.SetActive(true);
    }

    public void OnTriggerExit(Collider other)
    {
        inRange = false;
        speechBubblePrefab.SetActive(false);
    }

    //sets up various parameters when a crop is planted and instantiates them
    public void Plant(int item)
    {
        //just returns if something is already planted in this plot
        if (seeded)
        {
            return;
        }

        seeded = true;
        cropNum = item;
        growthStage = 0;

        if (item == 0)
        {
            fullGrowthTime = 1;
            modelListOffset = 0;
        }
        else if (item == 1)
        {
            fullGrowthTime = 2;
            modelListOffset = 2;
        }
        else if (item == 2)
        {
            fullGrowthTime = 2;
            modelListOffset = 5;
        }

        InstantiatePlants(modelListOffset);
    }

    //instantiates 9 copies of the plant laid out in a grid
    public void InstantiatePlants(int index)
    {
        int count = 0;
        for(int i = -1; i < 2; i++)
        {
            for(int j = -1; j < 2; j++)
            {
                instantiatedPlants[count] = Instantiate(growthStageModels[index], position + (i * xVector) + (j * zVector), transform.rotation);
                count++;
            }
        }
    }

    public void Harvest()
    {
        seeded = false;
        DestroyCrops();

        //TODO: give resources
    }

    //returns whether or not something is growing at this plot
    public bool IsGrowing()
    {
        return seeded;
    }

    //called to advance crop growth 1 stage in the plot
    public void Grow()
    {
        if(growthStage == fullGrowthTime)
        {
            return;
        }

        growthStage++;
        DestroyCrops();

        InstantiatePlants(modelListOffset + growthStage);
    }

    //clears out the currently instantiated plants
    public void DestroyCrops()
    {
        foreach (GameObject plant in instantiatedPlants)
        {
            Destroy(plant);
        }
    }

    
}
