using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class GrabDrop : MonoBehaviour
{
    public GameObject[] anakSD;

    public bool isGrab, isNotNull, isArrayFull;
    
    void Start()
    {
        isGrab = false;
        isNotNull = false;
        isArrayFull = false;
    }

    void Update()
    {
        
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(isGrab == false)
            {
                if (isNotNull == true)
                {
                    anakSD[0].gameObject.GetComponent<AIPath>().enabled = false;
                    isGrab = true;
                    isNotNull = false;
                }

                
            }
            else if(isGrab == true)
            {
                anakSD[0].gameObject.GetComponent<AIPath>().enabled = true;
                anakSD[0].gameObject.GetComponent<Collider2D>().isTrigger = false;
                isGrab = false;

                anakSD[0] = null;
                isArrayFull = false;
            }
        }

        if(isGrab == true)
        {
            anakSD[0].gameObject.GetComponent<Collider2D>().isTrigger = true;
            anakSD[0].transform.position = gameObject.transform.position;
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(isArrayFull == false)
        {
            if (col.gameObject.tag == "Grab" && anakSD[0] == null)
            {
                anakSD[0] = col.gameObject;
                isNotNull = true;
                isArrayFull = true;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Grab" && anakSD[0] != null)
        {
            anakSD[0] = null;
            isNotNull = false;

        }
    }
}
