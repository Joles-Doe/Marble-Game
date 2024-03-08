using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public MoveCamera followPoint;
    public GameObject plateParent;
    List<GameObject> plates = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameBegin()
    {
        Time.timeScale = 1f;
        followPoint.NextLevel(42f);
    }

    public void GamePause()
    {
        Time.timeScale = 0f;
    }

    public void GameResume()
    {
        Time.timeScale = 1f;
    }
}
