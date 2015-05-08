/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/. */

using UnityEngine;
using System.Collections;
namespace Piko.Misc
{
	public class Momentum : MonoBehaviour  {

		/// <summary>
		/// How much the delta is multiplied by at the start of momentum
		/// </summary>
		public readonly static float StartMultiplier = 1.0f;

		/// <summary>
		/// How much the delta is multiplied by at the end of momentum
		/// </summary>
		public readonly static float EndMultiplier = 0.0f;


		/// <summary>
		/// Slope of falloff
		/// </summary>
		public float Slope = -1.0f;

		/// <summary>
		/// Calculate the delta in momentum\n
		/// </summary>
		/// <returns>The momentum.</returns>
		/// <param name="progress">Progress.</param>
		/// <param name="initialDelta">Initial delta.</param>
		/// <param name="falloff">Falloff.</param>
		public static float DeltaMomentum(float tStart, float tEnd, float initialDelta, float slope)
		{
			// integral of y = slope*x + StartMultiplier
			float distance = slope*(Mathf.Pow(tEnd, 2))/2.0f + StartMultiplier*(tEnd) 
				- slope*(Mathf.Pow(tStart, 2))/2.0f - StartMultiplier*(tStart);

			return distance*initialDelta;
		}


		/// <summary>
		/// Event to update anyone listening (<see cref="_MomentumProgress"/> /<see cref="MomentumTime"/> , <see cref="_MomentumValue"/> )
		/// </summary>
		public System.Action<float, float> OnMomentumUpdate;

		public System.Action OnMomentumFinished;
		public System.Action<float> OnMomentumStart;

		/// <summary>
		/// Whether or not the momentum has been fired
		/// </summary>
		bool _Fired = false;
		
		/// <summary>
		/// Time that momentum is active for
		/// </summary>
		float _MomentumTime = 1.0f;
		
		/// <summary>
		/// The value of how much time has passed since firing momentum
		/// </summary>
		float _MomentumProgress = 0.0f;
		
		/// <summary>
		/// The previous <see cref="_MomentumProgress"/> value 
		/// </summary>
		float _PreviousMomentumProgress = 0.0f;

		/// <summary>
		/// The current momentum value (value of delta)
		/// </summary>
		float _MomentumValue = 0;

        public float Scale = 1.0f;
		
		/// <summary>
		/// If <see cref="_Fired"/> is true, use <see cref="_MomentumProgress"/> 
		/// and <see cref="Piko.Misc.Momentum.DeltaMomentum"/> to calculate 
		/// a momentum value to use. \n
		/// Broadcast using <see cref="OnMomentumUpdate"/> 
		/// </summary>
		void Update () {
			if(_Fired)
			{
				_MomentumProgress += Time.smoothDeltaTime;
				
				if(_MomentumProgress >= _MomentumTime)
				{
					_Fired = false;
					_MomentumProgress = _MomentumTime;
				}

				float distance = DeltaMomentum(_PreviousMomentumProgress, _MomentumProgress, _MomentumValue, Slope);
                if (Mathf.Approximately(distance * Scale, 0.0f) )
                    _Fired = false;
				float t = _MomentumProgress/_MomentumTime;

				if(OnMomentumUpdate != null)
                    OnMomentumUpdate(t, distance * Scale);

                if (_Fired == false)
                {
                    if (OnMomentumFinished != null)
                        OnMomentumFinished();
                }

				_PreviousMomentumProgress = _MomentumProgress;
			}
		}

		/// <summary>
		/// Start firing momentum in Update
		/// </summary>
		/// <param name="delta">Delta.</param>
		public void Fire(float delta)
		{
			_Fired = true;
			_PreviousMomentumProgress = 0;
			_MomentumProgress = 0;
			_MomentumValue = delta;
			_MomentumTime = -StartMultiplier/Slope;
			if(OnMomentumStart != null)
				OnMomentumStart(delta);
		}


        /// <summary>
        /// Immediately stop momentuming
        /// </summary>
        public void Stop()
        {
            _Fired = false;
            if (OnMomentumFinished != null)
                OnMomentumFinished();
        }

	}
}