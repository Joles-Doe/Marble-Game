using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Build.Reporting;
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

    Quaternion targetRot;
    bool rotateY;
    float targetRotY = 0f;
    float step;
    float speed = 60;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        camOffset = virtualCamera.transform.rotation; 
        camOffset.x = 0f;

        rotY = Quaternion.Euler(0f, 0f, 0f);
        targetRot = Quaternion.Euler(0f, targetRotY, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        mouseClamp = new Vector2(mouseClamp.x + mouseDelta.x, mouseClamp.y + mouseDelta.y);
        mouseClamp.x = Mathf.Clamp(mouseClamp.x, -30f, 30f);
        mouseClamp.y = Mathf.Clamp(mouseClamp.y, -30f, 30f);

        if (rotateY == false)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rotateY = true;
                targetRotY = transform.rotation.y + 90f;
                targetRot = Quaternion.AngleAxis(targetRotY, Vector3.up);
            }

            //transform.rotation = rotX * rotZ;
            transform.rotation = Quaternion.Euler(mouseClamp.y, targetRotY, -mouseClamp.x);
        }
        else
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, speed * Time.deltaTime);
            //transform.rotation *= Quaternion.AngleAxis(speed, Vector3.up);
            if (transform.rotation.y == targetRot.y)
            {
                rotY.y = targetRot.y;
                rotateY = false;
            }
        }
    }

    void DEP_rotate()
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

    void DEP_rotate2()
    {
        mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        mouseClamp = new Vector2(mouseClamp.x + mouseDelta.x, mouseClamp.y + mouseDelta.y);
        mouseClamp.x = Mathf.Clamp(mouseClamp.x, -30f, 30f);
        mouseClamp.y = Mathf.Clamp(mouseClamp.y, -30f, 30f);

        if (rotateY == false)
        {
            rotX = Quaternion.AngleAxis(mouseClamp.y, Vector3.right);
            rotX.y = 0;

            rotZ = Quaternion.AngleAxis(mouseClamp.x, Vector3.back);
            rotZ.y = 0;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("ROTATE!!");
                rotateY = true;
                targetRotY = transform.rotation.y + 90f;
                targetRot = Quaternion.AngleAxis(targetRotY, Vector3.up);
            }

            transform.rotation = rotX * rotZ;
        }
        else
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, speed * Time.deltaTime);
            //transform.rotation *= Quaternion.AngleAxis(speed, Vector3.up);
            if (transform.rotation.y == targetRot.y)
            {
                rotY.y = targetRot.y;
                rotateY = false;
            }
        }
    }
}
