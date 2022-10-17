using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill3EffectOnClone : MonoBehaviour
{
    private SpriteRenderer _sr;

    public List<Collider2D> allTargetAllWithinArea;
    public float currentIntervalCd, intervalCd = 1.2f;
    public float currentColor;
    public int counter = 0;

    private void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Destroy(transform.parent.gameObject, PlayerTechTreeSkillManager.Instance.skill3CloneDuration);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentIntervalCd > 0)
        {
            currentIntervalCd -= Time.deltaTime;
        }

        if (currentColor > 0)
        {
            currentColor -= (Time.deltaTime * 120);
            _sr.color = new Color32((byte)currentColor, (byte)currentColor, (byte)currentColor, (byte)(currentColor));
        }

        if (counter >= PlayerTechTreeSkillManager.Instance.skill3MaxTarget)
        {
            Destroy(transform.parent.gameObject);
        }
    }

    private void InitiateSkill3Effects()
    {
        currentIntervalCd = intervalCd;
        currentColor = 255;
        //_sr.color = Color.white;
        
        List<GameObject> deathRow = new List<GameObject>();

        if (allTargetAllWithinArea.Count == 0) return;

        foreach (var item in allTargetAllWithinArea)
        {
            deathRow.Add(item.gameObject);
            counter++;
            break;
        }

        foreach (var item in deathRow)
        {
            Destroy(item);
        }
    }

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Grab") && !PlayerTechTreeSkillManager.Instance.isAbilityCanceled)
        {
            allTargetAllWithinArea.Add(collision);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (currentIntervalCd <= 0 && collision.CompareTag("Grab"))
        {
            InitiateSkill3Effects();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Grab"))
        {
            allTargetAllWithinArea.Remove(collision);
        }
    }
}
