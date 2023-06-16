using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameScore : MonoBehaviour
{
    Text scoreTextUI;
    int score;

    public int Score
    {
        get
        {
            return this.score;
        }
        set
        {
            this.score = value;
            UpdateScoreUIText();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        scoreTextUI = GetComponent<Text>();
    }
    void UpdateScoreUIText()
    {
        string scoreStr = string.Format("{0:000000}", score);
        scoreTextUI.text = scoreStr;
    }
}
