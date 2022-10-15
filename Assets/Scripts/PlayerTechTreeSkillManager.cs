using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTechTreeSkillManager : MonoBehaviour
{
    public static PlayerTechTreeSkillManager Instance { get; set; }

    public GameObject cancelButton;
    public bool isAbilityCanceled, isDashing;
    public int skillPoint = 99;
    public float skill1Cd = 3, skill1DashPower = 2, skill1DashDuration = 0.2f, skill1MaxStack = 0, skill1CurrentStacks = 0;
    public float skill2Duration = 2f;
    public float skill3Range = 2f, skill3MaxTarget = 3;
    
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

    public void EnableSkill1Stack()
    {
        skill1MaxStack = 2;
    }
}
