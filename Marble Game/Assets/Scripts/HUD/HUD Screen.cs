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
        //grabs needed components and sets variables for the level text
        foreach (Transform child in GetComponentsInChildren<Transform>())
        {
            if (child.name == "Heart")
            {
                hearts.Add(child.GetComponent<Image>());
            }
            if (child.name == "Level Text")
            {
                offscreenTextPos = child.transform.position;
                movePos = new Vector3(offscreenTextPos.x * -1, offscreenTextPos.y, offscreenTextPos.z);
            }
        }

        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        ///Level text moving breaks on build, so this is commented out
        //if move is true, move the text across the screen
        //if (move)
        //{
        //    levelTMP.transform.position = Vector3.MoveTowards(levelTMP.transform.position, movePos, textSpeed * Time.deltaTime);
        //    if (levelTMP.transform.position.x <= -840)
        //    {
        //        levelTMP.transform.position = offscreenTextPos;
        //        move = false;
        //    }
        //}
        //if colorChange is true, sets a functional heart to black
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

    //updates text
    public void UpdateText(string level)
    {
        levelTMP.text = level;
    }

    //sets colorChange to true and reduces the index
    public void LoseLife()
    {
        if (heartsIndex > 0)
        {
            heartsIndex--;
            colorChange = true;
            lerpTimer = 0;
        }
    }

    //resets the hearts
    public void Reset()
    {
        heartsIndex = 3;
        for (int x = 0; x < hearts.Count; x++)
        {
            hearts[x].color = Color.white;
        }
    }
}
