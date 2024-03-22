using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndScreen : MonoBehaviour
{
    public GameManager manager;

    Button restartButton;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in GetComponentsInChildren<Transform>())
        {
            if (child.name == "Restart Button")
            {
                restartButton = child.GetComponent<Button>();
            }
        }
        restartButton.onClick.AddListener(Restart);
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Activate()
    {
        gameObject.SetActive(true);
    }

    void Restart()
    {
        manager.GameBegin();
        gameObject.SetActive(false);
    }
}
