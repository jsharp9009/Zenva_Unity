using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonController : MonoBehaviour
{
    public int scoreToGive = 1;
    public int clicksToPop = 5;
    public float scaleIncrease = 0.1f;
    public ScoreManager scoreManager;

    void OnMouseDown(){
        clicksToPop -= 1;
        transform.localScale += Vector3.one * scaleIncrease;

        if (clicksToPop == 0){
            scoreManager.IncreaseScore(scoreToGive);
            Destroy(gameObject);
        }
    }
}
