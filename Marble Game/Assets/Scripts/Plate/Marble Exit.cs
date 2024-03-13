using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarbleExit : MonoBehaviour
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

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Marble>() != null)
        {
            marbleScript = other.GetComponent<Marble>();
            marbleScript.destroy = true;
        }
    }
}
