using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct AirState3D : ICharacterState3D
{

    private const int maxJumpCount = 2;

    private readonly Controller3D controller;

    private readonly Velocity3D velocity;

    private int jumpCount;

    private float jumpGracePeriod; //Amount of time player has to perform a jump after entering an airstate through falling off a platform

    private float timeEntered;

    public AirState3D(Controller3D controller, Velocity3D velocity)
    {
        if(controller == null)
        {
            throw new ArgumentNullException("Controller");
        }
        if(velocity == null)
        {
            throw new ArgumentNullException("Velocity");
        }

        this.controller = controller;
        this.velocity = velocity;
        jumpCount = 0;
        jumpGracePeriod = 0.2f;
        timeEntered = Time.time;
    }

    public void Enter()
    {
        velocity.SetY(0.0f);
    }

    public void Exit() { }

    public void Update(Vector3 movementInput, float deltaTime)
    {
        if (ShouldJump())
            PerformJump();

        UpdateVelocity(movementInput, deltaTime);
        if(Time.time > timeEntered + jumpGracePeriod && jumpCount == 0)
        {
            jumpCount++;
        }
    }

    public CharacterStateSwitch3D HandleCollisions(CollisionFlags collisionFlags)
    {
        CharacterStateSwitch3D stateSwitch;
        if((collisionFlags & CollisionFlags.Below) == CollisionFlags.Below)
        {
            stateSwitch = new CharacterStateSwitch3D(new GroundState3D(controller, velocity));
        } else
        {
            stateSwitch = new CharacterStateSwitch3D();
        }

        return stateSwitch;
    }

    private void UpdateVelocity(Vector3 movementInput, float deltaTime)
    {
        var smoothDampDataX = GetSmoothDampData(movementInput.x);
        var smoothDampDataZ = GetSmoothDampData(movementInput.z);

        velocity.AddY(controller.Gravity * deltaTime);
        velocity.SmoothDampUpdate(movementInput, smoothDampDataX, smoothDampDataZ, deltaTime);
    }

    private SmoothDampData GetSmoothDampData(float input)
    {
        var targetVelocity = input * controller.MoveSpeed;
        var smoothTime = controller.AirAccelerationTime;
        if(Mathf.Abs(input) < MathHelper.FloatEpsilon)
        {
            smoothTime *= controller.AirDeaccelerationScale;
        }

        return new SmoothDampData(targetVelocity, smoothTime);
    }

    private void PerformJump()
    {
        
        ++jumpCount;
        velocity.SetY(controller.MaxJumpVelocity);
           
    }

    private bool ShouldJump()
    {
        return Input.GetKeyDown(KeyCode.Space);
    }

}
