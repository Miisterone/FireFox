using UnityEngine;

public class FoxMenu : MonoBehaviour
{
    private Animator foxAnimator;
    private void Start()
    {
        foxAnimator = GetComponent<Animator>();
        foxAnimator.SetBool("Wait", true);
    }
}
