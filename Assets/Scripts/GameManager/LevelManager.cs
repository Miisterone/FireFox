using System.Collections;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public SceneSwitcher SceneSwitcher;

    private void Start()
    {
        SceneSwitcher = GameObject.Find("Camera Rig").GetComponent<SceneSwitcher>();
    }

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
        SceneSwitcher.NextLevel();
    }
}
