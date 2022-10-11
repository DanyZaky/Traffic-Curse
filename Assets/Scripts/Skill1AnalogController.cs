using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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
    public float cd, currentCd;

    private void Awake()
    {
        
    }

    private void Start()
    {
        
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
            PlayerTechTreeSkillManager.Instance.OpenSkillCancelButton();
        }
        else
        {
            GameplaySceneController.Instance.ShowPromptMessage("Skill is Cooldown!");
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (currentCd > 0) return;
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
        currentCd = cd;
        cooldownImg.fillAmount = 1;
        playerRb = player.GetComponent<Rigidbody2D>();
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
