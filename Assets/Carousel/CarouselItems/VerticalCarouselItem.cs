/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/. */
using UnityEngine;
using System.Collections;
namespace Carousel
{
	/// <summary>
	/// Vertically aligned carousel item.
	/// </summary>
	public class VerticalCarouselItem : CarouselItem {

		/// <summary>
		/// Calculates the score. 
		/// </summary>
		/// <returns>The score.</returns>
		public override float CalculateScore()
		{
			return transform.localPosition.y;
		}
		
		/// <summary>
		/// Updates the score and position. 
		/// </summary>
		/// <param name="score">Score.</param>
		public override void UpdateScore(float score)
		{
			transform.localPosition = new Vector3(transform.localPosition.x, score, transform.localPosition.z);
		}
	}
}