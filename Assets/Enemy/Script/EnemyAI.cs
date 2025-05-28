using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public HealthModule Health;
    public Weapons HoldingWeaponPrefab;
    public float stoppingDistance = 5f;
    public float shootCooldown = 0.5f;
    public float sightDistance = 10f; // Sight detection distance
    private float org;
    public LayerMask obstacleLayer;  // Layer for obstacles to check line of sight
    public bool JumpForNoreason = false;
    public Transform Hand;
    private Transform player;
    private NavMeshAgent agent;
    private float lastShootTime;

    void Start()
    {
        org = sightDistance;
        HoldingWeaponPrefab = Instantiate(HoldingWeaponPrefab, Hand);
        HoldingWeaponPrefab.transform.localPosition = new Vector3(0, 0, 0);
        HoldingWeaponPrefab.isAIUsing = true;
        agent = GetComponent<NavMeshAgent>();
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");

        if (playerObj != null)
            player = playerObj.transform;
        else
            Debug.LogError("Player not found! Make sure the Player GameObject is tagged 'Player'");
    }
    private float jumpIntervalMin = 3f;
    private float jumpIntervalMax = 5f;
    private float nextJumpTime;
    void Jump()
    {

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(Vector3.up * 500f, ForceMode.Impulse);
        }
        else
        {
            Debug.LogWarning("No Rigidbody found on the NavMeshAgent!");
        }
    }

    void SetNextJumpTime()
    {
        nextJumpTime = Time.time + Random.Range(jumpIntervalMin, jumpIntervalMax);
    }
    void Update()
    {
        
        if (Health.Angry)
        {
            sightDistance = 20f;
        }
        else
        {
            sightDistance = org;
        }
        if (player == null) return;

        // Check if the player is within the sight range and visible
        float distance = Vector3.Distance(transform.position, player.position);
        bool isPlayerInSight = IsPlayerInSight();

        if (isPlayerInSight && distance <= sightDistance)
        {
            if (distance <= stoppingDistance)
            {
                if (JumpForNoreason)
                {
                    if (Time.time >= nextJumpTime)
                    {
                        Jump();
                        SetNextJumpTime(); // Set the next random jump time
                    }
                }
                agent.isStopped = true;

                // Rotate toward the player
                Vector3 direction = (player.position - transform.position).normalized;
                Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);

                // Shoot with cooldown
                if (Time.time - lastShootTime > shootCooldown)
                {
                    Shoot();
                    lastShootTime = Time.time;
                }
            }
            else
            {
                agent.isStopped = false;
                agent.SetDestination(player.position);
            }
        }
        else
        {
            // If player is out of sight range, continue patrolling or idle (can add patrol logic here if needed)
            agent.isStopped = false;
        }
    }

    bool IsPlayerInSight()
    {
        // Raycast to check if there is a clear line of sight to the player
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        Ray ray = new Ray(transform.position, directionToPlayer);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, sightDistance, obstacleLayer))
        {
            // If ray hits something that is not the player, it means the player is obstructed
            if (hit.collider.CompareTag("Player"))
            {
                return true;
            }
            return false;
        }

        return true;  // No obstacles in the way, so the player is in sight
    }

    void Shoot()
    {
        if (Health.Alive)
        {
            HoldingWeaponPrefab.UseWeapon();
        }
    }
}
