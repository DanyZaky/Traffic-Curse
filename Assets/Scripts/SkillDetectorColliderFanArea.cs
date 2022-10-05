using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDetectorColliderFanArea : MonoBehaviour
{
    private SpriteRenderer _sr;
    public Skill2AnalogController skill2AnalogController;


    private void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Grab") && !PlayerSkillManager.Instance.isAbilityCanceled)
        {
            _sr.color = Color.green;
            skill2AnalogController.allTargetAllWithinArea.Add(collision);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Grab"))
        {
            _sr.color = Color.white;
            skill2AnalogController.allTargetAllWithinArea.Remove(collision);
        }
    }
}
