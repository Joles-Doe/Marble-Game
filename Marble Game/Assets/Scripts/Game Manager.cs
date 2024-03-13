using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public HUDManager HUDManager;
    public MoveCamera followPoint;
    public GameObject plateParent;

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
        if (checkCamera)
        {
            if (followPoint.IsMoving())
            {
                checkCamera = false;
                UnlockCurrentPlate();
                UnlockCurrentSpawners();
            }
        }
    }

    public void GameBegin()
    {
        Time.timeScale = 1f;
        MoveNextLevel();
    }

    public void GamePause()
    {
        Time.timeScale = 0f;
        LockCurrentPlate();
    }

    public void GameResume()
    {
        Time.timeScale = 1f;
        UnlockCurrentPlate();
    }

    public void MoveNextLevel()
    {
        currentLevel++;
        LockCurrentPlate();
        followPoint.NextLevel(42f);
        checkCamera = true;
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

    public void UnlockCurrentSpawners()
    {
        plateManagers[currentLevel].UnlockSpawners();
    }
}
