using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    public Image fill;

    public static HealthBarUI instance;

    private void Awake(){
        instance = this;
    }

    public void UpdateFill(int currentHP, int maxHP){
        fill.fillAmount = (float)currentHP / (float)maxHP;
    }
}
