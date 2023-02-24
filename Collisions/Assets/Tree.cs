using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    public MeshRenderer mr;
    public Color hitColor;

    private void OnCollisionEnter(Collision collision){
        mr.material.color = hitColor;
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
