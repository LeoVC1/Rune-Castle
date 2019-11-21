using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VFX
{
    public class GroundCrackle : MonoBehaviour
    {
        [SerializeField] private MeshRenderer[] cracklesMask;
        [SerializeField] private ParticleSystem[] explosions;

        [SerializeField] private Vector2 crackleSpeed;
        [SerializeField] private Vector2 crackleScale;
        [SerializeField] private float explosionsInterval;
        [SerializeField] private float explosionDelay;

        private void Start()
        {
            DesactivateCrackle();
        }

        private void Update()
        {

            if (Input.GetKeyDown(KeyCode.Alpha9))
            {
                Explode();
            }
        }

        private void ActivateCrackle()
        {
            StartCoroutine(ShowHoles());
        }

        private void DesactivateCrackle(bool fade = false)
        {
            if(fade)
                StartCoroutine(HideHoles());
            else
                for (int i = 0; i < cracklesMask.Length; i++)
                {
                    float t = 1;
                    while (t >= 0)
                    {
                        cracklesMask[i].material.SetFloat("_Tile", t);
                        t -= Time.deltaTime;
                    }
                    cracklesMask[i].material.SetFloat("_Tile", 0);
                }
        }

        public void Explode()
        {
            StartCoroutine(ActivateExplosions());
        }

        IEnumerator ActivateExplosions()
        {

            for (int i = 0; i < explosions.Length; i++)
            {
                explosions[i].gameObject.SetActive(true);
                explosions[i].Play();
                cracklesMask[i].material.SetFloat("_Tile", 0.5f);
                yield return new WaitForSeconds(explosionsInterval);
            }
            yield return new WaitForSeconds(2.5f);
            DesactivateCrackle(false);
        }

        IEnumerator ShowHoles()
        {
            for (int i = 0; i < cracklesMask.Length; i++)
            {
                float t = 0;
                while (t < 0.5f)
                {
                    t += Time.deltaTime * Random.Range(crackleScale.x, crackleScale.y);
                    cracklesMask[i].material.SetFloat("_Tile", t);
                    yield return new WaitForSeconds(Random.Range(crackleSpeed.x, crackleSpeed.y));
                }
                cracklesMask[i].material.SetFloat("_Tile", 0.5f);
            }
            yield return new WaitForSeconds(explosionDelay);
            Explode();
        }

        IEnumerator HideHoles()
        {
            for (int i = 0; i < cracklesMask.Length; i++)
            {
                float t = 0.5f;
                while (t < 1)
                {
                    t += Time.deltaTime * Random.Range(crackleScale.x, crackleScale.y);
                    cracklesMask[i].material.SetFloat("_Tile", t);
                    yield return new WaitForSeconds(Random.Range(crackleSpeed.x, crackleSpeed.y));
                }
                cracklesMask[i].material.SetFloat("_Tile", 1);
            }
            yield return new WaitForSeconds(explosionDelay);
        }

    }
}