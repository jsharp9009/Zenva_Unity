using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject spawnObject;
    public int spawnCount = 5;
    public float spawnOffset = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        if (spawnObject != null)
        {
            SpawnEnemies();
        }
        else
        {
            Debug.LogError("Cannot Spawn Enemies! Prefab Missing");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SpawnEnemies()
    {
        for(int i = 0; i < spawnCount; i++){
            var xPos = i * spawnOffset;
            var pos = new Vector3(xPos, 0, 0);
            Instantiate(spawnObject, pos, Quaternion.identity);
        }
    }
}
