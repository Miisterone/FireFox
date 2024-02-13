using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    private Animator foxAnimator;
    void Start()
    {
        foxAnimator = GetComponent<Animator>();
        foxAnimator.SetBool("Wait", true);
    }

    public void StartGame()
    {
       SceneManager.LoadScene("Level0");
    }
}
