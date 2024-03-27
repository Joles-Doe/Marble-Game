using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarbleExit : MonoBehaviour
{
    Marble marbleScript;
    AudioSource exitSound;

    // Start is called before the first frame update
    void Start()
    {
        exitSound = GetComponent<AudioSource>();
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
            if (marbleScript.canLeave == false)
            {
                exitSound.Play();
            }
        }
    }
}
