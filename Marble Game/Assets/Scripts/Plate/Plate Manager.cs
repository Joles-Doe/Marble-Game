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

    public void UnlockSpawners()
    {
        foreach (MarbleSpawn spawn in spawnList)
        {
            spawn.isActive = true;
        }
    }

    public void LoseLife()
    {
        manager.LoseLife();
    }

    public void RotatePlate()
    {
        manager.RotateCurrentPlate();
    }
}
