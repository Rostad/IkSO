using UnityEngine;
public struct CharacterStateSwitch3D
{
    private readonly ICharacterState3D newState;
    private readonly Vector3 movementInput;
    private readonly float deltaTime;
    private readonly bool runImmediately;

    public CharacterStateSwitch3D(ICharacterState3D newState)
    : this(newState, Vector3.zero, 0.0f, false)
    { }

    public CharacterStateSwitch3D(ICharacterState3D newState, Vector3 movementInput,
   float deltaTime, bool runImmediately)
    {
        this.newState = newState;
        this.movementInput = movementInput;
        this.deltaTime = deltaTime;
        this.runImmediately = runImmediately;
    }
    /// <summary>
    /// The new state, if any.
    /// </summary>
    public ICharacterState3D NewState
    {
        get { return newState; }
    }
    /// <summary>
    /// The movement input vector to use for the new state.
    /// </summary>
    public Vector3 MovementInput
    {
        get { return movementInput; }
    }
    /// <summary>
    /// The delta time to use for the new state.
    /// </summary>
    public float DeltaTime
    {
        get { return deltaTime; }
    }

    /// <summary>
    /// True if the new state should be run in the same update.
    /// </summary>
    public bool RunImmediately
    {
        get { return runImmediately; }
    }
}