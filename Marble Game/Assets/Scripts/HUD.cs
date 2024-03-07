using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD : MonoBehaviour
{
    public GameManager gameManager;

    List<GameObject> screens = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject screen in screens) 
        {
            screens.Add(screen);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
