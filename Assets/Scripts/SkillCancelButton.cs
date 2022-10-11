using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SkillCancelButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        PlayerTechTreeSkillManager.Instance.isAbilityCanceled = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        PlayerTechTreeSkillManager.Instance.isAbilityCanceled = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
