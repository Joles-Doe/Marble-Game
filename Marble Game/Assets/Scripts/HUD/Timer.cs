using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    TextMeshProUGUI timerText;

    bool canTick;
    bool incraseTimer;

    [HideInInspector]public int timerNum;
    public float increaseNum = 10f;

    // Start is called before the first frame update
    void Start()
    {
        timerText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (incraseTimer)
        {

        }
        else if (canTick)
        {

        }
    }

    public void StartTick()
    {
        canTick = true;
    }

    public void StopTick()
    {
        canTick = false;
    }

    public void IncrementTimer()
    {

    }
}
