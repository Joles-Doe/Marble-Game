using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsScreen : MonoBehaviour
{
    public GameManager GameManager;
    public HUDManager HUDManager;

    public RotateBaseplate settingsPlate;
    public Cinemachine.CinemachineVirtualCamera settingsCam;

    Button invertButton;
    Button returnButton;
    Slider volumeSlider;
    Slider sensSlider;

    bool ignoreEvent = true;

    // Start is called before the first frame update
    void Start()
    {
        //grabs each button component and gives them listeners
        foreach (Transform child in GetComponentsInChildren<Transform>())
        {
            if (child.name == "Invert Button")
            {
                invertButton = child.GetComponent<Button>();
            }
            if (child.name == "Return Button")
            {
                returnButton = child.GetComponent<Button>();
            }
            if (child.name == "Volume Slider")
            {
                volumeSlider = child.GetComponent<Slider>();
            }
            if (child.name == "Sensitivity Slider")
            {
                sensSlider = child.GetComponent<Slider>();
            }
        }
        invertButton.onClick.AddListener(InvertButtonClicked);
        returnButton.onClick.AddListener(ReturnButtonClicked);
        volumeSlider.value = GameManager.audioSliderValue;
        sensSlider.value = GameManager.audioSliderValue;
        ignoreEvent = false;
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //changes the camera view to the settings camera and allows the plate to be moved
    public void Activate()
    {
        volumeSlider.value = GameManager.audioSliderValue;
        sensSlider.value = GameManager.sensSliderValue;
        settingsPlate.rotSensitivity = sensSlider.value;
        settingsPlate.invert = GameManager.invertY;
        settingsCam.Priority = 20;
        settingsPlate.focused = true;
    }

    //changes the camera back to the main camera and freezes the plate
    public void Deactivate()
    {
        settingsCam.Priority = 9;
        settingsPlate.focused = false;
    }

    //calls a function in game manager to invert the Y axis
    public void InvertButtonClicked()
    {
        if (GameManager.invertY == false)
        {
            PlayerPrefs.SetInt("invertY", 1);
        }
        else
        {
            PlayerPrefs.SetInt("invertY", 0);
        }
        GameManager.invertY = !GameManager.invertY;
        GameManager.SettingsInvertY();
        settingsPlate.invert = GameManager.invertY;
    }

    //function to return to the main menu
    public void ReturnButtonClicked()
    {
        Deactivate();
        HUDManager.OpenMenu();
        gameObject.SetActive(false);
    }

    //function to react when the volume slider is changed
    public void VolumeChanged()
    {
        if (!ignoreEvent)
        {
            GameManager.SettingsChangeVolume((int)volumeSlider.value);
        }
    }

    //function to react when the sens slider is changed
    public void SensitivityChanged()
    {
        if (!ignoreEvent)
        {
            GameManager.SettingsChangeSensitivity(sensSlider.value);
            settingsPlate.rotSensitivity = sensSlider.value;
        }
    }
}
