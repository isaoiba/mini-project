using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMoveset : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform target;
    public Transform location;

    public Animator animator;
    
    bool isShot
    void Start()
    {
        
    }

    void Update()
    {
        agent.SetDestination(target.position);

        //isShot = Physics.CheckCapsule(groundCheck.position, groundDistance, groundMask);
    }
}
