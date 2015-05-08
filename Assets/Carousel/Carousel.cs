using UnityEngine;
using System.Collections;
namespace Carousel
{
	/// <summary>
	/// Generic infinite carousel. 
	/// A carousel is a design that lets us scroll through items. 
	/// Use the Events to swap out items appropriately.
	/// </summary>
	public class Carousel : MonoBehaviour {
		
		/// <summary>
		/// When the least poisition has moved to the most position
		/// Broadcast the most up to date least and most
		/// </summary>
		/// <param name="least">Least positioned GameObject</param>
		/// <param name="most">Most positioned GameObject</param>
		public System.Action<GameObject, GameObject> OnLeastToMost;
		
		/// <summary>
		/// When the least poisition has moved to the most position
		/// Broadcast the most up to date least and most
		/// </summary>
		/// <param name="least">Least positioned GameObject</param>
		/// <param name="most">Most positioned GameObject</param>
		public System.Action<GameObject, GameObject> OnMostToLeast;

        /// <summary>
        /// Don't allow MostToLeast if <see cref="CapMin"/> is true
        /// </summary>
        public bool CapMin { get; set; }

        /// <summary>
        /// Don't allow LeastToMost if <see cref="CapMax"/> is true
        /// </summary>
        public bool CapMax { get; set; }
		
		/// <summary>
		/// The items in the Carousel.
		/// </summary>
		public CarouselItem[] Items;
		
		/// <summary>
		/// Poll <see cref="CarouselItem.Score"/> of Items and swap them
		/// </summary>
		void LateUpdate()
		{
			if(Items.Length > 0)
			{
                bool moved = true;
                while(moved)
                { 
				    //Sort Items, but we only care about the lowest 2 and the highest 2
				    //We're using 4 variables instead of an array so that we don't allocate any memory
				    //LINQ way with allocations: var scores = Items.Select(i => new{score=i.CalculateScore(), item=i}).OrderBy(i => i.score); 
				    CarouselItem itemLowest, itemLow, itemHigh, itemHighest;
				    itemLowest = itemLow = itemHigh = itemHighest = null;

				    float scoreLowest, scoreLow, scoreHigh, scoreHighest;
				    scoreLowest = scoreLow = Mathf.Infinity;
				    scoreHigh = scoreHighest = Mathf.NegativeInfinity;

				    for(int i = 0; i < Items.Length; i++)
				    {
					    float s = Items[i].CalculateScore();
					    if(s < scoreLowest)
					    {
						    itemLow = itemLowest;
						    itemLowest = Items[i];
						
						    scoreLow = scoreLowest;
						    scoreLowest = s;
					    }
					    else if(s < scoreLow)
					    {
						    itemLow = Items[i];
						
						    scoreLow = s;
					    }
					
					    if(s > scoreHighest)
					    {
						    itemHigh = itemHighest;
						    itemHighest = Items[i];
						
						    scoreHigh = scoreHighest;
						    scoreHighest = s;
					    }
					    else if(s > scoreHigh)
					    {
						    itemHigh = Items[i];
						
						    scoreHigh = s;
					    }
				    }
                    moved = false;

                    float lowestBottom = Mathf.Abs(scoreLowest - itemLowest.Padding - itemLowest.SizeBottom);
                    float lowestTop = Mathf.Abs(scoreLowest + itemLowest.Padding + itemLowest.SizeTop);

                    float highestBottom = Mathf.Abs(scoreHighest - itemHighest.SizeBottom - itemHighest.Padding);
                    float highestTop = Mathf.Abs(scoreHighest + itemHighest.SizeTop + itemHighest.Padding);

                    if ((lowestTop > highestTop) && (CapMax == false))
                    {
                        moved = true;
                        LeastToMost(itemLowest, itemHighest, itemLow);
                    }
                    if ((highestBottom > lowestBottom) && (CapMin == false))
                    {
                        moved = true;
                        MostToLeast(itemLowest, itemHighest, itemHigh);
                    }
                }
			}
		}
		
		/// <summary>
		/// Swap the most position to normalized least position - 1.0
		/// </summary>
		/// <param name="least">Least valued CarouselItem</param>
		/// <param name="most">Most valued CarouselItem</param>
		/// <param name="newMost">The new most valued CarouselItem</param>
		void MostToLeast(CarouselItem least, CarouselItem most, CarouselItem newMost)
		{
            most.UpdateScore(least.CalculateScore() - least.SizeBottom - least.Padding - most.SizeTop - most.Padding);
			
			if(OnMostToLeast != null)
				OnMostToLeast(most.gameObject, newMost.gameObject);
		}
		
		/// <summary>
		/// Swap the least position to normalized most position + 1.0
		/// </summary>
		/// <param name="least">Least valued CarouselItem</param>
		/// <param name="most">Most valued CarouselItem</param>
		/// <param name="newLeast">The new least valued CarouselItem</param>
		void LeastToMost(CarouselItem least, CarouselItem most, CarouselItem newLeast)
		{
            least.UpdateScore(most.CalculateScore() + most.SizeTop + most.Padding + least.SizeBottom + least.Padding);
			
			if(OnLeastToMost != null)
				OnLeastToMost(newLeast.gameObject, least.gameObject);
		}

        /// <summary>
        /// Returns the total size calculated by summing each CarouselItem size
        /// </summary>
        /// <returns></returns>
        public float Size()
        {
            float size = 0;
            for (int i = 0; i < Items.Length; i++)
            {
                size += Items[i].Padding*2.0f;
                size += Items[i].SizeTop;
                size += Items[i].SizeBottom;
            }
            return size;
        }
        
        /// <summary>
        /// Gets the upper and lower scores of the carousel
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        public void Bounds(ref float min, ref float max)
        {
            for (int i = 0; i < Items.Length; i++)
            {
                float bottom = Items[i].CalculateScore() - Items[i].Padding - Items[i].SizeBottom;
                float top = Items[i].CalculateScore() + Items[i].Padding + Items[i].SizeBottom;

                if(i == 0)
                {
                    min = bottom;
                    max = top;
                }
                else
                {
                    if (bottom < min)
                        min = bottom;
                    if (top > max)
                        max = top;
                }
            }
        }
	}
}