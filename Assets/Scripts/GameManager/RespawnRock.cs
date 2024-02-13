using UnityEngine;

public class RespawnRock : MonoBehaviour
{
    public GameObject rockPrefab;
    public GameObject spawn;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Rock"))
        {
            GameObject rock = other.gameObject;
            rock.transform.position = spawn.transform.position;
            rock.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }
}