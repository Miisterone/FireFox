using Fusion;
using UnityEngine;

public class VRPlayerController : NetworkBehaviour
{
    [Networked]
    public Vector3 Position { get; set; }

    [Networked]
    public Quaternion Rotation { get; set; }

    public bool IsReady { get; private set; } = false;

    public override void Spawned() {
        IsReady = true;
    }

    void Update()
    {
        if (HasStateAuthority)
        {
            Position = transform.position;
            Rotation = transform.rotation;
        }
    }
}