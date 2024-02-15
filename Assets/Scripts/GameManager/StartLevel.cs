using System.Collections;
using UnityEngine;

public class StartLevel : MonoBehaviour
{
    public GameObject faderScreen;
    public GameObject[] environementEnable;
    public GameObject[] environementDisable;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            faderScreen.GetComponent<ScreenFader>().FadeOut();
            StartCoroutine(Wait());
        }
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(4f);
        Disable();
        Enable();
        faderScreen.GetComponent<ScreenFader>().FadeIn();
    }

    private void Enable()
    {
        foreach (var e in environementEnable)
        {
            if (e != null)
            {
                e.SetActive(true);
            }
        }
    }
    
    private void Disable()
    {
        foreach (var e in environementDisable)
        {
            if (e != null)
            {
                e.SetActive(false);
            }
        }
    }
}