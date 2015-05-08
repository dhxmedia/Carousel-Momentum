using UnityEngine;
using System.Collections;
namespace Carousel
{ 
    public class CarouselCapMinMax : MonoBehaviour {
        Carousel _Carousel;
        CarouselIndex _CarouselIndex;
        public int CapMin = 0;
        public int CapMax = 0;

        void Awake()
        {
            _Carousel = GetComponent<Carousel>();
            _CarouselIndex = GetComponent<CarouselIndex>();

            _Carousel.OnLeastToMost += OnLeastToMost;
            _Carousel.OnMostToLeast += OnMostToLeast;

            _CarouselIndex.OnLeastToMostIndices += OnLeastToMostIndices;
            _CarouselIndex.OnMostToLeastIndices += OnMostToLeastIndices;
        }

	    // Use this for initialization
	    void Start () {
            Refresh();
            if (_CarouselIndex.LeastIndex <= CapMin)
                _Carousel.CapMin = true;

            if (_CarouselIndex.MostIndex >= (CapMax - 1))
                _Carousel.CapMax = true;
	    }


        /// <summary>
        /// Used when a carousel should be refreshed
        /// </summary>
        public void Refresh()
        {
            if (_CarouselIndex.LeastIndex <= CapMin)
                _Carousel.CapMin = true;
            else
                _Carousel.CapMin = false;

            if (_CarouselIndex.MostIndex >= (CapMax - 1))
                _Carousel.CapMax = true;
            else
                _Carousel.CapMax = false;
        }

        /// <summary>
        ///  Listen to <see cref="_CarouselIndexInstance"/>. <see cref="Carousel.CarouselIndex.OnLeastToMost"/> \n  
        /// Replace the clips in most
        /// </summary>
        /// <param name="least">Least valued GameObject</param>
        /// <param name="most">Most valued GameObject</param>
        /// <param name="leastIndex">Least index.</param>
        /// <param name="mostIndex">Most index.</param>
        void OnLeastToMostIndices(GameObject least, GameObject most, int leastIndex, int mostIndex)
        {
            if (mostIndex >= (CapMax - 1))
                _Carousel.CapMax = true;
        }


        /// <summary>
        /// Listen to <see cref="_CarouselIndexInstance"/>. <see cref="Carousel.CarouselIndex.OnMostToLeast"/> \n
        /// Replace the clips in least
        /// </summary>
        /// <param name="least">Least valued GameObject</param>
        /// <param name="most">Most valued GameObject</param>
        /// <param name="leastIndex">Least index.</param>
        /// <param name="mostIndex">Most index.</param>
        void OnMostToLeastIndices(GameObject least, GameObject most, int leastIndex, int mostIndex)
        {
            if (leastIndex <= CapMin)
                _Carousel.CapMin = true;
        }


        /// <summary>
        /// Listen to <see cref="_CarouselInstance"/>.<see cref="Carousel.OnLeastToMost">OnLeastToMost</see>
        /// Increment the LeastIndex and the MostIndex
        /// </summary>
        /// <param name="least">Least.</param>
        /// <param name="most">Most.</param>
        void OnLeastToMost(GameObject least, GameObject most)
        {
            _Carousel.CapMin = false;
        }

        /// <summary>
        /// Listen to <see cref="_CarouselInstance"/>.<see cref="Carousel.OnMostToLeast">OnMostToLeast</see> 
        /// Decrement the LeastIndex and the MostIndex
        /// </summary>
        /// <param name="least">Least.</param>
        /// <param name="most">Most.</param>
        void OnMostToLeast(GameObject least, GameObject most)
        {
            _Carousel.CapMax = false;
        }
    }
}