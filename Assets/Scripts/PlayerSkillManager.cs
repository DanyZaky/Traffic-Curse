using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillManager : MonoBehaviour
{
    public static PlayerSkillManager Instance { get; set; }

    public GameObject cancelButton;
    public bool isAbilityCanceled, isDashing;
    public float skill1DashPower = 2, skill1DashDuration = 0.2f;
    public float skill2Duration = 2f;
    public float skill3Range = 2f, skill3maxTarget = 3;
    
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
