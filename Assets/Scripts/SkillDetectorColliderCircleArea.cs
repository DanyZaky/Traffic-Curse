using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDetectorColliderCircleArea : MonoBehaviour
{
    private SpriteRenderer _sr;
    public Skill3AnalogController skill3AnalogController;

    private void Start()
    {
        
    }

    private void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Grab") && !PlayerSkillManager.Instance.isAbilityCanceled)
        {
            _sr.color = Color.green;
            skill3AnalogController.allTargetAllWithinArea.Add(collision);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Grab"))
        {
            _sr.color = Color.white;
            skill3AnalogController.allTargetAllWithinArea.Remove(collision);
        }
    }
}
