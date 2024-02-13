using System.Collections;
using UnityEngine;

public class SpawnerRock : MonoBehaviour
{
    public GameObject rockPrefab;
    
    
    void Start()
    {
        StartCoroutine(SpawnRock());
    }
    
    IEnumerator SpawnRock()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f);
            Instantiate(rockPrefab, transform.position, Quaternion.identity);
        }
    }
}
