using UnityEngine;
using UnityEngine.SceneManagement;
public class GameController : MonoBehaviour
{
    private Animator foxAnimator;
    void Start()
    {
        foxAnimator = GameObject.Find("Fox").GetComponent<Animator>();
        foxAnimator.SetBool("Wait", true);
    }
}
