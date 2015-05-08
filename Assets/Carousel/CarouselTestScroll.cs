/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/. */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Carousel.CarouselIndex))]
public class CarouselTestScroll : MonoBehaviour
{
	public KeyCode LowKey, HighKey;
	public Color[] ContentList;

	Carousel.Carousel _carousel;
	Carousel.CarouselIndex _index;

	private void Awake()
	{
		_carousel = GetComponent<Carousel.Carousel>();
		_index = GetComponent<Carousel.CarouselIndex>();
		
		_index.OnObjectInitialized += NewContent;
		_index.OnLeastToMostIndices += (_, most, __, highI) => NewContent(most, highI);
		_index.OnMostToLeastIndices += (least, _, lowI, __) => NewContent(least, lowI);
	}

	private void Update()
	{
		if(Input.anyKey)
		{
			if(Input.GetKey(LowKey))
				foreach(var c in _carousel.Items)
					c.UpdateScore(c.CalculateScore() - 0.02f);
			else if(Input.GetKey(HighKey))
				foreach(var c in _carousel.Items)
					c.UpdateScore(c.CalculateScore() + 0.02f);
		}

	}

	private void NewContent(GameObject newContainer, int newI)
	{
		//newContainer.GetComponent<tk2dSprite>().color = ContentList[Carousel.CarouselIndex.RealIndex(newI, ContentList.Length)];
	}
}
