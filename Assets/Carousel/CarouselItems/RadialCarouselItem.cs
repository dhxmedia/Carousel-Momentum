using UnityEngine;
using System.Collections;
namespace Carousel
{
	/// <summary>
	/// Radially aligned carousel item.
	/// </summary>
	public class RadialCarouselItem : CarouselItem {

		/// <summary>
		/// Calculates the score. 
		/// </summary>
		/// <returns>The score.</returns>
		public override float CalculateScore()
		{
			return transform.RadiansFromZero();
		}
		
		/// <summary>
		/// Updates the score and position. 
		/// </summary>
		/// <param name="score">Score.</param>
		public override void UpdateScore(float score)
		{
			float r = new Vector2(transform.localPosition.x, transform.localPosition.y).magnitude;//transform.localPosition.magnitude;
            float th = score;

			transform.localPosition = new Vector3(r * Mathf.Sin(th), r * Mathf.Cos(th), transform.localPosition.z);
		}
	}
}