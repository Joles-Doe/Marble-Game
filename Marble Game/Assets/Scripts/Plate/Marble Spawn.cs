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
                if (marbleScript.destroy)
                {
                    if (marbleScript.canLeave)
                    {
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

    public void SpawnMarble()
    {
        Invoke("InvokeSpawn", 1f);
    }

    void InvokeSpawn()
    {
        if (marble == null)
        {
            marble = Instantiate(prefab);
            marble.gameObject.transform.position = transform.position;
            marbleScript = marble.GetComponent<Marble>();
        }
    }

    void DestroyMarble()
    {
        explosion = Instantiate(explosionFX);
        explosion.transform.position = marble.transform.position;
        Destroy(marble);
        Destroy(explosion, 2);
    }
}
