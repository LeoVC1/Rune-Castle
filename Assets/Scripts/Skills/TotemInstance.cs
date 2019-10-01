using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.LightningBolt;

public class TotemInstance : MonoBehaviour
{
    public LightningBoltScript[] bolts;

    [SerializeField] private List<Transform> nearbyEnemys = new List<Transform>();
    [SerializeField] private List<Enemy> aimingEnemys = new List<Enemy>();
    private Animator anim;

    [Space]
    [Header("Properties:")]
    public int damage;
    public int targets;
    public float distance;
    public float lifeTime;
    [Range(0f, 5)] public float cooldown;

    [Header("GameObject References:")]
    public GameObject blackhole;
    public GameObject _parent;

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
        StartCoroutine(TryToAttack());
        StartCoroutine(LifeTimer());
        Invoke("Awaked", 1.2f);

        targets = bolts.Length;
    }

    private void FixedUpdate()
    {
        if (!awaked)
            return;

        if (isDead)
            return;

        GetNearestTarget();

        if (aimingEnemys.Count > 0)
        {
            for (int i = aimingEnemys.Count - 1; i >= 0; i--)
            {
                OnGetTarget(aimingEnemys[i].transform, bolts[i]);
            }
        }

    }

    //private void UpdateList()
    //{
    //    for (int i = nearbyEnemys.Count - 1; i >= 0; i--)
    //    {
    //        if (!nearbyEnemys[i])
    //            nearbyEnemys.Remove(nearbyEnemys[i]);
    //    }
    //    for (int i = aimingEnemys.Count - 1; i >= 0; i--)
    //    {
    //        if (!aimingEnemys[i])
    //            aimingEnemys.Remove(aimingEnemys[i]);
    //    }
    //}

    private IEnumerator TryToAttack()
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
                for(int i = aimingEnemys.Count - 1; i >= 0; i--)
                {
                    Attack(aimingEnemys[i], bolts[i]);
                }
                yield return new WaitForSeconds(Time.deltaTime);
            }
            yield return null;
        }
    }

    private IEnumerator LifeTimer()
    {
        while (lifeTimer < lifeTime)
        {
            lifeTimer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        DeathAnimation();
    }

    private void Attack(Enemy myEnemy, LightningBoltScript bolt)
    {
        if (!awaked)
            return;

        if (myEnemy)
        {
            attackSound.PlayOneShot(attackClip[Random.Range(0, attackClip.Length)]);

            bolt.Trigger();
            if (myEnemy.ReceiveDamage(damage))
                GetNearestTarget();
        }
    }

    private void DeathAnimation()
    {
        isDead = true;
        anim.SetBool("isDead", true);
        blackhole.SetActive(true);
        Destroy(_parent, 1.6f);
        StopAllCoroutines();
    }

    private void OnGetTarget(Transform _target, LightningBoltScript bolt)
    {
        bolt.EndObject = _target.gameObject;
    }

    private void Awaked()
    {
        awaked = true;
        idleSound.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (!nearbyEnemys.Contains(other.transform))
            {
                nearbyEnemys.Add(other.transform);
                GetNearestTarget();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (nearbyEnemys.Contains(other.transform))
            {
                nearbyEnemys.Remove(other.transform);
                GetNearestTarget();
            }
        }
    }

    private void GetNearestTarget()
    {
        if (nearbyEnemys.Count == 0)
            return;

        aimingEnemys.Clear();

        for (int i = nearbyEnemys.Count - 1, j = 0; i >= 0; i--, j++)
        {
            if (!nearbyEnemys[i])
                nearbyEnemys.Remove(nearbyEnemys[i]);
        }

        float[] distances = new float[nearbyEnemys.Count];

        for (int i = nearbyEnemys.Count - 1, j = 0; i >= 0; i--, j++)
        {
            distances[j] = Vector3.Distance(transform.position, nearbyEnemys[i].position);
        }

        List<Transform> enemysByDistance = InsertionSort(distances, nearbyEnemys);

        for(int i = 0; i < enemysByDistance.Count; i++)
        {
            if (i >= targets)
                break;

            aimingEnemys.Add(enemysByDistance[i].GetComponent<Enemy>());
        }
    }

    private List<Transform> InsertionSort(float[] distances, List<Transform> enemys)
    {
        for (var i = 1; i < distances.Length; i++)
        {
            Transform aux1 = enemys[i];
            var aux = distances[i];
            var j = i - 1;

            while (j >= 0 && distances[j] > aux)
            {
                distances[j + 1] = distances[j];
                enemys[j + 1] = enemys[j];
                j -= 1;
            }
            enemys[j + 1] = aux1;
            distances[j + 1] = aux;
        }

        return enemys;
    }
}