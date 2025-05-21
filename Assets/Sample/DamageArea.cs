using UnityEngine;

public class DamageArea : MonoBehaviour
{
    public bool DamageNotME = false;
    public float timer = 0f;
    public bool isInTrigger = false;
    public int Damage = 5;
    void OnTriggerStay(Collider other)
    {
        if(DamageNotME && other.CompareTag("Player"))
        {
            return;
        }
        timer += Time.deltaTime;
        if (timer >= 1f)
        {
            if (other.gameObject.GetComponent<HealthModule>())
            {
                other.gameObject.GetComponent<HealthModule>().Damage(Damage);
            }
            timer = 0f;
        }
    }
}
