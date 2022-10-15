using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class WindowTechTree : MonoBehaviour
{
    public Button[] skill1ButtonPairs, skill2ButtonPairs, skill3ButtonPairs;
    public TextMeshProUGUI skillPointText;


    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        skillPointText.text = PlayerTechTreeSkillManager.Instance.skillPoint.ToString();
    }

    private bool DeactivateButtonPairs(Button[] pairs)
    {
        if (PlayerTechTreeSkillManager.Instance.skillPoint > 0)
        {
            foreach (var item in pairs)
            {
                item.interactable = false;
            }

            PlayerTechTreeSkillManager.Instance.skillPoint--;

            return false;
        }

        GameplaySceneController.Instance.ShowPromptMessage("Not Enough Skill Point!");
        return true;
    }

    public void UpgradeSkill1A()
    {
        if (DeactivateButtonPairs(skill1ButtonPairs)) return;

        PlayerTechTreeSkillManager.Instance.skill1DashPower = 7;
        PlayerTechTreeSkillManager.Instance.skill1Cd = 4;
    }

    public void UpgradeSkill1B()
    {
        if (DeactivateButtonPairs(skill1ButtonPairs)) return;

        PlayerTechTreeSkillManager.Instance.EnableSkill1Stack();
    }
}
