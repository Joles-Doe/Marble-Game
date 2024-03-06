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
        rotZ = Quaternion.AngleAxis(mouseClamp.y, camOffset * Vector3.right);
        transform.rotation = rotX * rotZ * Quaternion.Euler(1f, 0f, 1f);
    }
}
