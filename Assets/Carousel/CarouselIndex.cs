/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/. */
using UnityEngine;
using System.Collections;

namespace Carousel
{
	/// <summary>
	/// Manages the min/max indices and broadcasts on change
	/// </summary>
	[RequireComponent(typeof(Carousel))]
	public class CarouselIndex : MonoBehaviour {

		public int LeastIndex = 0;
		public int MostIndex = 0;
		Carousel _CarouselInstance;

		/// <summary>
		/// When the object is first initialized
		/// Broadcast the object, and its index
		/// </summary>
		public System.Action<GameObject, int> OnObjectInitialized;

		/// <summary>
		/// When the least position has moved to the most position
		/// Broadcast the most up to date least and most, and its indices
		/// </summary>
		public System.Action<GameObject, GameObject, int, int> OnLeastToMostIndices;

		/// <summary>
		/// When the least position has moved to the most position
		/// Broadcast the most up to date least and most, and its indices
		/// </summary>
		public System.Action<GameObject, GameObject, int, int> OnMostToLeastIndices;

		/// <summary>
		/// Initialize <see cref="_CarouselInstance"/> 
		/// </summary>
		void Init()
		{
			_CarouselInstance = GetComponent<Carousel>();
			_CarouselInstance.OnLeastToMost += OnLeastToMost;
			_CarouselInstance.OnMostToLeast += OnMostToLeast;
		}

		/// <summary>
		/// initialize LeastIndex and MostIndex
		/// </summary>
		/// <remarks>
		/// LeastIndex=0 MostIndex=_CarouselInstance.Items.Count - 1
		/// </remarks>
		void Awake ()
		{
			Init();
            Refresh();
		}

        /// <summary>
        /// Used when a carousel should be refreshed
        /// </summary>
        public void Refresh()
        {

            LeastIndex = 0;
            MostIndex = _CarouselInstance.Items.Length - 1;
        }

		/// <summary>
		/// Listen to <see cref="_CarouselInstance"/>.<see cref="Carousel.OnLeastToMost">OnLeastToMost</see>
		/// Increment the LeastIndex and the MostIndex
		/// </summary>
		/// <param name="least">Least.</param>
		/// <param name="most">Most.</param>
		void OnLeastToMost(GameObject least, GameObject most)
		{
			LeastIndex++;
			MostIndex++;

			if(OnLeastToMostIndices != null)
				OnLeastToMostIndices(least, most, LeastIndex, MostIndex);
		}
		
		/// <summary>
		/// Listen to <see cref="_CarouselInstance"/>.<see cref="Carousel.OnMostToLeast">OnMostToLeast</see> 
		/// Decrement the LeastIndex and the MostIndex
		/// </summary>
		/// <param name="least">Least.</param>
		/// <param name="most">Most.</param>
		void OnMostToLeast(GameObject least, GameObject most)
		{
			LeastIndex--;
			MostIndex--;

			if(OnMostToLeastIndices != null)
				OnMostToLeastIndices(least, most, LeastIndex, MostIndex);
		}

		/// <summary>
		/// Unsubscribe from <see cref="_CarouselInstance"/> events before the <see cref="Carousel.CarouselIndex"/>
		/// is reclaimed by garbage collection.
		/// </summary>
		void OnDestroy()
		{
			if(_CarouselInstance != null)
			{
				_CarouselInstance.OnLeastToMost -= OnLeastToMost;
				_CarouselInstance.OnMostToLeast -= OnMostToLeast;
			}
		}

		/// <summary>
		/// Translates a relative index (-IntMax to +IntMax) to a number between 0 and maxIndex
		/// </summary>
		/// <returns>The real index.</returns>
		/// <param name="relativeIndex">Relative index.</param>
		/// <param name="maxIndex">Max index.</param>
		public static int RealIndex(int relativeIndex, int maxIndex)
		{
            return (relativeIndex < 0) ? ((maxIndex - (Mathf.Abs(relativeIndex) % maxIndex)) % maxIndex) : relativeIndex % maxIndex;
		}
	}
}