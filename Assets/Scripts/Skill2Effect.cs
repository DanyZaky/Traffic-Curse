using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Skill2Effect : MonoBehaviour
{
    private AIPath aIPath;
    public GameObject peluitVfx;
    public float currentDuration;
    // Start is called before the first frame update
    void Start()
    {
        aIPath = GetComponent<AIPath>();
        //StartCoroutine(StartsEffect());
        StartsEffect();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentDuration > 0)
        {
            currentDuration -= Time.deltaTime;
        }
        else
        {
            Destroy(this);
        }
    }

    private void OnDestroy()
    {
        aIPath.enabled = true;
        Destroy(peluitVfx);
    }

    private void StartsEffect()
    {
        currentDuration = PlayerTechTreeSkillManager.Instance.skill2Duration;
        aIPath.enabled = false;
        peluitVfx = Instantiate(GameplaySceneController.Instance.peluitVfxPrefab, transform);
        //Destroy(Instantiate(GameplaySceneController.Instance.peluitVfxPrefab, transform), PlayerTechTreeSkillManager.Instance.skill2Duration);
        //yield return new WaitForSeconds(PlayerTechTreeSkillManager.Instance.skill2Duration);
    }
}
