using UnityEngine;

public class SimpleBullet : MonoBehaviour
{
    public int Damage = 10;
    public float speed = 1f;
    public GameObject bulletHolePrefab; // Assign your bullet hole prefab in the inspector

    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }
    /*
     *         if (bulletHolePrefab != null)
        {
            ContactPoint contact = other.contactOffset();
            GameObject bulletHole = Instantiate(bulletHolePrefab, contact.point + contact.normal * 0.001f, Quaternion.LookRotation(contact.normal));
            bulletHole.transform.SetParent(other.transform);
        }
    */
    void OnTriggerEnter(Collider other)
    {
        print("hit!");
        if (other.gameObject.GetComponent<HealthModule>())
        {
            other.gameObject.GetComponent<HealthModule>().Damage(Damage);
        }
        Destroy(gameObject);
    }
    
}
