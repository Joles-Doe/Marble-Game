using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBaseplate : MonoBehaviour
{
    public Cinemachine.CinemachineVirtualCamera virtualCamera;

    Vector2 mouseDelta;
    Vector2 mouseClamp = new Vector2(0f, 0f);

    Quaternion camOffset;
    Quaternion rotX;
    Quaternion rotY;
    Quaternion rotZ;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        camOffset = virtualCamera.transform.rotation; 
        camOffset.x = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        mouseClamp = new Vector2(mouseClamp.x + mouseDelta.x, mouseClamp.y + mouseDelta.y);

        mouseClamp.x = Mathf.Clamp(mouseClamp.x, -20f, 20f);
        mouseClamp.y = Mathf.Clamp(mouseClamp.y, -20f, 20f);

        rotX = Quaternion.AngleAxis(mouseClamp.x, camOffset * Vector3.back);
        rotX.y = 0f;
        rotY = Quaternion.Euler(1f, 1f, 1f);
        rotZ = Quaternion.AngleAxis(mouseClamp.y, camOffset * Vector3.right);
        rotZ.y = 0f;

        transform.rotation = rotX * rotZ;

        Debug.Log("x Rot y axis: " + rotX.y);
        Debug.Log("z Rot y axis: " + rotZ.y);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("ROTATE!!");
            rotY *= Quaternion.AngleAxis(20, Vector3.up);
            //transform.Rotate(0f, 200f, 0f, Space.Self);
            //transform.rotation *= Quaternion.Euler(1f, 20f, 1f);
        }
    }
}
