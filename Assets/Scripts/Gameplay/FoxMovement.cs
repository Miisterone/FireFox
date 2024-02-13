using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class FoxMovement : MonoBehaviour
{
    public Transform destinationPoint;
    private NavMeshAgent navAgent;
    private Animator foxAnimator;
    public GameObject respawnPoint;

    private void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        foxAnimator = GetComponent<Animator>();
        destinationPoint = GameObject.FindWithTag("End").transform;

        navAgent.isStopped = false;
    }

    private void Update()
    {
        MoveToDestination();

        if (IsOnNavMeshLink())
        {
            foxAnimator.SetBool("IsWalking", true);
        }
        else if (navAgent.velocity.magnitude > 0)
        {
            foxAnimator.SetBool("IsWalking", true);
        }
        else
        {
            foxAnimator.SetBool("IsWalking", false);
        }
    }

    private void MoveToDestination()
    {
        if (destinationPoint != null)
        {
            navAgent.isStopped = false;
            navAgent.SetDestination(destinationPoint.position);
        }
        else
        {
            Debug.LogWarning("Aucun point de destination défini pour le renard!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if (other.CompareTag("End"))
        {
            navAgent.isStopped = true;
            foxAnimator.SetBool("IsWalking", false);
        }

        if (other.CompareTag("RockFalling"))
        {
            Dead();
        }
    }

    private bool IsOnNavMeshLink()
    {
        return navAgent.isOnOffMeshLink;
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("FireParticleSystem"))
        {
          Dead();
        }
    }

    private void Dead()
    {
        transform.position = respawnPoint.transform.position;
    }
}