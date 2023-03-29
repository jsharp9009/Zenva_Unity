using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldTile : MonoBehaviour
{
    public GameObject cropPrefab;
    public SpriteRenderer sr;
    private bool tilled;

    [Header("Sprites")]
    public Sprite grassSprite;
    public Sprite tilledSprite;
    public Sprite watertedTilledSprite;

    private Crop currentCrop;

    void Start()
    {
        sr.sprite = grassSprite;
    }

    public void Interact()
    {
        if (!tilled)
        {
            tilled = true;
            Till();
        }
        else if (!HasCrop() && GameManager.instance.CanPlant())
        {
            PlantNewCrop(GameManager.instance.selectedCrop);
        }
        else if (HasCrop() && currentCrop.canHarvest())
        {
            currentCrop.Harvest();
        }
        else
        {
            Water();
        }
    }

    void PlantNewCrop(CropData crop)
    {
        if (!tilled) return;
        currentCrop = Instantiate(cropPrefab, transform).GetComponent<Crop>();
        currentCrop.Plant(crop);
        GameManager.instance.onNewDay += OnNewDay;
    }

    void Till()
    {
        tilled = true;
        sr.sprite = tilledSprite;
        Debug.LogWarning("Tilled");
    }

    void Water()
    {
        sr.sprite = watertedTilledSprite;

        if (HasCrop()) currentCrop.Water();

        Debug.Log("Watered");
    }

    void OnNewDay()
    {
        if (currentCrop == null)
        {
            tilled = false;
            sr.sprite = grassSprite;
            GameManager.instance.onNewDay -= OnNewDay;
        }
        else {
            sr.sprite = tilledSprite;
            currentCrop.NewDayCheck();
        }

    }

    bool HasCrop()
    {
        return currentCrop != null;
    }
}
