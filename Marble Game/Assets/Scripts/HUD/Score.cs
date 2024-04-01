using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    TextMeshProUGUI scoreText;

    int scoreValue;

    // Start is called before the first frame update
    void Start()
    {
        //grabs the TMP component
        scoreText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //function to add a set amount to the score
    public void AddScore(float _input)
    {
        scoreValue += Mathf.RoundToInt(_input);
        scoreText.text = scoreValue.ToString();
    }

    //resets the score
    public void Reset()
    {
        scoreValue = 0;
    }
}
