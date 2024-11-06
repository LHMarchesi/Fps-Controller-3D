using System;
using UnityEngine;
using UnityEngine.AI;

public enum States
{
    onPatrol, chasing, attacking
}

public class AiEnemy : MonoBehaviour
{
    public float chaseRange;
    public float attackRange;
    public float speed;
    public float attackCooldown = 2f;
    public Transform[] waypoints;

    private int waypointsIndex;
    private Vector3 target;
    private PlayerController player;
    private NavMeshAgent iA;
    private States currentState;
    private bool hasAttacked;
    private float lastAttackTime;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        iA = GetComponent<NavMeshAgent>();
        iA.speed = this.speed;
        currentState = States.onPatrol;
        UpdateDestination();
    }

    //public Animation Anim; (Esto se debería activar cuando el Vigía tenga las animaciones)
    //public string NombreAnimacionCaminar; (para cuando esté la animación de caminar. Tipo, se pone el nombre) 
    //public string NombreAnimacionAtacar;

    void Update()
    {
        switch (currentState)
        {
            case States.onPatrol:
                Patrol();
                CheckForPlayer(); // Detect player within range
                break;

            case States.chasing:
                ChasePlayer();
                break;
            case States.attacking:
                if (Time.time >= lastAttackTime + attackCooldown && !hasAttacked)
                {
                    AttackPlayer();
                }
                else
                {
                    currentState = States.chasing; // Return to chasing after attack cooldown
                }
                break;
        }
    }

    private void AttackPlayer()
    {
        PlayerHealth playerHealth = player.gameObject.GetComponent<PlayerHealth>();
        playerHealth.GetDamage();
        hasAttacked = true;
        currentState = States.onPatrol;
        lastAttackTime = Time.time; 
    }

    private void Patrol()
    {
        // Move towards waypoints
        if (Vector3.Distance(transform.position, target) <= 2f) // If close to the target, go to next waypoint
        {
            IterateWaypoints();
            UpdateDestination();
        }
    }

    private void CheckForPlayer()
    {
        // Check if the player is within chase range
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (distanceToPlayer <= chaseRange)
        {
            currentState = States.chasing;
            hasAttacked = false; // Reset attack status when chasing starts
        }
    }

    private void ChasePlayer()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        // Move towards the player
        iA.SetDestination(player.transform.position);

        // Exit chasing if player moves out of range
        if (distanceToPlayer > chaseRange)
        {
            currentState = States.onPatrol;
            UpdateDestination();
        }

        // Check if player is within attack range
        if (distanceToPlayer <= attackRange)
        {
            currentState = States.attacking;
        }
    }

    private void UpdateDestination()
    {
        target = waypoints[waypointsIndex].position;
        iA.SetDestination(target);
    }

    private void IterateWaypoints()
    {
        waypointsIndex++;
        if (waypointsIndex == waypoints.Length)
        {
            waypointsIndex = 0;
        }
    }
}
