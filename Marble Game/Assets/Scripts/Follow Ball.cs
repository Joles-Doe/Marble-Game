using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FollowBall : MonoBehaviour
{
    public Vector3 offset = new Vector3 (0f, 0.5f, 0f);
    public GameObject marble;

    Vector2 mouseDelta;
    bool rotate = true;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(marble.transform.position.x + offset.x, marble.transform.position.y + offset.y, marble.transform.position.z + offset.z);

        mouseDelta = new Vector2(0, Input.GetAxis("Mouse X"));
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (rotate)
            {
                rotate = false;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                rotate = true;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            
        }

        if (rotate) 
        {
            transform.Rotate(mouseDelta);
        }
    }
}
