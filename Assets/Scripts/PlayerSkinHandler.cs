using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkinHandler : MonoBehaviour
{
    public GameObject skill1A, skill1A2, skill1B, skill1B2;
    public GameObject skill2A, skill2B, skill3A, skill3B;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnableSkin(string name)
    {
        if (string.Equals(name, "S1A"))
        {
            skill1A.SetActive(true);
            skill1A2.SetActive(true);
        }
        else if (string.Equals(name, "S1B"))
        {
            skill1B.SetActive(true);
            skill1B2.SetActive(true);
        }
        else if (string.Equals(name, "S2A"))
        {
            skill2A.SetActive(true);
        }
        else if (string.Equals(name, "S2B"))
        {
            skill2B.SetActive(true);
        }
        else if (string.Equals(name, "S3A"))
        {
            skill3A.SetActive(true);
        }
        else if (string.Equals(name, "S3B"))
        {
            skill3B.SetActive(true);
        }
    }
}
