using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawning : MonoBehaviour
{
    public GameObject[] anakSD;

    public float spawnDelay, spawnDelayCounter;

    void Start()
    {
        spawnDelayCounter = spawnDelay;
    }

    void Update()
    {
        spawnDelayCounter -= 1f * Time.deltaTime;

        if(spawnDelayCounter < 0)
        {
            Instantiate(anakSD[Random.Range(0, anakSD.Length)], new Vector3(-0.77f, -10.48f, 0), Quaternion.identity);
            spawnDelayCounter = spawnDelay;
        }
    }
}
