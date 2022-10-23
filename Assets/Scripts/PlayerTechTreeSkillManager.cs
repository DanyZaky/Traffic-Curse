using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTechTreeSkillManager : MonoBehaviour
{
    public enum SkillType { BASE, A, B};

    public static PlayerTechTreeSkillManager Instance { get; set; }

    public GameObject cancelButton;
    public GameObject[] skillButtons;
    public GameObject skillAlert;
    public Image skillFilledBar;
    public bool isAbilityCanceled, isDashing;
    public int skillPoint = 99;
    public float skill1Cd = 3, skill1DashPower = 2, skill1DashDuration = 0.2f, skill1MaxStack = 0, skill1CurrentStacks = 0;
    public float skill2Cd = 8, skill2Duration = 9f, skill2MaxTarget = 0;
    public SkillType skill2AreaType = SkillType.BASE;
    public float skill3Cd = 12, skill3Range = 4f, skill3MaxTarget = 2, skill3CloneDuration = 10f;
    public SkillType skill3AreaType = SkillType.BASE;

    [HideInInspector] public int gainedSkillpoint;
    
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);
    }

    private void Start()
    {
        foreach (var item in skillButtons)
        {
            item.SetActive(false);
        }
    }

    private void Update()
    {
        skillFilledBar.fillAmount = gainedSkillpoint / 6f;
        if (skillPoint != 0)
        {
            skillAlert.SetActive(true);
        }
        else
        {
            skillAlert.SetActive(false);
        }
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
