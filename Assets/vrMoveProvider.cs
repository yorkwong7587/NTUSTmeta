using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.XR.Interaction.Toolkit.Inputs;
using static UnityEngine.InputSystem.InputAction;

public class vrMoveProvider : MonoBehaviour
{
    [SerializeField] Transform forwardSource; // empty GameObject, Player child
                                              //[SerializeField] Rigidbody playerRigidbody; // if you wish to use Rigidbody's move/turn
    [SerializeField] InputActionProperty m_TurnAction; // set to: XRI Righthand Locomotion/Turn (Input Action Based)
    public InputActionProperty turnAction
    {
        get => m_TurnAction;
        set => SetInputActionProperty(ref m_TurnAction, value);
    }
    [SerializeField] InputActionProperty m_MoveAction; // set to: XRI Lefthand Locomotion/Move (Input Action Based)
    public InputActionProperty moveAction
    {
        get => m_MoveAction;
        set => SetInputActionProperty(ref m_MoveAction, value);
    }
    protected void OnEnable()
    {
        m_TurnAction.EnableDirectAction();
        m_MoveAction.EnableDirectAction();
    }
    protected void OnDisable()
    {
        m_TurnAction.DisableDirectAction();
        m_MoveAction.DisableDirectAction();
    }
    public Vector2 ReadTurnInput()
    {
        var turnValue = m_TurnAction.action?.ReadValue<Vector2>() ?? Vector2.zero;
        return turnValue;
    }
    public Vector2 ReadMoveInput()
    {
        var moveValue = m_MoveAction.action?.ReadValue<Vector2>() ?? Vector2.zero;
        return moveValue;
    }
    void SetInputActionProperty(ref InputActionProperty property, InputActionProperty value)
    {
        if (Application.isPlaying)
            property.DisableDirectAction();

        property = value;

        if (Application.isPlaying && isActiveAndEnabled)
            property.EnableDirectAction();
    }

    [Header("Turn")]
    [SerializeField] public bool canTurn = true;
    [SerializeField] float turnSpeed = 60f;
    [Header("Movement")]
    [SerializeField] public bool canMove = true;
    [SerializeField] float moveSpeed = 2f;

    void Update()
    {
        Vector2 turnValue = ReadTurnInput();
        Vector2 moveValue = ReadMoveInput();

        if (canMove && !moveValue.Equals(Vector2.zero))
        {
            Vector3 moveDirection = forwardSource.forward * moveValue.y + forwardSource.right * moveValue.x;
            Vector3 force = moveDirection.normalized * moveSpeed * Time.deltaTime;
            this.transform.position += force;
            //playerRigidbody.AddForce(force, ForceMode.Force); // if you wish to use Rigidbody's move/turn
        }

        /* // if you wish to use Rigidbody's move/turn
       if(!playerRigidbody.velocity.Equals(Vector3.zero))
        {
            Vector3 flatVel = new Vector3(playerRigidbody.velocity.x, 0f, playerRigidbody.velocity.z);
            if (flatVel.magnitude > moveSpeed * 100f)
            {
                Vector3 limitedVel = flatVel.normalized * moveSpeed*100f;
                playerRigidbody.velocity = new Vector3(limitedVel.x, playerRigidbody.velocity.y, limitedVel.z);
             }
        }
        */

        if (canTurn && !turnValue.Equals(Vector2.zero))
        {
            float turnAngle = turnValue.x * turnSpeed * Time.deltaTime;
            this.transform.Rotate(Vector3.up, turnAngle);
            //    playerRigidbody.AddRelativeTorque(Vector3.up * turnAngle * 10f); // if you wish to use Rigidbody's move/turn
        }
    }
}