using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMoveset : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform target;

    void Update()
    {
        agent.SetDestination(target.position);
    }
}
