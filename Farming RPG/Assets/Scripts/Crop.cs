using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Crop : MonoBehaviour
{  
    private CropData currentCrop;
    private int plantDay;
    private int DaysSinceLastWatered;
    public SpriteRenderer sr;
    public static event UnityAction<CropData> onPlantCrop;
    public static event UnityAction<CropData> onHarvestCrop;

    public void Plant(CropData crop){
        currentCrop = crop;
        plantDay = GameManager.instance.currentDay;
        DaysSinceLastWatered = 1;
        UpdateCropSprite();
        onPlantCrop?.Invoke(crop);
    }

    public void NewDayCheck(){
        DaysSinceLastWatered++;
        if(DaysSinceLastWatered > 3){
            Destroy(this);
            return;
        }

        UpdateCropSprite();
    }

    public void UpdateCropSprite(){
        int cropProgress = CropProgress();
        if(cropProgress <= currentCrop.daysToGrow){
            sr.sprite = currentCrop.growProgressSprites[cropProgress];
        }
        else{
            sr.sprite = currentCrop.readyToHarvestSprite;
        }
    }

    public void Water(){
        DaysSinceLastWatered = 0;
    }
    
    public void Harvest(){
        if(canHarvest()){
            onHarvestCrop?.Invoke(currentCrop);
            Destroy(gameObject);
        }
    }

    int CropProgress(){
        return GameManager.instance.currentDay - plantDay;
    }

    public bool canHarvest(){
        return CropProgress() > currentCrop.daysToGrow;
    }
}
