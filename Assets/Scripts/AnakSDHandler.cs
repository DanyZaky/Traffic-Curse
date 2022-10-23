using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnakSDHandler : MonoBehaviour
{
    private AIPath aipath;
    private GameObject sadVfx, fightVfx;
    [SerializeField] private GameObject sadVfxPrefab, fightVfxPrefab;
    private float sadCounterChance;

    public bool isFighting, isSad;

    private void Awake()
    {
        aipath = GetComponent<AIPath>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (sadCounterChance > 0)
        {
            sadCounterChance -= Time.deltaTime;
        }
        else
        {
            if (Random.Range(0, 10) <= 1)
            {
                isSad = true;
                GameManager.Instance.PlaySfx("Nangis");
            }
            else if(Random.Range(0, 10) >= 6)
            {
                GameManager.Instance.PlaySfxRandom("AnakKecil", 3);
            }
            sadCounterChance = 8;
        }

        if (isSad)
        {
            aipath.maxSpeed = 5.5f;
            if (sadVfx == null) sadVfx = Instantiate(sadVfxPrefab, transform);
        }
        else
        {
            aipath.maxSpeed = 2;
            Destroy(sadVfx);
        }
    }
}
