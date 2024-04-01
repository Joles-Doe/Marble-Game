using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Timer : MonoBehaviour
{
    AudioSource tickSound;
    TextMeshProUGUI timerText;

    bool canTick;
    [HideInInspector]public bool increaseTimer;

    [HideInInspector]public int timerNum;
    int timerOld;
    int timerStart;
    int timerTarget;

    public int increaseNum = 15;
    float incrementTimerDelay;

    float deltaTimer = 0f;
    float lerpTimer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        //grabs all necessary components
        timerNum = 0;
        timerText = GetComponent<TextMeshProUGUI>();
        tickSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // small note to self
        // takes 4.12 seconds for the camera to move to the next level

        //if the timer should increase,
        if (increaseTimer)
        {
            //use a lerp to set the current time between the old time and the target
            lerpTimer += Time.deltaTime / 4;
            timerNum = (int)Mathf.Round(Mathf.Lerp(timerStart, timerTarget, lerpTimer));

            if (timerNum != timerOld)
            {
                tickSound.Play();
                timerOld = timerNum;
            }
            if (timerNum >= timerTarget)
            {
                increaseTimer = false;
                deltaTimer = 0f;
            }
        }
        //normal tick behaviour
        else if (canTick)
        {
            deltaTimer += Time.deltaTime;
            if (deltaTimer >= 1f)
            {
                timerNum--;
                tickSound.Play();
                deltaTimer = 0f;
            }
        }
        //sets the text to the time variable
        timerText.text = timerNum.ToString();
    }

    //function to start ticking
    public void StartTick()
    {
        canTick = true;
    }

    //function to stop ticking
    public void StopTick()
    {
        canTick = false;
    }

    //function to increase the timer
    public void IncrementTimer()
    {
        timerStart = timerNum;
        timerTarget = timerNum + increaseNum;
        timerOld = 0;
        lerpTimer = 0f;
        increaseTimer = true;
    }

    //overloaded function to specify what the target variable should be
    public void IncrementTimer(int target)
    {
        timerStart = 0;
        timerTarget = target;
        timerOld = 0;
        lerpTimer = 0f;
        increaseTimer = true;
    }
}
