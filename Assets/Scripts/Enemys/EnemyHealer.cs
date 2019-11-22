using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealer : Enemy
{
    public float healPerc;
    public int shieldCount = 2;

    List<Enemy> nearbyFriends = new List<Enemy>();

    public ParticleSystem shield;
    public ParticleSystem breakShield;

    private void FixedUpdate()
    {
        myHealth += healPerc * Time.deltaTime;
        myHealth = Mathf.Clamp(myHealth, -10, enemyData.maxHealth);
        healthBar.fillAmount = myHealth / enemyData.maxHealth;

        for (int i = nearbyFriends.Count - 1; i >= 0; i--)
        {
            //nearbyFriends[i].myHealth += healPerc * Time.deltaTime;
        }
    }

    public override bool ReceiveDamage(int damage)
    {
        if (shieldCount > 0)
        {
            shieldCount--;
            if(shieldCount == 0)
            {
                OnDestroyShield();
            }
            return false;
        }
        else
            return base.ReceiveDamage(damage);
    }

    void OnDestroyShield()
    {
        //StartCoroutine(HideShield());
        shield.gameObject.SetActive(false);
        breakShield.Play();
    }

    //IEnumerator HideShield()
    //{
    //    float t = 0;
    //    while (t <= 1)
    //    {
    //        shield.material.SetFloat("_Dissolve", t);
    //        t += Time.deltaTime;
    //        yield return new WaitForEndOfFrame();
    //    }
    //    shield.gameObject.SetActive(false);
    //}

    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);

        Enemy friend = other.GetComponent<Enemy>();

        if(!nearbyFriends.Contains(friend))
        {
            nearbyFriends.Add(friend);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Enemy friend = other.GetComponent<Enemy>();

        if (nearbyFriends.Contains(friend))
        {
            nearbyFriends.Remove(friend);
        }
    }
}
