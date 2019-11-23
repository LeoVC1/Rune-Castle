using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
    [Header("References:")]
    public SpawnerManager spawnerManager;
    public SpawnerController spawnerController;
    public EnemyData enemyData;
    public Renderer[] renderers;
    public GameObject deathParticle;
    public Animator anim;
    public Image healthBar;
    public TargetObject mainTarget;
    public Dissolve myDissolve;

    public TargetObject _target;
    public NavMeshAgent _agent;

    public List<TargetObject> nearbyTargets = new List<TargetObject>();

    [Header("Properties:")]
    public float myHealth;
    public float maxHealth;
    public float deathAnimationTime;
    public float animationTime;
    public bool arriveAtCrystal;

    public bool dead;

    public bool isAttacking;
    public bool isWalking;

    int targetPoint;

    public bool boss;

    public Vector3 cameraOffset;

    public GameObject[] runes;
    public int spawnRuneChance;

    public virtual void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        myHealth = spawnerController.GetEnemiesMaxHealth(enemyData.maxHealth);
        maxHealth = myHealth;
        spawnerManager.OnEnemySpawn();
    }

    public virtual void Update()
    {

        if (dead)
            return;

        //if (nearbyTargets.Count > 0)
        //{
        //    GetTarget();
        //    targetPoint = Random.Range(0, _target.targetPoint.Length);
        //}
        //else 
        if (_target == null)
        {
            _target = mainTarget;
            targetPoint = Random.Range(0, _target.targetPoint.Length);
        }


        float distance = Vector3.Distance(transform.position, _target.targetPoint[targetPoint].transform.position);

        if(distance < enemyData.range)
        {
            _agent.isStopped = true;

            if (!isAttacking)
            {
                isAttacking = true;
                Invoke("Attack", animationTime);
                if (anim)
                    anim.SetTrigger("Attack");
            }
        }
        else
        {
            isAttacking = false;
            _agent.isStopped = false;
            CancelInvoke("Attack");
        }

        if (_target)
            _agent.destination = _target.targetPoint[targetPoint].transform.position;

        isWalking = !_agent.isStopped;

        if(boss)
            anim.SetBool("Walking", isWalking);
    }

    private void LateUpdate()
    {
        if (_target)
        {
            transform.LookAt(new Vector3(_target.targetPoint[targetPoint].transform.position.x, transform.position.y, _target.targetPoint[targetPoint].transform.position.z));
        }
    }

    private void GetTarget()
    {
        int maxPriority = int.MinValue;
        for(int i = nearbyTargets.Count - 1; i >= 0; i--)
        {
            if(nearbyTargets[i].priority > maxPriority)
            {
                maxPriority = nearbyTargets[i].priority;
                _target = nearbyTargets[i];
            }
        }
    }

    public void Attack()
    {
        if(_target.ReceiveDamage(enemyData.damage))
        {
            nearbyTargets.Remove(_target);
            GetTarget();
        }
        else
        {
            isAttacking = false;
        }
    }

    public virtual bool ReceiveDamage(int damage)
    {
        myHealth -= damage;
        healthBar.fillAmount = myHealth / enemyData.maxHealth;
        if (myHealth <= 0 && !dead)
        {
            OnDeath();
            dead = true;
            return true;
        }
        else
            return false;
    }

    public virtual void ReceiveHeal(float heal)
    {
        if(myHealth + heal <= enemyData.maxHealth)
            myHealth += heal;
        healthBar.fillAmount = myHealth / enemyData.maxHealth;
    }

    public virtual void OnDeath()
    {
        anim.enabled = false;
        GetComponent<CapsuleCollider>().enabled = false;
        StopAllCoroutines();
        Vector3 particlePoint = transform.position + new Vector3(0, Random.Range(-3f, 1.5f),0);
        GameObject particle = Instantiate(deathParticle, particlePoint, Quaternion.identity);
        Destroy(particle, 3.5f);
        //StartCoroutine(DeathScale(particlePoint));
        _agent.enabled = false;
        myDissolve.StartLerp();
        spawnerManager.OnEnemyDie();
        if(spawnRuneChance > 0)
        {
            int chance = Random.Range(0, 100);
            if(chance < spawnRuneChance)
            {
                Instantiate(runes[Random.Range(0, runes.Length)], transform.position + Vector3.up * 2, Quaternion.identity);
            }
        }
    }

    public virtual void Death()
    {
        _agent.enabled = false;
        Destroy(gameObject);
    }

    public IEnumerator DeathScale(Vector3 point)
    {
        float t = 0;
        while(t <= 1)
        {
            transform.localScale = Vector3.Slerp(transform.localScale, Vector3.zero, t);
            t += Time.deltaTime / deathAnimationTime;
            transform.position = Vector3.Slerp(transform.position, point, t);
            yield return new WaitForEndOfFrame();
        }
        Destroy(gameObject);
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        TargetObject target = other.GetComponent<TargetObject>();
        if (target)
        {
            if(!arriveAtCrystal && other.name == "Crystal")
            {
                arriveAtCrystal = true;
                spawnerManager.OnEnemiesGetCrystal();
            }
            if (!nearbyTargets.Contains(target))
                nearbyTargets.Add(target);
        }
    }
}