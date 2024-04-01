using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Holeenter : MonoBehaviour
{
    public GameObject enterEffect;
    GameObject currentEffect;

    AudioSource enterSound;
    Marble marbleScript;

    // Start is called before the first frame update
    void Start()
    {
        enterSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //trigger function that shoots the marble downwards and creates a VFX to indicate the ball has gone through the hole
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Marble>() != null)
        {
            marbleScript = other.GetComponent<Marble>();
            enterSound.Play();
            currentEffect = Instantiate(enterEffect);
            currentEffect.transform.position = transform.position;
            Destroy(currentEffect, 4);
            marbleScript.canLeave = true;
            marbleScript.MoveDown();
        }
    }
}
