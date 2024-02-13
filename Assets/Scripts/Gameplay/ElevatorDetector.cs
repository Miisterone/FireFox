using System;
using Unity.AI.Navigation;
using UnityEngine;

public class ElevatorDetector : MonoBehaviour
{
    public NavMeshLink navMeshLink;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Elevator"))
        {
           navMeshLink.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Elevator"))
        {
            navMeshLink.enabled = false;
        }
    }
}
