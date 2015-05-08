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