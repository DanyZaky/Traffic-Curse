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
    public Transform visualRangeArea;
    public Transform player;
    public Image cooldownImg;
    public float cd, currentCd;

    private void Awake()
    {
        visualCircleRadius.SetActive(false);
    }

    private void Update()
    {
        if (visualCircleRadius.activeInHierarchy)
        {
            visualCircleRadius.transform.position = player.position;
            Vector3 moveVector = (Vector3.up * joystick.Horizontal + Vector3.left * joystick.Vertical);

            if (joystick.Horizontal != 0 || joystick.Vertical != 0)
            {
                visualRangeArea.rotation = Quaternion.LookRotation(Vector3.forward, moveVector);
            }
        }

        if (currentCd > 0)
        {
            currentCd -= Time.deltaTime;
            cooldownImg.fillAmount = 1 - (currentCd / cd);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (currentCd <= 0)
        {
            visualCircleRadius.SetActive(true);
            PlayerSkillManager.Instance.OpenSkillCancelButton();
        }
        else
        {
            GameplaySceneController.Instance.ShowPromptMessage("Skill is Cooldown!");
        }
        
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (currentCd > 0) return;
        if (!PlayerSkillManager.Instance.isAbilityCanceled)
        {
            InitiateSkill2Effects();
        }
        else
        {
            print("Skill Canceled");
        }
        
        visualCircleRadius.SetActive(false);
        PlayerSkillManager.Instance.CloseSkillCancelButton();
    }

    private void InitiateSkill2Effects()
    {
        currentCd = cd;
        cooldownImg.fillAmount = 1;

        foreach (var item in allTargetAllWithinArea)
        {
            item.gameObject.AddComponent<Skill2Effect>();
        }
    }
}
