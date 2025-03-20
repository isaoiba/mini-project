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
    
    void Start()
    {
        
    }

    void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("YourAnimationName"))
        {
            agent.SetDestination(target.position);
        }
        else
        {
            agent.isStopped = true;
        }


    }
}
