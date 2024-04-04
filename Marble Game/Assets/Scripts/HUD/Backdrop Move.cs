using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackdropMove : MonoBehaviour
{
    // values changed in the editor
    public bool moveDown; // denotes whether the element moves up or down
    float moveSpeed = 0.7f;

    int delay;
    int delayTimer;

    bool moveFirst = true;
    bool move;

    RectTransform parentTransform;

    Vector2 originPos;
    Vector2 targetPos;

    // Start is called before the first frame update
    void Start()
    {
        parentTransform = GetComponentInParent<RectTransform>();
        // initializes variables
        if (moveDown)
        {
            originPos = transform.position;
            targetPos = new Vector2(transform.position.x, transform.position.y - 205);
            transform.position = new Vector2(transform.position.x, transform.position.y * 3);
        }
        else
        {
            originPos = transform.position;
            targetPos = new Vector2(transform.position.x, transform.position.y + 205);
            transform.position = new Vector2(transform.position.x, transform.position.y * -2);
            //transform.position = new Vector2(transform.position.x, -parentTransform.rect.height);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (move)
        {
            if (delayTimer >= delay)
            {
                if (moveFirst)
                {
                    // upon first movement, the element moves onto the screen before continuing it's basic function
                    if (moveDown)
                    {
                        transform.position = new Vector2(transform.position.x, transform.position.y - moveSpeed);
                        if (transform.position.y <= originPos.y)
                        {
                            moveFirst = false;
                        }
                    }
                    else
                    {
                        transform.position = new Vector2(transform.position.x, transform.position.y + moveSpeed);
                        if (transform.position.y >= originPos.y)
                        {
                            moveFirst = false;
                        }
                    }
                }
                else
                {
                    // moves up / down a certain amount before returning to its original position
                    if (moveDown)
                    {
                        transform.position = new Vector2(transform.position.x, transform.position.y - moveSpeed);
                        if (transform.position.y <= targetPos.y)
                        {
                            transform.position = originPos;
                        }
                    }
                    else
                    {
                        transform.position = new Vector2(transform.position.x, transform.position.y + moveSpeed);
                        if (transform.position.y >= targetPos.y)
                        {
                            transform.position = originPos;
                        }
                    }
                }
            }
            else
            {
                delayTimer += 1;
            }
            
        }
    }

    // allows the element to move
    public void Activate(int _delay, float _moveSpeed)
    {
        move = true;
        // sets a delay for the element to move
        delay = _delay;
        moveSpeed = _moveSpeed;
    }
}
