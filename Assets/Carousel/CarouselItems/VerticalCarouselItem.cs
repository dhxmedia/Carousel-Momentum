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