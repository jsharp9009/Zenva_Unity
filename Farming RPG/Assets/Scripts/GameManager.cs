using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int currentDay;
    public int money;
    public CropData selectedCrop;
    //singleton
    public static GameManager instance;

    public event UnityAction onNewDay;

    public int inventory;

    public TextMeshProUGUI statsText;

    void Start(){
        UpdateStatsText();
    }

    void OnEnable()
    {
        Crop.onPlantCrop += OnPlantCrop;
        Crop.onHarvestCrop += OnHarvestCrop;
    }

    void OnDisabled()
    {
        Crop.onPlantCrop -= OnPlantCrop;
        Crop.onHarvestCrop -= OnHarvestCrop;
    }

    void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);
        else
            instance = this;
    }

    public void SetNextDay()
    {
        currentDay++;
        onNewDay?.Invoke();
        UpdateStatsText();
    }

    public void OnPlantCrop(CropData crop)
    {
        inventory--;
        UpdateStatsText();
    }

    public void OnHarvestCrop(CropData crop)
    {
        money = crop.sellPrice;
        UpdateStatsText();
    }

    public void PurchaseCrop(CropData crop)
    {
        money -= crop.purchasePrice;
        inventory++;
        UpdateStatsText();
    }

    public bool CanPlant()
    {
        return inventory > 0;
    }

    public void OnBuyCropButton(CropData crop)
    {
        if(money > crop.purchasePrice){
            PurchaseCrop(crop);
        }
    }

    void UpdateStatsText()
    {
        statsText.text = $"Day: {currentDay}\nMoney: ${money}\nCrop Inventory: {inventory}";
    }
}
