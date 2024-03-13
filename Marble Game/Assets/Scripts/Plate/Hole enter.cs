using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Holeenter : MonoBehaviour
{
    Marble marbleScript;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Marble>() != null)
        {
            marbleScript = other.GetComponent<Marble>();
            marbleScript.canLeave = true;
            marbleScript.MoveDown();
        }
    }
}
