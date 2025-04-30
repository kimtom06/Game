using UnityEngine;

[RequireComponent(typeof(Collider))]
public class SimpleBullet : MonoBehaviour
{
    public int Damage = 10;
    public float speed = 100f;
    public GameObject bulletHolePrefab;
    public float maxLifetime = 5f;

    private Vector3 lastPosition;

    void Start()
    {
        lastPosition = transform.position;
        Destroy(gameObject, maxLifetime);
    }

    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
        Vector3 direction = transform.position - lastPosition;
        float distance = direction.magnitude;

        if (distance > 0 && Physics.Raycast(lastPosition, direction.normalized, out RaycastHit hit, distance))
        {
            HandleHit(hit.collider, hit.point, hit.normal);
        }

        lastPosition = transform.position;
    }

    void OnTriggerEnter(Collider other)
    {
        HandleHit(other, transform.position, -transform.forward);
    }

    void HandleHit(Collider other, Vector3 hitPoint, Vector3 hitNormal)
    {
        HealthModule health = other.GetComponent<HealthModule>();
        if (health != null)
        {
            Debug.Log("Hit!");
            health.Damage(Damage);
            health.PlayParticle(hitPoint, Quaternion.LookRotation(-transform.forward));
        }

        if (bulletHolePrefab != null)
        {
            GameObject bulletHole = Instantiate(
                bulletHolePrefab,
                hitPoint + hitNormal * 0.001f,
                Quaternion.LookRotation(hitNormal)
            );
            bulletHole.transform.SetParent(other.transform);
        }

        Destroy(gameObject);
    }
}
