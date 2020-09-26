using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct GroundState3D : ICharacterState3D
{

    private readonly Controller3D controller;

    private readonly Velocity3D velocity;

    public GroundState3D(Controller3D controller, Velocity3D velocity)
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

    }

    public void Enter()
    {
        velocity.SetY(0.0f);
    }

    public void Exit() { }


    public void Update(Vector3 movementInput, float deltaTime)
    {

        if (Input.GetKeyDown(KeyCode.Space) && CanDodge())
        {
            var stateSwitch = new CharacterStateSwitch3D(new DodgeState3D(controller, velocity), movementInput, deltaTime, true);
            controller.ChangeCharacterState(stateSwitch);
        }

        /*else if (Input.GetKeyDown(KeyCode.Space))
        {

            var stateSwitch = new CharacterStateSwitch3D(new AirState3D(controller, velocity), movementInput, deltaTime, true);
            controller.ChangeCharacterState(stateSwitch);
        }*/
        else
        {
            UpdateVelocity(movementInput, deltaTime);
        }
    }

    private bool CanDodge()
    {
        return velocity.Current.magnitude > MathHelper.FloatEpsilon && Time.time > (controller.TimeOfLastDodge + controller.DodgeCooldown);
    }

    public CharacterStateSwitch3D HandleCollisions(CollisionFlags collisionFlags)
    {
        CharacterStateSwitch3D stateSwitch;
        if((collisionFlags & CollisionFlags.Below) == CollisionFlags.Below){
            stateSwitch = new CharacterStateSwitch3D();
        } 
        else
        {
            stateSwitch = new CharacterStateSwitch3D(new AirState3D(controller, velocity));
            controller.ChangeCharacterState(stateSwitch);
        }

        return stateSwitch;
    }

    private void UpdateVelocity(Vector3 movementInput, float deltaTime)
    {
        var SmoothDampDataX = GetSmoothDampData(movementInput.x);
        var SmoothDampDataZ = GetSmoothDampData(movementInput.z);

        velocity.SetY(-20.0f);
        velocity.SmoothDampUpdate(movementInput, SmoothDampDataX, SmoothDampDataZ, deltaTime);
    }

    private SmoothDampData GetSmoothDampData(float input)
    {
        var targetVelocity = input * controller.MoveSpeed;
        var smoothTime = controller.GroundAccelerationTime;
        if(Mathf.Abs(input) < MathHelper.FloatEpsilon)
        {
            smoothTime *= controller.GroundDeaccelerationScale;
        }

        return new SmoothDampData(targetVelocity, smoothTime);
    }
}
