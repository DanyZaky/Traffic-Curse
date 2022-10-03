using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Skill2Effect : MonoBehaviour
{
    private AIPath aIPath;
    // Start is called before the first frame update
    void Start()
    {
        aIPath = GetComponent<AIPath>();
        StartCoroutine(StartsEffect());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator StartsEffect()
    {
        aIPath.enabled = false;
        Destroy(Instantiate(GameplaySceneController.Instance.peluitVfxPrefab, transform), PlayerSkillManager.Instance.skill2Duration);
        yield return new WaitForSeconds(PlayerSkillManager.Instance.skill2Duration);
        aIPath.enabled = true;
        Destroy(this);
    }
}
