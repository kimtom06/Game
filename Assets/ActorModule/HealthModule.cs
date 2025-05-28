using UnityEngine;
using System.Collections.Generic;
using System.Collections;
public class HealthModule : MonoBehaviour
{
    [HideInInspector]public bool Angry = false;
    public bool Player = false;
    public bool ShowHealthbar = false;
    [HideInInspector]public bool Alive = true;
    public int MaxHealth = 30;
    public int Health;
    Rigidbody rb;
    public GameObject hitParticlePrefab;
    public int poolSize = 10;
    public GameObject DeathEffectPrefab;
    private List<ParticleSystem> particlePool;
    
    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody>();
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
        Angry = true;
        if (Player)
        {
            DamageViggnetteModule.instance.ApplyDamageEffect();
        }
        Health = Health - dam;
        Mathf.Clamp(Health, 0, MaxHealth);
        if(Health <=0)
        {
           StartCoroutine(Death());
        }
        Alive = (Health > 0);
        return !Alive;
       
    }

    IEnumerator Death()
    {
        if (!Player)
        {
            
            rb.isKinematic = true;
            if (DeathEffectPrefab)
            {
                Instantiate(DeathEffectPrefab, transform).transform.localPosition = new Vector3(0, 0, 0);


            }
            for (int i = 0; i < 50; i++)
            {
                rb.position -= Vector3.up *0.05f;
                yield return new WaitForEndOfFrame();
            }

            Destroy(gameObject);
        }
        else
        {
            //Player is dead now get lost bruh
        }
        
    }
    public void KnockBack(Transform origin,float knockbackForce)
    {
        Vector3 knockbackDirection = transform.position - origin.position;
        rb.AddForce(knockbackDirection.normalized * knockbackForce, ForceMode.Impulse);
    }
}
