using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawning : MonoBehaviour
{
    public GameObject[] anakSD;
    public GameObject mobilKanan, mobilKiri;
    public Transform[] spawnLocation;
    public Transform spawnMobilKanan, spawnMobilKiri;

    public float spawnDelayAnak, spawnDelayMobil1, spawnDelayMobil2;
    private float spawnDelayAnakCounter, spawnDelayMobilKananCounter, spawnDelayMobil2Counter;

    void Start()
    {
        spawnDelayAnakCounter = 0;
        spawnDelayMobilKananCounter = spawnDelayMobil1;
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

        spawnDelayMobilKananCounter -= 1f * Time.deltaTime;

        if(spawnDelayMobilKananCounter < 0)
        {
            Instantiate(mobilKanan, spawnMobilKanan.position, Quaternion.identity);
            spawnDelayMobilKananCounter = Random.Range(spawnDelayMobil1 * 0.8f, spawnDelayMobil1 * 1.2f);
        }

        spawnDelayMobil2Counter -= 1f * Time.deltaTime;

        if (spawnDelayMobil2Counter < 0)
        {
            Instantiate(mobilKiri, spawnMobilKiri.position, Quaternion.identity);
            spawnDelayMobil2Counter = Random.Range(spawnDelayMobil2 * 0.8f, spawnDelayMobil2 * 1.2f);
        }
    }
}
