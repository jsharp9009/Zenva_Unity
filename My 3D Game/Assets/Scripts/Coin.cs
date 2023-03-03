using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public float rotateSpeed = 180;
    public int value;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other){
        if(other.CompareTag("Player")){
            other.GetComponent<PlayerController>().AddScore(value);
            Destroy(gameObject);
        }
    }
}
