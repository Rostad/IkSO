using System;
using UnityEngine;
public class Velocity3D
{
    // This class currently only uses smooth damping to update the velocity.
    // It makes perfect sense to add additional update methods and mix and match
    // between them if desired.
    private Vector3 velocity;

    private Vector3 velocityDampSmoothing;
    // The terminal velocity; the maximum velocity y. Gravity stops building up eventually.
    private float terminalVelocity;
    // Save the delta time each update since it will be used to calculate the current velocity.
    // We could calculate the current velocity in the update method and save it to a field, but doing so
    // would force the callers to remember to change the gravity before calling the update method.
    // By using the delta time this way the caller is free to change the gravity and update the velocity
    // in any order.
    private float deltaTime;
    public Velocity3D(float terminalVelocity)
    {
        if (terminalVelocity > 0.0f)
        {
            throw new ArgumentOutOfRangeException("terminalVelocity", terminalVelocity,
           "The terminal velocity must be negative.");
        }

        this.terminalVelocity = terminalVelocity;
    }
    public Vector3 Current
    {
        get { return velocity * deltaTime; }
    }

    public Vector3 CurrentNormalized
    {
        get { return velocity.normalized; }
    }

    public void SmoothDampUpdate(Vector3 movementInput, SmoothDampData smoothDampDataX,
   SmoothDampData smoothDampDataZ, float deltaTime)
    {
        velocity.x = Mathf.SmoothDamp(velocity.x, smoothDampDataX.TargetVelocity, ref velocityDampSmoothing.x, smoothDampDataX.SmoothTime);
        velocity.z = Mathf.SmoothDamp(velocity.z, smoothDampDataZ.TargetVelocity, ref velocityDampSmoothing.z, smoothDampDataZ.SmoothTime);
        this.deltaTime = deltaTime;
    }
    public void AddY(float velocityY)
    {
        velocity.y += velocityY;
        ClampVelocityYToTerminalVelocity();
    }
    public void SetY(float velocityY)
    {
        velocity.y = velocityY;
        ClampVelocityYToTerminalVelocity();
    }
    private void ClampVelocityYToTerminalVelocity()
    {
        velocity.y = Math.Max(velocity.y, terminalVelocity);
    }
}