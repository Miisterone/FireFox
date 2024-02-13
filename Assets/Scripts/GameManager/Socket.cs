using UnityEngine;
using UnityEngine.AI;

public class Socket : MonoBehaviour
{
    private GameObject handledObject;
    private NavMeshObstacle obstacle;

    private void Start()
    {
        obstacle = GetComponent<NavMeshObstacle>();
        handledObject = transform.GetChild(0).gameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Rock"))
        {
            obstacle.enabled = false;
            other.GetComponent<Rigidbody>().useGravity = false;
            other.GetComponent<Rigidbody>().isKinematic = true;
            other.GetComponent<NavMeshObstacle>().enabled = false;
            
            other.transform.position = handledObject.transform.position;
            other.transform.rotation = handledObject.transform.rotation;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Rock"))
        {
            obstacle.enabled = true;
            other.GetComponent<Rigidbody>().useGravity = true;
            other.GetComponent<Rigidbody>().isKinematic = false;
            other.GetComponent<NavMeshObstacle>().enabled = true;
        }
    }
}
