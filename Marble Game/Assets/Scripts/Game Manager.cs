using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public HUDManager HUDManager;
    public MoveCamera followPoint;
    public GameObject plateParent;
    public Timer timer;

    List<RotateBaseplate> plates = new List<RotateBaseplate>();
    List<PlateManager> plateManagers = new List<PlateManager>();

    int currentLevel = -1;

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
                MoveNextLevel();
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
            if (timer.timerNum <= 0)
            {
                //game over stuff
            }
        }
    }

    public void GameBegin()
    {
        Time.timeScale = 1f;
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
}
