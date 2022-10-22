using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Skill2AnalogController : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    public FixedJoystick joystick;
    public List<Collider2D> allTargetAllWithinArea;
    public GameObject visualCircleRadius;
    public Transform[] visualRangeArea;
    public Transform player;
    public Image cooldownImg;
    public float currentCd;

    public Image handle;
    private void Awake()
    {
        visualCircleRadius.SetActive(false);
        handle.color = new Color32(255, 255, 255, 0);
    }

    private void Update()
    {
        if (visualCircleRadius.activeInHierarchy)
        {
            visualCircleRadius.transform.position = player.position;
            Vector3 moveVector = (Vector3.up * joystick.Horizontal + Vector3.left * joystick.Vertical);

            if (joystick.Horizontal != 0 || joystick.Vertical != 0)
            {
                visualRangeArea[(int)PlayerTechTreeSkillManager.Instance.skill2AreaType].rotation = Quaternion.LookRotation(Vector3.forward, moveVector);
            }
        }

        if (currentCd > 0)
        {
            currentCd -= Time.deltaTime;
            cooldownImg.fillAmount = 1 - (currentCd / PlayerTechTreeSkillManager.Instance.skill2Cd);
        }
    }

    private void ShowVisualSkillEffectRange()
    {
        visualCircleRadius.SetActive(true);

        foreach (var item in visualRangeArea)
        {
            item.gameObject.SetActive(false);
        }

        visualRangeArea[(int)PlayerTechTreeSkillManager.Instance.skill2AreaType].gameObject.SetActive(true);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        handle.color = new Color32(255, 255, 255, 255);

        if (currentCd <= 0)
        {
            ShowVisualSkillEffectRange();
            PlayerTechTreeSkillManager.Instance.OpenSkillCancelButton();
        }
        else
        {
            GameplaySceneController.Instance.ShowPromptMessage("Skill is Cooldown!");
        }
        
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        handle.color = new Color32(255, 255, 255, 0);

        if (currentCd > 0) return;
        if (!PlayerTechTreeSkillManager.Instance.isAbilityCanceled)
        {
            InitiateSkill2Effects();
        }
        else
        {
            print("Skill Canceled");
        }
        
        visualCircleRadius.SetActive(false);
        PlayerTechTreeSkillManager.Instance.CloseSkillCancelButton();
    }

    private void InitiateSkill2Effects()
    {
        currentCd = PlayerTechTreeSkillManager.Instance.skill2Cd;
        cooldownImg.fillAmount = 1;

        if (PlayerTechTreeSkillManager.Instance.skill2AreaType == PlayerTechTreeSkillManager.SkillType.A) 
        {
            int counter = 0;
            foreach (var item in allTargetAllWithinArea)
            {
                if (item.gameObject.TryGetComponent<Skill2Effect>(out _)) continue;
                else
                {
                    item.gameObject.AddComponent<Skill2Effect>();
                    counter++;
                }

                if (counter == PlayerTechTreeSkillManager.Instance.skill2MaxTarget) break;
            }
        }
        else
        {
            foreach (var item in allTargetAllWithinArea)
            {
                item.gameObject.AddComponent<Skill2Effect>();
            }
        }        
    }
}
