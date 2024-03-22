using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public HUDManager HUDManager;
    public MoveCamera followPoint;
    public GameObject plateParent;
    public Timer timer;
    public Score score;

    List<RotateBaseplate> plates = new List<RotateBaseplate>();
    List<PlateManager> plateManagers = new List<PlateManager>();

    int currentLevel = -1;
    int lives = 3;

    bool checkCamera = false;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0f;

        foreach (Transform child in plateParent.GetComponentsInChildren<Transform>())
        {
            if (child.GetComponent<RotateBaseplate>() != null)
            {
                plates.Add(child.GetComponent<RotateBaseplate>());
            }
            if (child.GetComponent<PlateManager>() != null)
            {
                plateManagers.Add(child.GetComponent<PlateManager>());
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (currentLevel != -1)
        {
            if (plateManagers[currentLevel].levelComplete == true)
            {
                if (plateManagers[currentLevel] == plateManagers[0])
                {
                    HUDManager.OpenEndScreen();
                }
                else
                {
                    MoveNextLevel();
                }
                timer.StopTick();
            }
            if (checkCamera)
            {
                if (followPoint.IsMoving())
                {
                    checkCamera = false;
                    UnlockCurrentPlate();
                    UnlockCurrentSpawners();
                    timer.StartTick();
                }
            }
            if (timer.timerNum <= 0 || lives <= 0)
            {
                if (timer.increaseTimer == false)
                {
                    timer.StopTick();
                    HUDManager.OpenEndScreen();
                }
                
            }
        }
    }

    public void GameBegin()
    {
        Time.timeScale = 1f;
        currentLevel = -1;
        lives = 3;
        foreach (RotateBaseplate plate in plates)
        {
            plate.Reset();
        }
        foreach (PlateManager plate in plateManagers)
        {
            plate.Reset();
        }
        followPoint.SetPos(50);
        MoveNextLevel();
        timer.IncrementTimer(120);
    }

    public void GamePause()
    {
        Time.timeScale = 0f;
        plateManagers[currentLevel].activePlate = false;
        timer.StopTick();
        LockCurrentPlate();
    }

    public void GameResume()
    {
        Time.timeScale = 1f;
        plateManagers[currentLevel].activePlate = true;
        timer.StartTick();
        UnlockCurrentPlate();
    }

    public void MoveNextLevel()
    {
        if (currentLevel != -1)
        {
            plateManagers[currentLevel].activePlate = false;
            score.AddScore((currentLevel * 10) + timer.timerNum);
        }
        currentLevel++;
        plateManagers[currentLevel].activePlate = true;
        LockCurrentPlate();
        followPoint.NextLevel(42f);
        checkCamera = true;
        timer.IncrementTimer();
        //HUDManager.HUDNextLevelText(plates[currentLevel].name);
        HUDManager.HUDNextLevelText($"Level {currentLevel + 1}");
    }

    public void UnlockCurrentPlate()
    {
        plates[currentLevel].focused = true;
    }

    public void LockCurrentPlate()
    {
        plates[currentLevel].focused = false;
    }

    public void RotateCurrentPlate()
    {
        plates[currentLevel].Rotate();
    }

    public void UnlockCurrentSpawners()
    {
        plateManagers[currentLevel].UnlockSpawners();
    }

    public void LoseLife()
    {
        lives--;
    }
}
