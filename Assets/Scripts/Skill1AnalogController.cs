using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class Skill1AnalogController : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IDragHandler
{
    private Vector3 lastJoystickDirection;
    private Rigidbody2D playerRb;
    
    public TrailRenderer playerTrailRenderer;
    public FixedJoystick joystick;
    public GameObject visualCircleRadius;
    public Transform visualArrowDirection;
    public Transform player;
    public Image cooldownImg;
    public TextMeshProUGUI stackCounter;
    public float currentCd;

    public Image handle;

    private void Awake()
    {
        
    }

    private void Start()
    {
        playerRb = player.GetComponent<Rigidbody2D>();
        stackCounter.gameObject.SetActive(false);
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
                visualArrowDirection.rotation = Quaternion.LookRotation(Vector3.forward, moveVector);
            }
        }

        if (currentCd > 0 && PlayerTechTreeSkillManager.Instance.skill1MaxStack == 0)
        {
            currentCd -= Time.deltaTime;
            cooldownImg.fillAmount = 1 - (currentCd / PlayerTechTreeSkillManager.Instance.skill1Cd);
        }
        else if (PlayerTechTreeSkillManager.Instance.skill1MaxStack != 0)
        {
            if (currentCd > 0)
            {
                if (PlayerTechTreeSkillManager.Instance.skill1CurrentStacks < PlayerTechTreeSkillManager.Instance.skill1MaxStack)
                {
                    currentCd -= Time.deltaTime;
                    cooldownImg.fillAmount = 1 - (currentCd / PlayerTechTreeSkillManager.Instance.skill1Cd);
                }                
            }
            else if (PlayerTechTreeSkillManager.Instance.skill1CurrentStacks < PlayerTechTreeSkillManager.Instance.skill1MaxStack)
            {
                currentCd = PlayerTechTreeSkillManager.Instance.skill1Cd;
                cooldownImg.fillAmount = 1;
                PlayerTechTreeSkillManager.Instance.skill1CurrentStacks++;
            }

            stackCounter.gameObject.SetActive(true);
            stackCounter.text = PlayerTechTreeSkillManager.Instance.skill1CurrentStacks.ToString();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        handle.color = new Color32(255, 255, 255, 255);
        if ((currentCd <= 0 && PlayerTechTreeSkillManager.Instance.skill1MaxStack == 0) || (PlayerTechTreeSkillManager.Instance.skill1CurrentStacks > 0 && PlayerTechTreeSkillManager.Instance.skill1MaxStack != 0))
        {
            visualCircleRadius.SetActive(true);
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

        if ((currentCd > 0 && PlayerTechTreeSkillManager.Instance.skill1MaxStack == 0) || (PlayerTechTreeSkillManager.Instance.skill1CurrentStacks < 1 && PlayerTechTreeSkillManager.Instance.skill1MaxStack != 0)) return;
        if (!PlayerTechTreeSkillManager.Instance.isAbilityCanceled)
        {
            InitiateSkill1Effects();
        }
        else
        {
            print("Skill Canceled");
        }

        visualCircleRadius.SetActive(false);
        PlayerTechTreeSkillManager.Instance.CloseSkillCancelButton();
    }

    private void InitiateSkill1Effects()
    {
        if (PlayerTechTreeSkillManager.Instance.skill1MaxStack == 0)
        {
            currentCd = PlayerTechTreeSkillManager.Instance.skill1Cd;
            cooldownImg.fillAmount = 1;
        }
        else if (PlayerTechTreeSkillManager.Instance.skill1CurrentStacks > 0)
        {
            if (PlayerTechTreeSkillManager.Instance.skill1CurrentStacks == PlayerTechTreeSkillManager.Instance.skill1MaxStack) currentCd = PlayerTechTreeSkillManager.Instance.skill1Cd;
            PlayerTechTreeSkillManager.Instance.skill1CurrentStacks--;
        }
        
        StartCoroutine(Dash());
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        lastJoystickDirection = joystick.Direction;
    }

    private IEnumerator Dash()
    {
        //playerRb.velocity += lastJoystickDirection * PlayerTechTreeSkillManager.Instance.skill1DashPower;
        playerTrailRenderer.emitting = true;
        playerRb.transform.position += lastJoystickDirection * PlayerTechTreeSkillManager.Instance.skill1DashPower;
        PlayerTechTreeSkillManager.Instance.isDashing = true;
        yield return new WaitForSeconds(PlayerTechTreeSkillManager.Instance.skill1DashDuration);
        PlayerTechTreeSkillManager.Instance.isDashing = false;
        playerTrailRenderer.emitting = false;
    }
}
