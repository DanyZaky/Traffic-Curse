using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class GrabStuff : MonoBehaviour
{
    public GameObject[] anakSD;

    public bool isGrab, isNotNull, isArrayFull;

    public float cd;


    private void Start()
    {
        isArrayFull = false;
        isGrab = false;

        
    }

    private void Update()
    {
        if (isGrab == true)
        {
            anakSD[0].gameObject.GetComponent<Collider2D>().isTrigger = true;
            anakSD[0].transform.position = gameObject.transform.position;
        }

        if(isGrab && Input.GetKeyDown(KeyCode.Space) && cd <= 0f)
        {
            anakSD[0].gameObject.GetComponent<Collider2D>().isTrigger = false;
            isGrab = false;
            Debug.Log("droppppppppppppppppppp");
            anakSD[0] = null;
            isArrayFull = false;
        }

        if(cd > 0)
        {
            cd -= Time.deltaTime;
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (Input.GetKeyDown(KeyCode.Space) && col.gameObject.tag == "Grab" && !isArrayFull && !isGrab)
        {
            Debug.Log(col.gameObject.name);
            anakSD[0] = col.gameObject;
            isArrayFull = true;
            isGrab = true;
            Debug.Log("drragggggggg");

            cd = 0.3f;
        }
    }
}
