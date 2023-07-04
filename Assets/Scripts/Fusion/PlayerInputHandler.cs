using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Inputs;
using Fusion;

public class PlayerInputHandler : MonoBehaviour
{
    InputData _data;
    Vector2 turnValue = Vector2.zero;
    Vector2 moveValue = Vector2.zero;

    [SerializeField] bool _onlySendInputWhenFocused;
    [SerializeField] Transform _relativeTo;
    [SerializeField] Transform _head;
    public LocalController LeftController;
    public LocalController RightController;

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

    private void Awake()
    {
        if( _relativeTo == null )
        {
            _relativeTo = transform.parent;
        }
    }

    void Start()
    {
        var networkedParent = GetComponentInParent<NetworkObject>();
        if( networkedParent == null || networkedParent.Runner == null )
        {
            return;
        }

        var runner = networkedParent.Runner;
        var events = runner.GetComponent<NetworkEvents>();

        events.OnInput.AddListener( OnInput );

        var player = networkedParent.GetComponent<Player>();
        if( player != null )
        {
            player._leftHand.SetLocalController( LeftController );
            player._rightHand.SetLocalController( RightController );
        }
    }

    private void Update()
    {
        LeftController?.UpdateInput( ref _data.Left );
        RightController?.UpdateInput( ref _data.Right );       
    }

    void OnInput( NetworkRunner runner, NetworkInput inputContainer )
    {
        if( _onlySendInputWhenFocused && Application.isFocused == false )
        {
            return;
        }
        _data.HeadLocalPosition = _relativeTo.InverseTransformPoint( _head.position );
        _data.HeadLocalRotation = Quaternion.Inverse( _relativeTo.rotation ) * _head.rotation;

        LeftController?.UpdateInputFixed( ref _data.Left );
        RightController?.UpdateInputFixed( ref _data.Right );
        
        turnValue = ReadTurnInput();
        moveValue = ReadMoveInput();

        if (canMove && !moveValue.Equals(Vector2.zero))
        {
            Vector3 moveDirection = _head.forward * moveValue.y + _head.right * moveValue.x;
            _data.movementInput = moveDirection;
        }
        if (canTurn && !turnValue.Equals(Vector2.zero))
        {
            float turnAngle = turnValue.x * turnSpeed * Time.deltaTime;
            _data.turnInput = turnAngle;
        }
        inputContainer.Set( _data );

        _data.ResetStates();
    }
}
