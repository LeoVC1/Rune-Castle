using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    private bool isCastingSpell;
    public float minTimeBetweenSkill;
    public float maxTimeBetweenSkill;

    public string skillAnimationParamenter;
    public float skillAnimationTime;

    public GameObject groundHitVFX;

    public override void Start()
    {
        base.Start();
        StartCoroutine(Skill());
    }

    public override void Update()
    {
        if (dead)
            return;

        if (isCastingSpell)
            return;
        base.Update();
    }

    IEnumerator Skill()
    {
        float timer = 0;
        while (true)
        {
            float nextSkillTime = Random.Range(minTimeBetweenSkill, maxTimeBetweenSkill);
            timer = 0;
            isCastingSpell = false;
            while (timer < nextSkillTime)
            {
                timer += Time.deltaTime;
                yield return null;
            }
            isCastingSpell = true;
            _agent.isStopped = true;
            anim.SetTrigger(skillAnimationParamenter);
            Invoke("EmitParticle", 1.15f);
            yield return new WaitForSeconds(skillAnimationTime);
        }
    }

    void EmitParticle()
    {
        groundHitVFX.SetActive(true);
        Invoke("DisableParticle", 3f);
    }

    void DisableParticle()
    {
        groundHitVFX.SetActive(false);
    }
}
