using UnityEngine;
using System.Collections;

public static class TransformExtensions
{
    /// <summary>
    /// The radians from Vector3.up
    /// </summary>
    public static float RadiansFromZero(this UnityEngine.Transform transform)
    {
        return Vector3.Angle(Vector3.up, transform.localPosition) * (transform.localPosition.x < 0 ? -Mathf.Deg2Rad : Mathf.Deg2Rad);
    }
}
