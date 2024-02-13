using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public string nextLevelName;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Level finished!");
            other.GetComponent<Animator>().SetBool("End", true);
            StartCoroutine(WaitAndLoad());
        }
    }
    
    private IEnumerator WaitAndLoad()
    {
        yield return new WaitForSecondsRealtime(4f);
        SceneManager.LoadScene(nextLevelName);
    }
}
