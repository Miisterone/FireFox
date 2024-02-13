using UnityEngine;
using UnityEngine.AI;

public class End : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Animator>().SetBool("IsWalking", false);
            other.GetComponent<NavMeshAgent>().isStopped = true;
            other.GetComponent<NavMeshAgent>().speed = 0;
        }
    }
}
