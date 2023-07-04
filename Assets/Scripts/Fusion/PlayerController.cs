using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class PlayerController : NetworkBehaviour
{
    [SerializeField]
    private NetworkCharacterControllerPrototype networkCharacterController = null;

    [SerializeField]
    private float moveSpeed = 15f;

    Vector3 moveVector = Vector3.zero;

    public override void FixedUpdateNetwork()
    {
        if (GetInput(out InputData data))
        {
            moveVector = data.movementInput.normalized;
            //networkCharacterController.Transform.position += moveSpeed * moveVector * Runner.DeltaTime;
            networkCharacterController.Move( moveSpeed * moveVector * Runner.DeltaTime);
            //networkCharacterController.Transform.Rotate(Vector3.up,data.turnInput);
        }
        if (networkCharacterController.transform.position.y <= -5f)
        {
            Respawn();
        }
    }

    private void Respawn()
    {
        networkCharacterController.transform.position = Vector3.up * 2;
    }
}
