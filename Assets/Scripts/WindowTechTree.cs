using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class WindowTechTree : MonoBehaviour
{
    public GameObject[] skill1ButtonPairs, skill2ButtonPairs, skill3ButtonPairs;
    public GameObject[] containerSkill1ButtonPairs, containerSkill2ButtonPairs, containerSkill3ButtonPairs;
    public TextMeshProUGUI skillPointText;

    [Header("Sources")]
    public Sprite[] buttonSprites;

    void Start()
    {
        for (int i = 0; i < skill1ButtonPairs.Length; i++)
        {
            skill1ButtonPairs[i].SetActive(true);
            skill2ButtonPairs[i].SetActive(true);
            skill3ButtonPairs[i].SetActive(true);
            containerSkill1ButtonPairs[i].GetComponent<Image>().sprite = buttonSprites[1];
            containerSkill2ButtonPairs[i].GetComponent<Image>().sprite = buttonSprites[1];
            containerSkill3ButtonPairs[i].GetComponent<Image>().sprite = buttonSprites[1];
        }
    }

    void Update()
    {
        skillPointText.text = "You have " + PlayerTechTreeSkillManager.Instance.skillPoint.ToString() + " skill points";
    }

    private bool DeactivateButtonPairs(GameObject[] pairs, GameObject containerSelected, GameObject containerDeselected)
    {
        if (PlayerTechTreeSkillManager.Instance.skillPoint > 0)
        {
            foreach (var item in pairs)
            {
                item.gameObject.SetActive(false);
            }

            containerSelected.GetComponent<Image>().sprite = buttonSprites[0];
            containerDeselected.GetComponent<Image>().sprite = buttonSprites[2];

            PlayerTechTreeSkillManager.Instance.skillPoint--;

            return false;
        }

        GameplaySceneController.Instance.ShowPromptMessage("Not Enough Skill Point!");
        return true;
    }

    public void UpgradeSkill1A()
    {
        if (DeactivateButtonPairs(skill1ButtonPairs, containerSkill1ButtonPairs[0], containerSkill1ButtonPairs[1])) return;

        PlayerTechTreeSkillManager.Instance.skill1DashPower = 7;
        PlayerTechTreeSkillManager.Instance.skill1Cd = 5.5f;
    }

    public void UpgradeSkill1B()
    {
        if (DeactivateButtonPairs(skill1ButtonPairs, containerSkill1ButtonPairs[1], containerSkill1ButtonPairs[0])) return;

        PlayerTechTreeSkillManager.Instance.EnableSkill1Stack();
    }

    public void UpgradeSkill2A()
    {
        if (DeactivateButtonPairs(skill2ButtonPairs, containerSkill2ButtonPairs[0], containerSkill2ButtonPairs[1])) return;

        PlayerTechTreeSkillManager.Instance.skill2MaxTarget = 1;
        PlayerTechTreeSkillManager.Instance.skill2Cd = 1.5f;
        PlayerTechTreeSkillManager.Instance.skill2Duration = 7;
        PlayerTechTreeSkillManager.Instance.skill2AreaType = PlayerTechTreeSkillManager.SkillType.A;
    }

    public void UpgradeSkill2B()
    {
        if (DeactivateButtonPairs(skill2ButtonPairs, containerSkill2ButtonPairs[1], containerSkill2ButtonPairs[0])) return;

        PlayerTechTreeSkillManager.Instance.skill2Cd = 12f;
        PlayerTechTreeSkillManager.Instance.skill2Duration = 20;
        PlayerTechTreeSkillManager.Instance.skill2AreaType = PlayerTechTreeSkillManager.SkillType.B;
    }

    public void UpgradeSkill3A()
    {
        if (DeactivateButtonPairs(skill3ButtonPairs, containerSkill3ButtonPairs[0], containerSkill3ButtonPairs[1])) return;

        PlayerTechTreeSkillManager.Instance.skill3MaxTarget = 3;
        PlayerTechTreeSkillManager.Instance.skill3AreaType = PlayerTechTreeSkillManager.SkillType.A;

    }

    public void UpgradeSkill3B()
    {
        if (DeactivateButtonPairs(skill3ButtonPairs, containerSkill3ButtonPairs[1], containerSkill3ButtonPairs[0])) return;

        PlayerTechTreeSkillManager.Instance.skill3MaxTarget = 5;
        PlayerTechTreeSkillManager.Instance.skill3AreaType = PlayerTechTreeSkillManager.SkillType.B;
    }
}
