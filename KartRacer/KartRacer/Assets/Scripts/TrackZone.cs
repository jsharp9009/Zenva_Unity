using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackZone : MonoBehaviour
{
    public bool isGate;

    void OnTriggerEnter(Collider other){
        if(other.CompareTag("Player")){
            CarController car = other.GetComponent<CarController>();
            car.curTrackZone = this;
            car.zonesPassed++;

            if(isGate){
                car.currentLap++;
                GameManager.instance.CheckIsWinner(car);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
