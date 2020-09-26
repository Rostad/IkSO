using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Controller3D : MonoBehaviour
{

    #region constants

    public KeyCode dodgeKey = KeyCode.JoystickButton1;

    #endregion
    [Tooltip("Units per second")]
    public float MoveSpeed = 6.0f;

    [Tooltip("Units per second")]
    public float DodgeSpeed = 1.0f;

    [Tooltip("Duration of dodge state")]
    public float DodgeDuration = 0.8f;

    [Tooltip("Maximum downwards velocity (enter as positive value")]
    public float TerminalVelocity = 18.0f;

    [Tooltip("Duration in seconds")]
    public float DodgeCooldown = 1.2f;

    public float GroundAccelerationTime = 0.1f;

    public float AirAccelerationTime = 0.2f;

    public float GroundDeaccelerationScale = 0.8f;

    public float AirDeaccelerationScale = 3.5f;

    [Tooltip("The maximum jump height possible in units")]
    public float MaxJumpHeight = 4.0f;

    [Tooltip("The minimum jump height possible in units")]
    public float MinJumpHeight = 1.0f;

    [Tooltip("The time it takes in seconds to reach the maximum jump height")]
    public float TimeToJumpApex = 0.4f;

    private CharacterController characterController;

    private Velocity3D velocity;

    private ICharacterState3D characterState;

    public float MaxJumpVelocity { get; private set; }

    public float MinJumpVelocity { get; private set; }

    public float Gravity { get; private set; }

    public float TimeOfLastDodge { get; set; }

    public float ColliderHeight
    {
        get { return characterController.height; }
    }

    public void ChangeCharacterState(CharacterStateSwitch3D stateSwitch)
    {
        PrintStateSwitch(stateSwitch);

        characterState.Exit();
        characterState = stateSwitch.NewState;
        characterState.Enter();
        if (stateSwitch.RunImmediately)
        {
            characterState.Update(stateSwitch.MovementInput, stateSwitch.DeltaTime);
        }
    }

    private void Awake()
    {
        CreateVelocity();
        CacheComponents();
        CalculateGravity();
        CalculateJumpVelocities();
        SetInitialCharacterState();


        TimeOfLastDodge = -DodgeCooldown;
    }

    private void Update()
    {
        if (!GamePauser.IsPaused)
        {
            var deltaTime = Time.deltaTime;
            characterState.Update(GetMovementInput(), deltaTime);
            HandleCollisions(Move());
        }
        DrawAxes();
    }

    private void CacheComponents()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void CalculateGravity()
    {
        Gravity = -(2 * MaxJumpHeight) / Mathf.Pow(TimeToJumpApex, 2);
    }

    private void CalculateJumpVelocities()
    {
        var positiveGravity = Mathf.Abs(Gravity);
        MaxJumpVelocity = positiveGravity * TimeToJumpApex;
        MinJumpVelocity = Mathf.Sqrt(2 * positiveGravity * MinJumpHeight);
    }

    private void CreateVelocity()
    {
        velocity = new Velocity3D(-TerminalVelocity);
    }

    private void SetInitialCharacterState()
    {
        if (characterController.isGrounded)
        {
            characterState = new GroundState3D(this, velocity);
        }
        else
        {
            characterState = new AirState3D(this, velocity);
        }
    }

    private Vector3 GetMovementInput()
    {
        Vector3 v = new Vector3(Input.GetAxisRaw("Horizontal"), 0.0f, Input.GetAxisRaw("Vertical"));
        

        return v;

        
    }

    private CollisionFlags Move()
    {
        var moveDirection = transform.TransformDirection(velocity.Current).normalized;
        var moveLength = velocity.Current.magnitude;
        var motion = moveDirection * moveLength;
        return characterController.Move(motion);
    }

    private void HandleCollisions(CollisionFlags collisionFlags)
    {
        var stateSwitch = characterState.HandleCollisions(collisionFlags);
        if(stateSwitch.NewState != null)
        {
            ChangeCharacterState(stateSwitch);
        }
    }

    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    private void DrawAxes()
    {
        Debug.DrawRay(transform.position + transform.forward * characterController.radius, transform.forward, Color.blue);
        Debug.DrawRay(transform.position + transform.right * characterController.radius, transform.right, Color.red);
        Debug.DrawRay(transform.position + transform.up * characterController.height * 0.5f, transform.up, Color.green);

    }
    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    private void PrintStateSwitch(CharacterStateSwitch3D stateSwitch)
    {
        print("Switching character state from " + characterState.ToString() + " to " +
       stateSwitch.NewState.ToString());
    }

}
