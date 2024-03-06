using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBaseplate : MonoBehaviour
{
    public Cinemachine.CinemachineVirtualCamera virtualCamera;

    Quaternion camOffset;
    Vector2 mouseDelta;
    Vector2 mouseClamp = new Vector2(0f, 0f);

    Quaternion rot;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        rot = Quaternion.identity;
        camOffset = virtualCamera.transform.rotation; 
    }

    // Update is called once per frame
    void Update()
    {
        mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        mouseClamp = new Vector2(mouseClamp.x + mouseDelta.x, mouseClamp.y + mouseDelta.y);

        if (mouseClamp.x > 20)
        {
            mouseClamp.x = 20;
        }
        if (mouseClamp.y > 20)
        {
            mouseClamp.y = 20;
        }

        if (mouseClamp.x < -20)
        {
            mouseClamp.x = -20;
        }
        if (mouseClamp.y < -20)
        { 
            mouseClamp.y = -20;
        }

        transform.rotation = Quaternion.Euler(-mouseClamp.y, transform.rotation.y, -mouseClamp.x);
    }
}
