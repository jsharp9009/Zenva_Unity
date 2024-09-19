using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Xml.Serialization;

public class PlayerUI : MonoBehaviour
{
    public TextMeshProUGUI carPositionText;
    public TextMeshProUGUI countDownText;
    public CarController car;
    public TextMeshProUGUI gameOverText;

    void Update(){
        carPositionText.text = car.racePosition + "/" + GameManager.instance.carControllers.Count;
    }

    public void StartCountDown(){
        StartCoroutine(CountDown());

        IEnumerator CountDown(){
            countDownText.gameObject.SetActive(true);
            countDownText.text = "3";
            yield return new WaitForSeconds(1);
            countDownText.text = "2";
            yield return new WaitForSeconds(1);
            countDownText.text = "GO";
            yield return new WaitForSeconds(1);
            countDownText.gameObject.SetActive(false);
        }
    }

    public void GameOver(bool winner){
            gameOverText.gameObject.SetActive(true);
            gameOverText.color = winner ? Color.green : Color.red;
            gameOverText.text = winner ? "Winner!" : "You Lost";
        }
    
}
