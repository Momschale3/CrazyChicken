using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{

    [Header("Ints")]
    public int Score;
    private int scoreTemp;

    public TextMeshProUGUI text_score; 

    private void Start()
    {
        scoreTemp = Score;
        text_score.text = "Your Score: " + Score;
    }

    private void Update()
    {
        if(scoreTemp != Score)
        {
            text_score.text = "Your Score: " + Score;
            scoreTemp = Score;
        }
    }
    public void AddScore(int addScore)
    {
        Score += addScore;
        Debug.Log("Test");
    }

}
