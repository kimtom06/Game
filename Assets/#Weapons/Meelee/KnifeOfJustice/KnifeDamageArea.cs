using UnityEngine;

public class KnifeDamageArea : MonoBehaviour
{
    HealthModule mine;
    public int Damage = 5;
    private void Start()
    {
        mine = gameObject.GetComponentInParent<HealthModule>();
    }
    void OnTriggerEnter(Collider other)
    {
        

            if (other.gameObject.GetComponent<HealthModule>())
            {
            if (other.gameObject.GetComponent<HealthModule>() == mine)
            {
                return;
            }
            other.gameObject.GetComponent<HealthModule>().Damage(Damage);
           // other.gameObject.GetComponent<HealthModule>().KnockBack(transform,80);
            other.gameObject.GetComponent<HealthModule>().PlayParticle(other.transform.position, Quaternion.LookRotation(-transform.forward));
        }
    }
}
