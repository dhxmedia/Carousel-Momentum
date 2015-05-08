/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/. */
using UnityEngine;
using System.Collections;
namespace Carousel
{ 

    /// <summary>
    /// Class that takes into account items in a carousel move.
    /// Gives a maximum delta before it's not considered a click
    /// </summary>
    [RequireComponent(typeof(CarouselItem))]
    public class CarouselItemClick : MonoBehaviour {

        /// <summary>
        /// the maximum delta a score can be and still be considered a click
        /// </summary>
        public float MaxScoreDelta = 0.1f;

        /// <summary>
        /// The filtered on click event
        /// </summary>
        public System.Action OnClick;

        /// <summary>
        /// The previous recorded score in last update
        /// </summary>
        float _PrevScore = 0;

        /// <summary>
        /// The total delta accumulated while touching
        /// </summary>
        float _ScoreAccumulator = 0;

        /// <summary>
        /// Attached <see cref="CarouselItem"/> component
        /// </summary>
        CarouselItem _CarouselItem;

        /// <summary>
        /// Whether we're touched (controlled by outside script)
        /// </summary>
        bool _Touched = false;

	    // Use this for initialization
	    void Awake () {
            _CarouselItem = GetComponent<CarouselItem>();
	    }

	
	    // Update is called once per frame
	    void Update () {
            if (_Touched == true)
            {
                float currentScore = _CarouselItem.CalculateScore();
                float delta = Mathf.Abs(currentScore - _PrevScore);
                _ScoreAccumulator += delta;
                _PrevScore = currentScore;
            }
	    }

        /// <summary>
        /// Touch down function called by external UI controller
        /// </summary>
        public void OnTouchDown()
        {
            _Touched = true;
            _PrevScore = _CarouselItem.CalculateScore();
            _ScoreAccumulator = 0;
        }


        /// <summary>
        /// Touch up function called by external UI controller
        /// </summary>
        public void OnTouchUp(bool validClick)
        {
            _Touched = false;
            Debug.Log(_ScoreAccumulator + ":" + MaxScoreDelta);
            if (_ScoreAccumulator < MaxScoreDelta && validClick)
            {
                if(OnClick != null)
                {
                    OnClick();
                }
            }
        }
    }
}