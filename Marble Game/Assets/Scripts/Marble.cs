using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marble : MonoBehaviour
{
    public bool canLeave;
    public bool destroy;

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //function that gets called to shoot the marble straight down
    public void MoveDown()
    {
        rb.AddForce(Vector3.down * 20, ForceMode.Impulse);
    }
}
