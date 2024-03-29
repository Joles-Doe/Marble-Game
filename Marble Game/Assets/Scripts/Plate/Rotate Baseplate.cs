using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RotateBaseplate : MonoBehaviour
{
    public Transform plate;
    Rigidbody plateRB;
    [HideInInspector] public bool focused;

    Vector2 mouseDelta;
    Vector2 mouseClamp = new Vector2(0f, 0f);

    //// comment / uncomment dependent on if you're using smooth turning
    //float timerStart;
    //float timerCurrent;
    //float timerStep;
    //public float easeDuration = 4f;

    // comment / uncomment dependent on if you're using smooth turning
    public float rotSpeed = 175f;

    public float rotSensitivity;

    Quaternion targetRot;
    [HideInInspector] public bool rotateY;
    float originY;
    float currentY;

    // Start is called before the first frame update
    void Start()
    {
        plateRB = plate.GetComponent<Rigidbody>();
        plateRB.collisionDetectionMode = CollisionDetectionMode.Discrete;
        focused = false;

        originY = plate.transform.eulerAngles.y;
        currentY = plate.transform.eulerAngles.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (focused)
        {
            if (plateRB.collisionDetectionMode != CollisionDetectionMode.Continuous)
            {
                plateRB.collisionDetectionMode = CollisionDetectionMode.Continuous;
            }
            // Records mouse movement and clamps both axis between -30 and 30
            mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
            mouseClamp = new Vector2(mouseClamp.x + mouseDelta.x * rotSensitivity, mouseClamp.y + mouseDelta.y * rotSensitivity);
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
                    plate.rotation = Quaternion.Euler(mouseClamp.y, currentY, -mouseClamp.x);
                }
                else if (Mathf.Approximately(currentY, 90f))
                {
                    plate.rotation = Quaternion.Euler(mouseClamp.x, currentY, mouseClamp.y);
                }
                else if (Mathf.Approximately(currentY, 180f))
                {
                    plate.rotation = Quaternion.Euler(-mouseClamp.y, currentY, mouseClamp.x);
                }
                else if (Mathf.Approximately(currentY, -90f))
                {
                    plate.rotation = Quaternion.Euler(-mouseClamp.x, currentY, -mouseClamp.y);
                }
            }
            else
            {
                //// == Use this if you want to use smooth turning, instead of an ease-in, ease-out effect == \\\\\\
                // rotate towards the target rotation, at a speed of (speed * deltatime)
                plate.rotation = Quaternion.RotateTowards(plate.rotation, targetRot, rotSpeed * Time.deltaTime);
                // checks if the object has reached the target rotation or has exceeded it
                if (Mathf.Approximately(plate.transform.rotation.y, targetRot.y))
                {
                    rotateY = false;
                    mouseClamp = new Vector2(plate.rotation.y, plate.rotation.x);
                }

                ////// == Use this if you want to use turning with an ease-in, ease-out effect == \\\\
                //// calculates current interpolation value for rotation
                //timerCurrent = Time.time - timerStart;
                //timerStep = Mathf.SmoothStep(0f, 1f, timerCurrent / easeDuration);
                //// rotates object
                //plate.rotation = Quaternion.Slerp(plate.rotation, targetRot, timerStep);
                //// checks if the object has reached the target rotation or has exceeded it
                //if (Mathf.Approximately(plate.rotation.y, targetRot.y))
                //{
                //    rotateY = false;
                //    mouseClamp = new Vector2(plate.rotation.y, plate.rotation.x);
                //}
            }
        }
        else
        {
            if (plateRB.collisionDetectionMode != CollisionDetectionMode.Discrete)
            {
                plateRB.collisionDetectionMode = CollisionDetectionMode.Discrete;
            }
        }
    }

    public void Rotate()
    {
        // set target rotation
        if (rotateY == false)
        {
            currentY += 90f;
            targetRot = Quaternion.Euler(0f, currentY, 0f);
        }
        rotateY = true;
        
        //// comment / uncomment dependent on if you're using smooth turning
        //timerStart = Time.time;
    }

    public void Reset()
    {
        plate.transform.rotation = Quaternion.Euler(0f, originY, 0f);
    }
}
