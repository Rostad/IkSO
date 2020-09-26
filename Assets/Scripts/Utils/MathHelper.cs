using UnityEngine;
public static class MathHelper
{
    /// <summary>
    /// A value to use when comparing a float value with zero.
    /// </summary>
    public const float FloatEpsilon = 0.0001f;



    /// <summary>
    /// Gets the sign of the value.
    /// </summary>
    /// <param name="value">The value to get the sign from.</param>
    /// <returns><code>1</code> if <paramref name="value"/> is positive,
    /// <code>0</code> if <paramref name="value"/> is within <see cref="FloatEpsilon"/>of zero
    /// , else <code>-1</code>.</returns>


    public static int Sign(float value)
    {
        return Mathf.Abs(value) > FloatEpsilon
        ? value > 0.0f
        ? 1
        : -1
        : 0;
    }
    /// <summary>
    /// Clamps the angle between 0 and 359.
    /// </summary>
    /// <param name="angle">The angle to clamp.</param>
    /// <returns>The angle clamped between 0 and 359.</returns>
    public static float ClampAngleTo359(float angle)
    {
        return angle + ((int)angle / 360) * -360;
    }
}
