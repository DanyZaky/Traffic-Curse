using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillManager : MonoBehaviour
{
    public static PlayerSkillManager Instance { get; set; }

    public GameObject cancelButton;
    public bool isAbilityCanceled;
    public float skill2Duration = 2f;
    
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);
    }

    public void OpenSkillCancelButton()
    {
        cancelButton.SetActive(true);
    }
    public void CloseSkillCancelButton()
    {
        cancelButton.SetActive(false);
        isAbilityCanceled = false;
    }
}
