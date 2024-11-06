using UnityEngine;
using UnityEngine.AI;

public enum States
{
    onPatrol,chasing
}

public class AiEnemy : MonoBehaviour
{
    public float chaseRange; 
    public float speed; 
    public Transform[] waypoints;

    private int waypointsIndex;
    private Vector3 target;
    private PlayerController player;
    private NavMeshAgent iA;
    private States currentState;


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
            }
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
        if (Vector3.Distance(transform.position, player.transform.position) <= chaseRange)
        {
            currentState = States.chasing;
        }
    }

    private void ChasePlayer()
    {
        // Move towards the player
        iA.SetDestination(player.transform.position);

        // Exit chasing if player moves out of range
        if (Vector3.Distance(transform.position, player.transform.position) > chaseRange)
        {
            currentState = States.onPatrol;
            UpdateDestination();
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            playerHealth.GetDamage();
        }
    }
}
