using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour
{
    public HUDManager HUDManager;
    public MoveCamera followPoint;
    public GameObject plateParent;
    public Timer timer;
    public Score score;

    List<RotateBaseplate> plates = new List<RotateBaseplate>();
    List<PlateManager> plateManagers = new List<PlateManager>();

    GameObject[] activeMarbles;

    int currentLevel = -1;
    int lives = 3;

    bool checkCamera = false;

    public AudioMixer audio;
    [HideInInspector] public int audioSliderValue;
    [HideInInspector] public float sensSliderValue;
    [HideInInspector] public bool invertY;

    // Start is called before the first frame update
    void Start()
    {
        //grabs all of the necessary components
        foreach (Transform child in plateParent.GetComponentsInChildren<Transform>())
        {
            if (child.GetComponent<RotateBaseplate>() != null)
            {
                if (child.name != "Settings Plate")
                {
                    plates.Add(child.GetComponent<RotateBaseplate>());
                }
            }
            if (child.GetComponent<PlateManager>() != null)
            {
                plateManagers.Add(child.GetComponent<PlateManager>());
            }
        }
        //grabs settings, if they exist
        if (PlayerPrefs.HasKey("invertY"))
        {
            if (PlayerPrefs.GetInt("invertY") == 0)
            {
                invertY = false;
            }
            else
            {
                invertY = true;
                SettingsInvertY();
            }
        }
        else
        {
            invertY = false;
            PlayerPrefs.SetInt("invertY", 0);
        }
        if (PlayerPrefs.HasKey("volume"))
        {
            audioSliderValue = PlayerPrefs.GetInt("volume", 80);
            audio.SetFloat("volume", -80 + audioSliderValue);
        }
        else
        {
            audioSliderValue = 80;
            SettingsChangeVolume(audioSliderValue);
        }
        if (PlayerPrefs.HasKey("sensitivity"))
        {
            sensSliderValue = PlayerPrefs.GetFloat("sensitivity", 0.3f);
            SettingsChangeSensitivity(sensSliderValue);
        }
        else
        {
            sensSliderValue = 0.3f;
            SettingsChangeSensitivity(sensSliderValue);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentLevel != -1)
        {
            //open the menu if escape is pressed
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (Time.timeScale == 1)
                {
                    GamePause();
                }
                else
                {
                    GameResume();
                }
            }
            //move to the next level if the current level has been completed
            if (plateManagers[currentLevel].levelComplete == true)
            {
                //if the plate is the final one in the array, end the game
                if (plateManagers[currentLevel] == plateManagers[^1])
                {
                    HUDManager.OpenEndScreen();
                    LockCurrentPlate();
                    LockCurrentSpawners();
                    currentLevel = -1;
                }
                else
                {
                    MoveNextLevel();
                }
                timer.StopTick();
            }
            //if the camera is moving
            if (checkCamera)
            {
                //once the camera stops moving, unlock the plate and start the timer
                if (followPoint.IsMoving())
                {
                    checkCamera = false;
                    UnlockCurrentPlate();
                    UnlockCurrentSpawners();
                    timer.StartTick();
                }
            }
            //if the timer reaches 0 or if the player runs out of lives
            if (timer.timerNum <= 0 || lives <= 0)
            {
                if (timer.increaseTimer == false)
                {
                    timer.StopTick();
                    LockCurrentPlate();
                    LockCurrentSpawners();
                    HUDManager.OpenEndScreen();
                    currentLevel = -1;
                }
                
            }
        }
    }

    //function to begin the game - sets the timescale to 1 and resets everything to default values
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
        activeMarbles = GameObject.FindGameObjectsWithTag("Marble");
        foreach (GameObject child in activeMarbles)
        {
            Destroy(child);
        }
        HUDManager.HUDReset();
        followPoint.SetPos(50);
        MoveNextLevel();
        timer.IncrementTimer(120);
    }

    //function to freeze time, plate rotations and timer
    public void GamePause()
    {
        Time.timeScale = 0f;
        plateManagers[currentLevel].activePlate = false;
        timer.StopTick();
        HUDManager.PauseGame();
        LockCurrentPlate();
    }

    //function to resume time, plate rotations and timer
    public void GameResume()
    {
        Time.timeScale = 1f;
        plateManagers[currentLevel].activePlate = true;
        timer.StartTick();
        HUDManager.ResumeGame();
        UnlockCurrentPlate();
    }

    //function to send relevant information to different components on the next level
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

    //unlocks the current plate
    public void UnlockCurrentPlate()
    {
        plates[currentLevel].focused = true;
    }

    //locks the current plate
    public void LockCurrentPlate()
    {
        plates[currentLevel].focused = false;
    }

    //rotates the current plate by 90*
    public void RotateCurrentPlate()
    {
        plates[currentLevel].Rotate();
    }

    //unlocks current ball spawners
    public void UnlockCurrentSpawners()
    {
        plateManagers[currentLevel].UnlockSpawners();
    }

    //locks current ball spawners
    public void LockCurrentSpawners()
    {
        plateManagers[currentLevel].LockSpawners();
    }

    //reduces lives and sends information to HUD
    public void LoseLife()
    {
        lives--;
        HUDManager.HUDLoseLife();
    }

    //inverts the current way of rotation for the Y axis for all plates
    public void SettingsInvertY()
    {
        foreach (RotateBaseplate plate in plates)
        {
            plate.invert = invertY;
        }
    }

    //sets the current slider value in settings to game volume
    public void SettingsChangeVolume(int _input)
    {
        PlayerPrefs.SetInt("volume", _input);
        audioSliderValue = _input;
        audio.SetFloat("volume", -80 + _input);
    }

    //sets the sensitivity slider value in settings to game sensitivity
    public void SettingsChangeSensitivity(float _input)
    {
        PlayerPrefs.SetFloat("sensitivity", _input);
        sensSliderValue = _input;
        foreach (RotateBaseplate plate in plates)
        {
            plate.rotSensitivity = _input;
        }
    }
}
