using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawning : MonoBehaviour
{
    public GameObject[] anakSD;
    public GameObject Mobil, Mobil2;
    public Transform[] spawnLocation;

    public float spawnDelayAnak, spawnDelayMobil1, spawnDelayMobil2;
    private float spawnDelayAnakCounter, spawnDelayMobil1Counter, spawnDelayMobil2Counter;

    void Start()
    {
        spawnDelayAnakCounter = 0;
        spawnDelayMobil1Counter = spawnDelayMobil1;
        spawnDelayMobil2Counter = spawnDelayMobil2/2;
    }

    void Update()
    {
        spawnDelayAnakCounter -= 1f * Time.deltaTime;

        if(spawnDelayAnakCounter < 0)
        {
            Instantiate(anakSD[Random.Range(0, anakSD.Length)], spawnLocation[Random.Range(0, spawnLocation.Length)].position, Quaternion.identity);
            spawnDelayAnakCounter = spawnDelayAnak;
        }

        spawnDelayMobil1Counter -= 1f * Time.deltaTime;

        if(spawnDelayMobil1Counter < 0)
        {
            Instantiate(Mobil, new Vector3(-16.76f, 1.046598f, 0), Quaternion.identity);
            spawnDelayMobil1Counter = spawnDelayMobil1;
        }

        spawnDelayMobil2Counter -= 1f * Time.deltaTime;

        if (spawnDelayMobil2Counter < 0)
        {
            Instantiate(Mobil2, new Vector3(16.76f, -1.16883f, 0), Quaternion.identity);
            spawnDelayMobil2Counter = spawnDelayMobil2;
        }
    }
}
