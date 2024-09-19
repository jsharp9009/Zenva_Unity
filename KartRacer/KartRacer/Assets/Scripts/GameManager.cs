using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public List<CarController> carControllers = new List<CarController>();
    public float postitionUpdateRate = 0.05f;
    public float lastPositionUpdateTime;
    public Transform[] spawnPoints;
    public int playerToBegin = 2;
    public bool gameStart = false;
    public int lapsToWin = 3;

    void Awake(){
        instance = this;
    }

    void UpdateCarRacePositions(){
        carControllers.Sort(sortPosition);
        for(int i = 0; i < carControllers.Count; i++){
            carControllers[i].racePosition = carControllers.Count - i; 
        }
    }

    int sortPosition(CarController a, CarController b){
        if(a.zonesPassed > b.zonesPassed) return 1;
        else if (b.zonesPassed > a.zonesPassed) return -1;

        float aDist = Vector3.Distance(a.transform.position, a.curTrackZone.transform.position);
        float bDist = Vector3.Distance(b.transform.position, b.curTrackZone.transform.position);

        return aDist > bDist ? 1 : -1;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - lastPositionUpdateTime > postitionUpdateRate){
            lastPositionUpdateTime = Time.time;
            UpdateCarRacePositions();
        }

        if(!gameStart && carControllers.Count >= playerToBegin){
            gameStart = true;
            StartCountDown();
        }
    }

    void StartCountDown(){
        PlayerUI[] uis = FindObjectsOfType<PlayerUI>();
        foreach(var ui in uis) ui.StartCountDown();
        Invoke("BeginGame", 3.0f);
    }

    void BeginGame(){
        carControllers.ForEach(c => c.canMove = true);
    }

    public void CheckIsWinner(CarController car){
        if(car.currentLap == lapsToWin + 1){
            carControllers.ForEach(c => c.canMove = false);

            PlayerUI[] uis = FindObjectsOfType<PlayerUI>();
            foreach(var ui in uis) ui.GameOver(ui.car == car);
        }
    }
}
