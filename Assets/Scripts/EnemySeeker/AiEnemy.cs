using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiEnemy : MonoBehaviour
{
    public Transform objective;

    public float speed;

    public NavMeshAgent iA;

    void Update()
    {
        iA.speed = speed;
        iA.SetDestination(objective.position);
    }
}
