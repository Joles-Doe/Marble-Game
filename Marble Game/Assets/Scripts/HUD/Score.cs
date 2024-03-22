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
        scoreText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddScore(float _input)
    {
        scoreValue += Mathf.RoundToInt(_input);
        scoreText.text = scoreValue.ToString();
    }

    public void Reset()
    {
        scoreValue = 0;
    }
}
