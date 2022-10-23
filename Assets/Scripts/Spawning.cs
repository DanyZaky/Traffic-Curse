using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawning : MonoBehaviour
{
    public GameObject[] anakSD;
    public GameObject mobilKanan, mobilKiri, motorKanan, motorKiri;
    public Transform[] spawnLocation;
    public Transform spawnMobilKanan, spawnMobilKiri;

    public float spawnDelayMobil1, spawnDelayMobil2;
    public float minSpawnDelayAnak, maxSpawnDelayAnak;
    public float _spawnDelayAnakCounter, _spawnDelayMobilKananCounter, _spawnDelayMobil2Counter;

    void Start()
    {
        _spawnDelayAnakCounter = 0;
        _spawnDelayMobilKananCounter = spawnDelayMobil1;
        _spawnDelayMobil2Counter = spawnDelayMobil2/2;
    }

    void Update()
    {
        _spawnDelayAnakCounter -= 1f * Time.deltaTime;

        if(_spawnDelayAnakCounter < 0)
        {
            Instantiate(anakSD[Random.Range(0, anakSD.Length)], spawnLocation[Random.Range(0, spawnLocation.Length)].position, Quaternion.identity);
            //spawnDelayAnakCounter = spawnDelayAnak;
            //(Max + Min) - ((Max - Min) + (Delta * (Max - Min))
            // 16 - (5) = 11->Delta = 1
            // 16 - (11) = 5->Delta = 0
            // 16 - (6 + (1 * 6))
            // Delta = 1, 
            // 11 = 16 - 5
            // = 16 - (11 - 6)
            // = 16 - (11 - (11 - 5))
            // Delta = 0,
            // 5 = 16 - 11
            // = 16 - (11 - 0)
            // = 16 - (11 - (D * (11 - 5))
            // = (SumMaxMin - (Max - (DeltaTimeLeft * (Max - Min))
            
            _spawnDelayAnakCounter = 
                ((maxSpawnDelayAnak + minSpawnDelayAnak) - (maxSpawnDelayAnak - ((GameCondition.Instance.timeLeftCounter/GameCondition.Instance.timeLeft) * (maxSpawnDelayAnak - minSpawnDelayAnak))));
        }

        _spawnDelayMobilKananCounter -= 1f * Time.deltaTime;

        if(_spawnDelayMobilKananCounter < 0)
        {
            if (Random.Range(0, 2) == 1)
            {
                Instantiate(mobilKanan, spawnMobilKanan.position, Quaternion.identity);
            }
            else
            {
                Instantiate(motorKanan, spawnMobilKanan.position, Quaternion.identity);
            }
            
            _spawnDelayMobilKananCounter = Random.Range(spawnDelayMobil1 * 0.8f, spawnDelayMobil1 * 1.2f);
        }

        _spawnDelayMobil2Counter -= 1f * Time.deltaTime;

        if (_spawnDelayMobil2Counter < 0)
        {
            if (Random.Range(0, 2) == 1)
            {
                Instantiate(mobilKiri, spawnMobilKiri.position, Quaternion.identity);
            }
            else
            {
                Instantiate(motorKiri, spawnMobilKiri.position, Quaternion.identity);
            }
            _spawnDelayMobil2Counter = Random.Range(spawnDelayMobil2 * 0.8f, spawnDelayMobil2 * 1.2f);
        }
    }
}
