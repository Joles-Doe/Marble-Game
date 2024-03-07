using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Build.Reporting;
using UnityEngine;

public class RotateBaseplate : MonoBehaviour
{
    bool focused;

    Vector2 mouseDelta;
    Vector2 mouseClamp = new Vector2(0f, 0f);

    //// comment / uncomment dependent on if you're using smooth turning
    //float timerStart;
    //float timerCurrent;
    //float timerStep;
    //public float easeDuration = 4f;

    // comment / uncomment dependent on if you're using smooth turning
    public float rotSpeed = 100f;

    Quaternion targetRot;
    [HideInInspector] public bool rotateY;
    float currentY = 0f;

    // Start is called before the first frame update
    void Start()
    {
        focused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (focused)
        {
            // Records mouse movement and clamps both axis between -30 and 30
            mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
            mouseClamp = new Vector2(mouseClamp.x + mouseDelta.x, mouseClamp.y + mouseDelta.y);
            mouseClamp.x = Mathf.Clamp(mouseClamp.x, -60, 60f);
            mouseClamp.y = Mathf.Clamp(mouseClamp.y, -60f, 60f);

            // if statement to check if the object should be rotating on the Y axis
            if (rotateY == false)
            {
                // checks if currentY has exceeded bounds of 180
                if (Mathf.Approximately(currentY, 270f))
                {
                    currentY = -90f;
                }
                // sets current rotation
                if (Mathf.Approximately(currentY, 0f))
                {
                    transform.rotation = Quaternion.Euler(mouseClamp.y, currentY, -mouseClamp.x);
                }
                else if (Mathf.Approximately(currentY, 90f))
                {
                    transform.rotation = Quaternion.Euler(mouseClamp.x, currentY, mouseClamp.y);
                }
                else if (Mathf.Approximately(currentY, 180f))
                {
                    transform.rotation = Quaternion.Euler(-mouseClamp.y, currentY, mouseClamp.x);
                }
                else if (Mathf.Approximately(currentY, -90f))
                {
                    transform.rotation = Quaternion.Euler(-mouseClamp.x, currentY, -mouseClamp.y);
                }

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    rotateY = true;
                    // set target rotation
                    currentY += 90f;
                    targetRot = Quaternion.Euler(0f, currentY, 0f);

                    //// comment / uncomment dependent on if you're using smooth turning
                    //timerStart = Time.time;
                }
            }
            else
            {
                //// == Use this if you want to use smooth turning, instead of an ease-in, ease-out effect == \\\\\\
                // rotate towards the target rotation, at a speed of (speed * deltatime)
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, rotSpeed * Time.deltaTime);
                // checks if the object has reached the target rotation or has exceeded it
                if (Mathf.Approximately(transform.rotation.y, targetRot.y))
                {
                    rotateY = false;
                    mouseClamp = new Vector2(transform.rotation.y, transform.rotation.x);
                }

                ////// == Use this if you want to use turning with an ease-in, ease-out effect == \\\\
                //// calculates current interpolation value for rotation
                //timerCurrent = Time.time - timerStart;
                //timerStep = Mathf.SmoothStep(0f, 1f, timerCurrent / easeDuration);
                //// rotates object
                //transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, timerStep);
                //// checks if the object has reached the target rotation or has exceeded it
                //if (Mathf.Approximately(transform.rotation.y, targetRot.y))
                //{
                //    rotateY = false;
                //    mouseClamp = new Vector2(transform.rotation.y, transform.rotation.x);
                //}
            }
        }
    }
}
