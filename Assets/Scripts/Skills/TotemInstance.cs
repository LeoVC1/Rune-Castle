using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.LightningBolt;

public class TotemInstance : MonoBehaviour
{
    public SpawnerController spawnerController;

    Enemy myEnemy;
    LightningBoltScript lightningBolt;
    Animator anim;

    [Space]
    [Header("Properties:")]
    public int damage;
    public float distance;
    public float lifeTime;
    [Range(0f, 5)] public float cooldown;

    [Header("GameObject References:")]
    public GameObject blackhole;
    public GameObject _parent;
    public GameObject _base;

    [Header("Sounds:")]
    public AudioSource idleSound;
    public AudioSource attackSound;
    public AudioClip[] attackClip;

    private float lifeTimer = 0;
    private float cooldownTimer;
    private bool onTarget;
    private bool isDead;
    private bool awaked;

    private void Start()
    {
        anim = GetComponent<Animator>();
        lightningBolt = GetComponentInChildren<LightningBoltScript>();
        StartCoroutine(TryToAttack());
        StartCoroutine(LifeTimer());
        Invoke("Awaked", 1.2f);
    }

    private void Update()
    {
        if (!awaked)
            return;

        if (isDead)
            return;

        if(myEnemy == null)
        {
            (Transform target, float _distance) = spawnerController.GetClosestEnemy(transform);
            if (target && _distance < distance)
            {
                OnGetTarget(target);
            }
        }
    }

    IEnumerator TryToAttack()
    {
        while (true)
        {
            if (cooldownTimer < cooldown)
            {
                cooldownTimer += Time.deltaTime;
                yield return new WaitForSeconds(Time.deltaTime);
            }
            else
            {
                cooldownTimer = 0;
                Attack();
                yield return new WaitForSeconds(Time.deltaTime);
            }
            yield return null;
        }
    }
    IEnumerator LifeTimer()
    {
        while (lifeTimer < lifeTime)
        {
            lifeTimer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        DeathAnimation();
    }

    void Attack()
    {
        if (myEnemy)
        {
            //attackSound.clip = attackClip[Random.Range(0, attackClip.Length)];
            //attackSound.Play();
            attackSound.PlayOneShot(attackClip[Random.Range(0, attackClip.Length)]);

            lightningBolt.Trigger();
            if (myEnemy.ReceiveDamage(damage))
                myEnemy = null;
        }
    }

    void DeathAnimation()
    {
        isDead = true;
        anim.SetBool("isDead", true);
        blackhole.SetActive(true);
        Destroy(_parent, 1.6f);
        StopAllCoroutines();
    }

    void OnGetTarget(Transform _target)
    {
        lightningBolt.EndObject = _target.gameObject;
        myEnemy = _target.gameObject.GetComponent<Enemy>();
    }

    void Awaked()
    {
        awaked = true;
        idleSound.Play();
    }
}