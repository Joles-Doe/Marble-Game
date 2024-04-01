using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public float cameraSpeed;
    bool move = false;

    Vector3 targetPos;

    // Start is called before the first frame update
    void Start()
    {
        targetPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //if the camera should move, steadily move towards the target
        if (move)
        {
            if (transform.position == targetPos)
            {
                move = false;
            }
            transform.position = Vector3.MoveTowards(transform.position, targetPos, cameraSpeed * Time.deltaTime);
        }
    }

    //function to move the point down by a set distance
    public void NextLevel(float _distance)
    {
        targetPos = new Vector3(targetPos.x, targetPos.y - _distance, targetPos.z);
        move = true;
    }

    //function to manually set the position of the camera
    public void SetPos(float _pos)
    {
        transform.position = new Vector3(transform.position.x, _pos, transform.position.z);
        targetPos = transform.position;
    }

    //function to return whether the camera is moving
    public bool IsMoving()
    {
        return !move;
    }
}
