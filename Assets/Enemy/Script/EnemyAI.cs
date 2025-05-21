using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Weapons HoldingWeaponPrefab;
    public float stoppingDistance = 5f;
    public float shootCooldown = 0.5f;

    public Transform Hand;
    private Transform player;
    private NavMeshAgent agent;
    private float lastShootTime;

    void Start()
    {
        HoldingWeaponPrefab = Instantiate(HoldingWeaponPrefab, Hand);
        HoldingWeaponPrefab.transform.localPosition = new Vector3(0,0,0);
        HoldingWeaponPrefab.isAIUsing = true;
        agent = GetComponent<NavMeshAgent>();
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");

        if (playerObj != null)
            player = playerObj.transform;
        else
            Debug.LogError("Player not found! Make sure the Player GameObject is tagged 'Player'");
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= stoppingDistance)
        {
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

    void Shoot()
    {
        HoldingWeaponPrefab.UseWeapon();
    }
}