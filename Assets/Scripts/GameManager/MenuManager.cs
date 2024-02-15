using UnityEngine;

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
       // SceneManager.LoadScene(2);
       SimpleSceneSwitcher sceneSwitcher = FindObjectOfType<SimpleSceneSwitcher>();
       sceneSwitcher.CycleToNextScene();
    }
}
