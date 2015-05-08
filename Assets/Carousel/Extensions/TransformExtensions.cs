/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/. */

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
