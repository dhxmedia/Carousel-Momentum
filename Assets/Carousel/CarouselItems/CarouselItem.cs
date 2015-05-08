/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/. */

using UnityEngine;
using System.Collections;
namespace Carousel
{
	/// <summary>
	/// The component to attach to an item in the <see cref="Carousel.Carousel"/>.
	/// </summary>
	public abstract class CarouselItem : MonoBehaviour {


        public float Padding;

        public float SizeBottom;

        public float SizeTop;

		/// <summary>
		/// Updates the <see cref="Score"/> value. Override in subclasses to make it update position.
		/// </summary>
		/// <param name="score">Score.</param>
		abstract public void UpdateScore(float score);


		/// <summary>
		/// Calculates the score. Override in subclasses to make it dependent on position.
		/// </summary>
		/// <returns>The score.</returns>
		abstract public float CalculateScore();
	}
}