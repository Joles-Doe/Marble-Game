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
        if (move)
        {
            if (transform.position == targetPos)
            {
                move = false;
            }
            transform.position = Vector3.MoveTowards(transform.position, targetPos, cameraSpeed * Time.deltaTime);
        }
    }

    public void NextLevel(float _distance)
    {
        targetPos = new Vector3(targetPos.x, targetPos.y - _distance, targetPos.z);
        move = true;
    }

    public void SetPos(float _pos)
    {
        transform.position = new Vector3(transform.position.x, _pos, transform.position.z);
    }

    public bool IsMoving()
    {
        return !move;
    }
}
