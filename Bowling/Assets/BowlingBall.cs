using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlingBall : MonoBehaviour
{
    public float force;
    public float leftBound;
    public float rightBound;
    public float moveIncrement;
    public Rigidbody rigidbody;

    public void Bowl(){
        rigidbody.AddForce(transform.forward * force, ForceMode.Impulse);
    }

    public void MoveLeft(){
        var newPos = transform.position.x + -moveIncrement;
        if(newPos > leftBound)
            transform.position += new Vector3(-moveIncrement, 0, 0);
    }

    public void MoveRight(){
        var newPos = transform.position.x + moveIncrement;
        if(newPos < rightBound)
            transform.position += new Vector3(moveIncrement, 0, 0);
    }
}
