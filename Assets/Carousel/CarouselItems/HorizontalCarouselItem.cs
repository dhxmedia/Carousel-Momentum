/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/. */
using UnityEngine;
using System.Collections;
namespace Carousel
{
	
	/// <summary>
	/// Horizontally aligned carousel item.
	/// </summary>
	public class HorizontalCarouselItem : CarouselItem {

		/// <summary>
		/// Calculates the score. 
		/// </summary>
		/// <returns>The score.</returns>
		public override float CalculateScore()
		{
			return transform.localPosition.x;
		}

		/// <summary>
		/// Updates the score and position. 
		/// </summary>
		/// <param name="score">Score.</param>
		public override void UpdateScore(float score)
		{
			transform.localPosition = new Vector3(score, transform.localPosition.y, transform.localPosition.z);
		}
	}
}