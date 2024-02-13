using UnityEngine;
using UnityEngine.AI;

public class FoxIntro : MonoBehaviour
{
    public Transform destinationPoint;
    private NavMeshAgent navAgent;
    private Animator foxAnimator;
    
    private void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        foxAnimator = GetComponent<Animator>();
        if (navAgent != null)
        {
            navAgent.isStopped = false;
        }
    }
 
    private void Update()
    {
        if (navAgent != null && navAgent.isActiveAndEnabled)
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
    }
 
    private void MoveToDestination()
    {
        if (destinationPoint != null && navAgent != null && navAgent.isActiveAndEnabled)
        {
            navAgent.isStopped = false;
            navAgent.SetDestination(destinationPoint.position);
        }
        else
        {
            Debug.LogWarning("Aucun point de destination défini pour le renard ou NavMeshAgent est désactivé!");
        }
    }
 
    private bool IsOnNavMeshLink()
    {
        return navAgent != null && navAgent.isOnOffMeshLink;
    }
}