using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public int Score;
    public TextMeshProUGUI scoreText;

    void Start(){
        UpdateScore();
    }

    public void IncreaseScore(int amount){
        Score += amount;
        UpdateScore();
    }

    void UpdateScore(){
        scoreText.text = "Score: " + Score;
    }
}
