using UnityEngine;
using System.Collections.Generic;
public class HealthModule : MonoBehaviour
{
    public bool ShowHealthbar = false;
    [HideInInspector]public bool Alive = true;
    public int MaxHealth = 30;
    public int Health;

    public GameObject hitParticlePrefab;
    public int poolSize = 10;

    private List<ParticleSystem> particlePool;

    void Awake()
    {
        if (hitParticlePrefab) {
            particlePool = new List<ParticleSystem>();

            for (int i = 0; i < poolSize; i++)
            {
                GameObject obj = Instantiate(hitParticlePrefab, Vector3.zero, Quaternion.identity, transform);
                ParticleSystem ps = obj.GetComponent<ParticleSystem>();
                obj.SetActive(false);
                particlePool.Add(ps);
            }
        }
    }

    public void PlayParticle(Vector3 position, Quaternion rotation)
    {
        foreach (ParticleSystem ps in particlePool)
        {
            if (!ps.gameObject.activeInHierarchy)
            {
                ps.transform.SetPositionAndRotation(position, rotation);
                ps.gameObject.SetActive(true);
                ps.Play();
                StartCoroutine(DisableAfter(ps));
                return;
            }
        }

        Debug.LogWarning("All particles in pool are active. Consider increasing pool size.");
    }
    private IEnumerator<WaitForSeconds> DisableAfter(ParticleSystem ps)
    {
        yield return new WaitForSeconds(ps.main.duration);
        ps.Stop();
        ps.gameObject.SetActive(false);
    }

    private void Start()
    {
        Alive = true;
        Health = MaxHealth;
    }
    public bool Damage(int dam)
    {
        Health = Health - dam;
        Mathf.Clamp(Health, 0, MaxHealth);
        Alive = (Health != 0);
        return !Alive;
    }
}
