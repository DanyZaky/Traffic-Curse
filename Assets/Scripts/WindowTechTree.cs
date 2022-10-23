using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class WindowTechTree : MonoBehaviour
{
    private bool isShowingSkillPoints = true;
    private string lastDesc;

    public GameObject[] skillBase, skill1ButtonPairs, skill2ButtonPairs, skill3ButtonPairs;
    public GameObject[] containerSkillBase, containerSkill1ButtonPairs, containerSkill2ButtonPairs, containerSkill3ButtonPairs;
    public TextMeshProUGUI skillPointText;
    public bool[] isSkillLearned;

    [Header("Sources")]
    public Sprite[] buttonSprites;

    void Start()
    {
        for (int i = 0; i < skill1ButtonPairs.Length; i++)
        {
            skill1ButtonPairs[i].SetActive(false);
            skill2ButtonPairs[i].SetActive(false);
            skill3ButtonPairs[i].SetActive(false);
            containerSkill1ButtonPairs[i].GetComponent<Image>().sprite = buttonSprites[2];
            containerSkill2ButtonPairs[i].GetComponent<Image>().sprite = buttonSprites[2];
            containerSkill3ButtonPairs[i].GetComponent<Image>().sprite = buttonSprites[2];
        }

        for (int i = 0; i < skillBase.Length - 1; i++)
        {
            skillBase[i].SetActive(true);
            containerSkillBase[i].GetComponent<Image>().sprite = buttonSprites[1];
        }

        skillBase[2].SetActive(false);
        containerSkillBase[2].GetComponent<Image>().sprite = buttonSprites[2];
    }

    void Update()
    {
        if (isShowingSkillPoints)
        {
            skillPointText.text = "You have " + PlayerTechTreeSkillManager.Instance.skillPoint.ToString() + " skill points";
        }

        if (isSkillLearned[0] && isSkillLearned[1] && !isSkillLearned[2])
        {
            skillBase[2].SetActive(true);
            containerSkillBase[2].GetComponent<Image>().sprite = buttonSprites[1];
        }
    }

    private bool DeactivateButtonPairs(GameObject[] pairs, GameObject containerSelected, GameObject containerDeselected)
    {
        if (CheckSkillPoints()) return true;

        foreach (var item in pairs)
        {
            item.SetActive(false);
        }

        containerSelected.GetComponent<Image>().sprite = buttonSprites[0];
        containerDeselected.GetComponent<Image>().sprite = buttonSprites[2];

        return false;
    }

    private bool ActivateButtonPairs(GameObject[] evolutionPairs, GameObject[] evolutionContainerPairs, GameObject containerSelected, GameObject deselectedSkillBase)
    {
        if (CheckSkillPoints()) return true;

        for (int i = 0; i < evolutionPairs.Length; i++)
        {
            evolutionPairs[i].SetActive(true);
            evolutionContainerPairs[i].GetComponent<Image>().sprite = buttonSprites[1];
        }

        containerSelected.GetComponent<Image>().sprite = buttonSprites[0];
        deselectedSkillBase.SetActive(false);

        return false;
    }

    private bool CheckSkillPoints()
    {
        if (PlayerTechTreeSkillManager.Instance.skillPoint > 0)
        {
            PlayerTechTreeSkillManager.Instance.skillPoint--;
            return false;
        }
        else
        {
            GameplaySceneController.Instance.ShowPromptMessage("Not Enough Skill Point!");
            return true;
        }        
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
        PlayerTechTreeSkillManager.Instance.skill2Cd = 0.8f;
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

    public void UnlockSkill1()
    {
        if (ActivateButtonPairs(skill1ButtonPairs, containerSkill1ButtonPairs, containerSkillBase[0], skillBase[0])) return;

        PlayerTechTreeSkillManager.Instance.skillButtons[0].SetActive(true);
        isSkillLearned[0] = true;
    }

    public void UnlockSkill2()
    {
        if (ActivateButtonPairs(skill2ButtonPairs, containerSkill2ButtonPairs, containerSkillBase[1], skillBase[1])) return;

        PlayerTechTreeSkillManager.Instance.skillButtons[1].SetActive(true);
        isSkillLearned[1] = true;
    }

    public void UnlockSkill3()
    {
        if (ActivateButtonPairs(skill3ButtonPairs, containerSkill3ButtonPairs, containerSkillBase[2], skillBase[2])) return;

        PlayerTechTreeSkillManager.Instance.skillButtons[2].SetActive(true);
        isSkillLearned[2] = true;
    }

    public void SkillDesc(string desc)
    {
        if (isShowingSkillPoints)
        {
            isShowingSkillPoints = false;
            skillPointText.text = desc;
            lastDesc = desc;
        }
        else if(lastDesc != desc)
        {
            skillPointText.text = desc;
            lastDesc = desc;
        }
        else
        {
            isShowingSkillPoints = true;
        }
    }
}
