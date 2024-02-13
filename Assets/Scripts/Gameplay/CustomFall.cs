using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CustomFall : MonoBehaviour
{
    public float maxFallSpeed = -5f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (rb.velocity.y < maxFallSpeed)
        {
            rb.velocity = new Vector3(rb.velocity.x, maxFallSpeed, rb.velocity.z);
        }
    }
}