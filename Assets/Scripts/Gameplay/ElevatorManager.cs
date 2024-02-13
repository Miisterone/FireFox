using System;
using UnityEngine;

public class ElevatorManager : MonoBehaviour
{
    public Animator elevatorAnimator;

    private void Start()
    {
        elevatorAnimator = GetComponent<Animator>();
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            elevatorAnimator.SetBool("IsActive", value: !elevatorAnimator.GetBool("IsActive"));
        }
    }

    public void Down()
    {
        elevatorAnimator.SetBool("IsActive", value: !elevatorAnimator.GetBool("IsActive"));
    }
}
