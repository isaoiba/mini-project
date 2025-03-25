using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMoveset : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform target;
    public Transform location;

    private bool inChase;
    void Start()
    {
        inChase = false;
    }

    void Update()
    {
        if (inChase)
        {
            agent.SetDestination(target.position);
        }
        else
        {
            agent.ResetPath();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            inChase = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            inChase = false;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Touched player, do damage
        }
    }
}
