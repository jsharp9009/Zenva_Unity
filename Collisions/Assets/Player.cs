using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveForce;
    public Rigidbody player;

    void FixedUpdate()
    {
        float xInput = Input.GetAxis("Horizontal");
        player.AddForce(Vector3.right * xInput * moveForce);
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
