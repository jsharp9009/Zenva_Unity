using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CarController : MonoBehaviour
{
    public float acceleration;
    public float reverseAcceleration;
    public float turnSpeed;
    public Transform carModel;
    public Transform leftWheel;
    public Transform rightWheel;
    public Vector3 startModelOffset;
    public float groundCheckRate;
    public Rigidbody rigidBody;
    public float wheelTurnMax;

    private float YRotation;
    public float wheelRotation;
    private float lastGroundCheckTime;
    private bool accelerate;
    private bool reverse;
    private float turn;
    public TrackZone curTrackZone;
    public int zonesPassed;
    public int racePosition;
    public int currentLap;
    public bool canMove;


    void Start()
    {
        var body = carModel.GetChild(0).GetChild(0);
        
        startModelOffset = carModel.transform.localPosition;
        GameManager.instance.carControllers.Add(this);
        transform.position = GameManager.instance.spawnPoints[GameManager.instance.carControllers.Count - 1].position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!canMove) return;

        float turnRate = Vector3.Dot(rigidBody.velocity.normalized, carModel.forward);
        turnRate = Math.Abs(turnRate);
        YRotation += turn * turnSpeed * turnRate * Time.deltaTime;

        // if(turn != 0){
        //     wheelRotation += turn * turnSpeed * turnRate * Time.deltaTime;
        //     if(Math.Abs(wheelRotation) > wheelTurnMax){
        //         wheelRotation = wheelTurnMax * turn;
        //     }
        // }
        // else{new
        //     wheelRotation -= turn * turnSpeed * turnRate * Time.deltaTime;
        // }
        
        var wheelRotation = new Vector3(0, wheelTurnMax * turn, 0);
        
        
        leftWheel.localEulerAngles = wheelRotation;
        rightWheel.localEulerAngles = wheelRotation;

        carModel.position = transform.position + startModelOffset;
        //carModel.eulerAngles = new Vector3(0, YRotation, 0);
        checkGround();

        //leftWheel.rotation = new Quaternion(0, 30 , 0, 0);
        //rightWheel.rotation = new Quaternion(0, 30, 0, 0);
    }

    void FixedUpdate(){
        if(!canMove) return;
        if(accelerate){
            rigidBody.AddForce(carModel.forward * acceleration, ForceMode.Acceleration);
        }
        else if (reverse){
            rigidBody.AddForce(carModel.forward * reverseAcceleration * -1, ForceMode.Acceleration);
        }
    }

    public void OnAccelerate(InputAction.CallbackContext context){
        if(context.phase == InputActionPhase.Performed)
            accelerate = true;
        else
            accelerate = false;
    }

    public void OnReverse(InputAction.CallbackContext context){
        if(context.phase == InputActionPhase.Performed)
            reverse = true;
        else
            reverse = false;
    }

    public void OnTurn(InputAction.CallbackContext context){
        turn = context.ReadValue<float>();
    }

    void checkGround(){
        Ray ray = new Ray(transform.position + new Vector3(0, -1f, 0), Vector3.down);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, 1f)){
            carModel.up = hit.normal;
        }
        else{
            carModel.up = Vector3.up;
        }

        carModel.Rotate(new Vector3(0, YRotation, 0), Space.Self);
    }
}
