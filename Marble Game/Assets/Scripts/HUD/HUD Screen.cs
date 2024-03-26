using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDScreen : MonoBehaviour
{
    [HideInInspector] public bool move = false;

    Vector3 offscreenTextPos;
    Vector3 movePos;

    public TextMeshProUGUI levelTMP;
    public float textSpeed = 20f;

    List<Image> hearts = new List<Image>();
    bool colorChange = false;
    int heartsIndex = 3;

    float lerpTimer;

    // Start is called before the first frame update
    void Start()
    {
        offscreenTextPos = transform.GetChild(0).transform.position;
        movePos = new Vector3(offscreenTextPos.x * -1, offscreenTextPos.y, offscreenTextPos.z);

        foreach (Transform child in GetComponentsInChildren<Transform>())
        {
            if (child.name == "Heart")
            {
                hearts.Add(child.GetComponent<Image>());
            }
        }

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
        if (colorChange)
        {
            lerpTimer += Time.deltaTime * 1.5f;
            hearts[heartsIndex].color = Color.Lerp(Color.white, Color.black, lerpTimer);
            if (lerpTimer >= 1)
            {
                colorChange = false;
            }
        }
    }

    public void UpdateText(string level)
    {
        levelTMP.text = level;
    }

    public void LoseLife()
    {
        if (heartsIndex > 0)
        {
            heartsIndex--;
            colorChange = true;
            lerpTimer = 0;
        }
    }

    public void Reset()
    {
        heartsIndex = 3;
        for (int x = 0; x < hearts.Count; x++)
        {
            hearts[0].color = Color.white;
        }
    }
}
