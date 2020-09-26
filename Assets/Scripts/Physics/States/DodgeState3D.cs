using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct DodgeState3D : ICharacterState3D
{

    private readonly Controller3D controller;

    private readonly Velocity3D velocity;

    private readonly Vector3 direction;

    private float timer;
    private float duration;


    public DodgeState3D(Controller3D controller, Velocity3D velocity)
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
        direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0.0f, Input.GetAxisRaw("Vertical"));
        direction.Normalize();
        timer = 0.0f;
        duration = controller.DodgeDuration;
    }
    public void Enter()
    {
        velocity.SetY(0.0f);
    }

    public void Exit()
    {
        controller.TimeOfLastDodge = Time.time;
    }

    public CharacterStateSwitch3D HandleCollisions(CollisionFlags collisionFlags)
    {
        CharacterStateSwitch3D stateSwitch;
        if ((collisionFlags & CollisionFlags.Below) == CollisionFlags.Below)
        {
            stateSwitch = new CharacterStateSwitch3D();
        }
        else
        {
            stateSwitch = new CharacterStateSwitch3D(new AirState3D(controller, velocity));
            controller.ChangeCharacterState(stateSwitch);
        }

        return stateSwitch;
    }

    public void Update(Vector3 movementInput, float deltaTime)
    {
        UpdateVelocity(direction, deltaTime);

        timer += deltaTime;
        if(timer >= duration)
        {
            var stateSwitch = new CharacterStateSwitch3D(new GroundState3D(controller, velocity), movementInput, deltaTime, true);
            controller.ChangeCharacterState(stateSwitch);
        }
    }

    public void UpdateVelocity(Vector3 direction, float deltaTime)
    {
        var smoothDampDataX = GetSmoothDampData(direction.x);
        var smoothDampDataZ = GetSmoothDampData(direction.z);

        velocity.SetY(-20.0f);
        velocity.SmoothDampUpdate(direction, smoothDampDataX, smoothDampDataZ, deltaTime);
    }

    private SmoothDampData GetSmoothDampData(float direction)
    {
        var targetVelocity = direction * controller.DodgeSpeed;
        var smoothTime = 0.01f;
        return new SmoothDampData(targetVelocity, smoothTime);
    }

    
}
