using UnityEngine;
using UnityEngine.AI;

public class IntroFallen : MonoBehaviour
{
    public GameObject[] planks;
    public GameObject bridge;

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Fox")
        {
            var foxAgent = other.GetComponent<NavMeshAgent>();
            if (foxAgent != null && foxAgent.isActiveAndEnabled)
            {
                foxAgent.enabled = false;
            }
            
            other.GetComponent<Animator>().SetBool("Falling",true);
            other.GetComponent<Animator>().enabled = false;
            
            var foxRigidbody = other.GetComponent<Rigidbody>();
            if (foxRigidbody != null)
            {
                foxRigidbody.useGravity = true;
            }

            bridge.GetComponent<MeshCollider>().enabled = false;

            foreach (var plank in planks)
            {
                var plankRigidbody = plank.GetComponent<Rigidbody>();
                if (plankRigidbody != null)
                {
                    plankRigidbody.useGravity = true;
                }
            }
        }
    }
}