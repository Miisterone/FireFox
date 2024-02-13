using Fusion;
using UnityEngine;

public class PlayerMovement : NetworkBehaviour
{
    private CharacterController _controller;

    public float PlayerSpeed = 2f;
    public Camera Camera;
    public float JumpForce = 5f;
    public float GravityValue = -9.81f;

    private Vector3 velocity;
    private bool _jumpPressed;

    [Networked] 
    public string playerName { get; set; }

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }
    
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            _jumpPressed = true;
        }
    }

    public override void Spawned()
    {
        if (HasStateAuthority)
        {
            Camera = gameObject.transform.GetChild(1).GetComponent<Camera>();
            Camera.GetComponent<FirstPersonCamera>().Target = GetComponent<NetworkTransform>().InterpolationTarget;
        }

        playerName = "VRPlayer";
        name = playerName;
    }

    public override void FixedUpdateNetwork()
    {
        // Only move own player and not every other player. Each player controls its own player object.
        if (HasStateAuthority == false)
        {
            Destroy(Camera.GetComponent<FirstPersonCamera>());
            return;
        }

        var cameraRotationY = Quaternion.Euler(0, Camera.transform.rotation.eulerAngles.y, 0);
        Vector3 move = cameraRotationY * new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * Runner.DeltaTime * PlayerSpeed;

        velocity.y += GravityValue * Runner.DeltaTime;
        if (_jumpPressed && _controller.isGrounded)
        {
            velocity.y += JumpForce;
        }

        _controller.Move(move + velocity * Runner.DeltaTime);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }

        if (_controller.isGrounded)
        {
            velocity = new Vector3(0, -1, 0);
        }

        _jumpPressed = false;
    }
}