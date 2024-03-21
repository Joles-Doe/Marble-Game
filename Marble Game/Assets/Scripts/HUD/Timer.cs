using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Timer : MonoBehaviour
{
    TextMeshProUGUI timerText;

    bool canTick;
    bool increaseTimer;

    [HideInInspector]public int timerNum;
    int timerStart;
    int timerTarget;

    public int increaseNum = 15;
    float incrementTimerDelay;

    float deltaTimer = 0f;
    float lerpTimer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        timerNum = 0;
        timerText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        // small note to self
        // takes 4.12 seconds for the camera to move to the next level
        if (increaseTimer)
        {
            lerpTimer += Time.deltaTime / 4;
            timerNum = (int)Mathf.Round(Mathf.Lerp(timerStart, timerTarget, lerpTimer));

            //if (deltaTimer >= incrementTimerDelay)
            //{
            //    timerNum++;
            //    deltaTimer = 0f;
            //}
            if (timerNum >= timerTarget)
            {
                increaseTimer = false;
                deltaTimer = 0f;
            }
        }
        else if (canTick)
        {
            deltaTimer += Time.deltaTime;
            if (deltaTimer >= 1f)
            {
                timerNum--;
                deltaTimer = 0f;
            }
        }
        timerText.text = timerNum.ToString();
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
        timerStart = timerNum;
        timerTarget = timerNum + increaseNum;
        lerpTimer = 0f;
        increaseTimer = true;
    }

    public void IncrementTimer(int target)
    {
        timerStart = timerNum;
        timerTarget = target;
        lerpTimer = 0f;
        increaseTimer = true;
    }
}
