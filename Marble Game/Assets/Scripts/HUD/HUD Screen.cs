using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUDScreen : MonoBehaviour
{
    [HideInInspector] public bool move = false;

    Vector3 offscreenTextPos;
    Vector3 movePos;

    public TextMeshProUGUI levelTMP;
    public GameObject gameOverPanel;

    public float textSpeed = 20f;

    // Start is called before the first frame update
    void Start()
    {
        offscreenTextPos = transform.GetChild(0).transform.position;
        movePos = new Vector3(offscreenTextPos.x * -1, offscreenTextPos.y, offscreenTextPos.z);
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (move)
        {
            levelTMP.transform.position = Vector3.MoveTowards(levelTMP.transform.position, movePos, textSpeed * Time.deltaTime);
            if (levelTMP.transform.position.x <= -840)//movePos.x)
            {
                levelTMP.transform.position = offscreenTextPos;
                move = false;
            }
        }
    }

    public void UpdateText(string level)
    {
        levelTMP.text = level;
    }
}
