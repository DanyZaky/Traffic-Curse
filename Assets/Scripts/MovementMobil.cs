using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementMobil : MonoBehaviour
{
    public float speed;
    public float targetX, targetY;
    public Vector3[] pathMobil;
    public int pathIndex = 0;

    private bool isFeel, isAnnoyed;
    private float timeFeel = 1, timeFeelCounter;

    public GameObject feel, variasiKiriKanan, variasiAtasBawah;
    private GameCondition gc;
    private float cdKlakson;
    void Start()
    {
        feel.SetActive(false);
        gc = GameObject.Find("GameCondition").GetComponent<GameCondition>();

        isFeel = false;
        timeFeelCounter = timeFeel;
    }

    void Update()
    {
        Movement(speed);

        if (cdKlakson > 0)
        {
            cdKlakson -= Time.deltaTime;
        }

        if(isFeel == true)
        {
            
            timeFeelCounter -= 1f * Time.deltaTime;
            //gc.totalScore -= 4f * Time.deltaTime;
            gc.timeSpentMobilKeganggu += Time.deltaTime;
            feel.SetActive(true);

            if (timeFeelCounter < 0)
            {
                
                isFeel = false;
                timeFeelCounter = timeFeel;
                feel.SetActive(false);
            }

            if (!isAnnoyed)
            {
                gc.countMobileKeganggu++;
                isAnnoyed = true;
            }
        }
    }

    private void Movement(float speedms)
    {
        //transform.position = Vector3.MoveTowards(transform.position, new Vector3(targetX, targetY, 0f), speedms * Time.deltaTime);
        if (Vector3.Distance(transform.position, pathMobil[pathIndex]) > 0)
        {
            if (pathIndex == 1)
            {
                variasiAtasBawah.SetActive(true);
                variasiKiriKanan.SetActive(false);
            }
            else
            {
                variasiAtasBawah.SetActive(false);
                variasiKiriKanan.SetActive(true);
            }
            transform.position = Vector3.MoveTowards(transform.position, pathMobil[pathIndex], speedms * Time.deltaTime);
        }
        else
        {
            pathIndex++;

            if (pathIndex == pathMobil.Length) 
            {
                Destroy(gameObject);
                if (!isAnnoyed) gc.countMobileLolos++;
            }
        }
        
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.CompareTag("Grab"))
        {
            isFeel = true;

            if (cdKlakson <= 0)
            {
                cdKlakson = 3;
                GameManager.Instance.PlaySfxRandom("Klakson", 3);
            }
            
        }
    }    
}
