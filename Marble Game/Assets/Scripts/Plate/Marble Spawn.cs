using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarbleSpawn : MonoBehaviour
{
    public PlateManager manager;

    public bool spawnVital;
    
    public GameObject prefab;
    public GameObject explosionFX;
    [HideInInspector] public GameObject marble;
    [HideInInspector] public Marble marbleScript;
    [HideInInspector] public GameObject explosion;

    [HideInInspector] public bool isActive = false;
    [HideInInspector] public bool haventSpawned = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            if (haventSpawned)
            {
                SpawnMarble();
                haventSpawned = false;
            }
            else if (marbleScript != null)
            {
                //if the marble needs to be destroyed, check if it went through the hole and react accordingly
                if (marbleScript.destroy)
                {
                    if (marbleScript.canLeave)
                    {
                        //makes the spawner the marble came from inactive
                        isActive = false;
                        manager.inactiveSpawners++;
                        DestroyMarble();
                        marble = null;
                        marbleScript = null;
                    }
                    else
                    {
                        if (spawnVital)
                        {
                            //lose a life if the marble was a vital one
                            manager.LoseLife();
                        }
                        DestroyMarble();
                        manager.RotatePlate();
                        marble = null;
                        marbleScript = null;
                        SpawnMarble();
                    }
                }
            }
        }
    }

    //function that uses the invoke command to spawn a marble with a delay
    public void SpawnMarble()
    {
        Invoke("InvokeSpawn", 1f);
    }

    //function to spawn the marble
    void InvokeSpawn()
    {
        if (marble == null)
        {
            marble = Instantiate(prefab);
            marble.gameObject.transform.position = transform.position;
            marbleScript = marble.GetComponent<Marble>();
        }
    }

    //function to destroy the marble
    void DestroyMarble()
    {
        explosion = Instantiate(explosionFX);
        explosion.transform.position = marble.transform.position;
        Destroy(marble);
        Destroy(explosion, 2);
    }
}
