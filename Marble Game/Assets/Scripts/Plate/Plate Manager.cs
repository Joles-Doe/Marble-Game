using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateManager : MonoBehaviour
{
    public GameManager manager;

    List<MarbleSpawn> spawnList = new List<MarbleSpawn>();

    [HideInInspector] public bool activePlate = false;
    [HideInInspector] public bool levelComplete = false;

    int spawnerAmount = 0;
    [HideInInspector] public int inactiveSpawners = 0;

    // Start is called before the first frame update
    void Start()
    {
        //grabs each spawner and stores them in a list
        foreach (Transform child in transform.GetChild(0))
        {
            if (child.parent == transform.GetChild(0))
            {
                if (child.GetComponent<MarbleSpawn>() != null)
                {
                    spawnList.Add(child.GetComponent<MarbleSpawn>());
                    spawnerAmount++;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!levelComplete)
        {
            if (spawnerAmount == inactiveSpawners)
            {
                levelComplete = true;
            }
        }
    }

    //unlocks the managed spawners
    public void UnlockSpawners()
    {
        foreach (MarbleSpawn spawn in spawnList)
        {
            spawn.isActive = true;
        }
    }

    //locks the managed spawners
    public void LockSpawners()
    {
        foreach (MarbleSpawn spawn in spawnList)
        {
            spawn.isActive = false;
        }
    }

    //communicates to the manager that a vital marble has fallen
    public void LoseLife()
    {
        manager.LoseLife();
    }

    //communicates to the manager that the plate needs to be rotated
    public void RotatePlate()
    {
        manager.RotateCurrentPlate();
    }

    //resets the spawners
    public void Reset()
    {
        foreach (MarbleSpawn child in spawnList)
        {
            child.isActive = false;
            child.haventSpawned = true;
        }
        levelComplete = false;
        inactiveSpawners = 0;
    }
}
