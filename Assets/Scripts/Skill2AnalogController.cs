using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Skill2AnalogController : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    public FixedJoystick joystick;
    public GameObject visualCircleRadius;
    public Transform visualRangeArea;
    public Transform player;

    public float range;

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

        
    }

    public void OnDrag(PointerEventData eventData)
    {
        print("oke");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        visualCircleRadius.SetActive(true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        visualCircleRadius.SetActive(false);
    }
}
