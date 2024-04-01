using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleRotate : MonoBehaviour
{
    //simple script to smoothly rotate the attached object

    public PlateManager manager;

    public float rotSpeed;
    [HideInInspector] bool isActive;

    // Start is called before the first frame update
    void Start()
    {
        isActive = manager.activePlate;
    }

    // Update is called once per frame
    void Update()
    {
        isActive = manager.activePlate;

        if (isActive)
        {
            transform.Rotate(Vector3.up * rotSpeed * Time.deltaTime);
        }
    }
}
