using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUDScreen : MonoBehaviour
{
    [HideInInspector] public bool move = false;
    bool resetPos = true;

    Vector3 offscreenTextPos;
    Vector3 movePos;

    TextMeshProUGUI levelTMP;

    public float textSpeed = 20f;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
        levelTMP = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        offscreenTextPos = transform.GetChild(0).transform.position;
        movePos = new Vector3(offscreenTextPos.x * -1, offscreenTextPos.y, offscreenTextPos.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (move)
        {
            if (levelTMP.transform.position == movePos)
            {
                resetPos = true;
                move = false;
            }
            if (resetPos)
            {
                levelTMP.transform.position = offscreenTextPos;
                resetPos = false;
            }
            levelTMP.transform.position = Vector3.MoveTowards(levelTMP.transform.position, movePos, textSpeed * Time.deltaTime);
        }
    }

    public void UpdateText(string level)
    {
        levelTMP.text = level;
    }
}
