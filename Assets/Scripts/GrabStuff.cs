using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class GrabStuff : MonoBehaviour
{
    private List<Collider2D> collisions = new List<Collider2D>();
    public GameObject[] anakSD;
    public LayerMask childLayerMask;

    public bool isChildNearby, isGrabbing;
    public float cd;
    public float radius;


    private void Start()
    {

    }

    private void Update()
    {
        if(cd > 0)
        {
            cd -= Time.deltaTime;
        }

        if (anakSD[0] != null && isGrabbing)
        {
            anakSD[0].transform.position = gameObject.transform.position;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    public void PickorDropChild()
    {
        if (!isGrabbing)
        {
            collisions.Add(Physics2D.OverlapCircle(transform.position, radius, childLayerMask));

            if (collisions[0] != null) GrabChild(collisions[0].gameObject);

            collisions.Clear();
        }
        else if (anakSD[0] != null)
        {
            anakSD[0].GetComponent<Collider2D>().isTrigger = false;
            anakSD[0] = null;
            isGrabbing = false;
        }
        else
        {
            print("kosong");
        }
        

        //if (isGrab == true)
        //{
        //    anakSD[0].gameObject.GetComponent<Collider2D>().isTrigger = true;
        //    anakSD[0].transform.position = gameObject.transform.position;
        //}

        //if (isGrab && cd <= 0f)
        //{
        //    anakSD[0].gameObject.GetComponent<Collider2D>().isTrigger = false;
        //    isGrab = false;
        //    Debug.Log("droppppppppppppppppppp");
        //    anakSD[0] = null;
        //    isArrayFull = false;
        //}
    }

    private void GrabChild(GameObject child)
    {
        anakSD[0] = child;
        anakSD[0].GetComponent<Collider2D>().isTrigger = true;
        isGrabbing = true;
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        
        //if (col.gameObject.CompareTag("Grab") && !isArrayFull && !isGrab)
        //{
        //    Debug.Log(col.gameObject.name);
        //    anakSD[0] = col.gameObject;
        //    isActivate = false;
        //    isArrayFull = true;
        //    isGrab = true;
        //    Debug.Log("drragggggggg");

        //    cd = 0.3f;
        //}
    }
}
