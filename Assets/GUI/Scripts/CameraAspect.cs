﻿using UnityEngine;
using System.Collections;

/**  Adjusts the camera's viewport according to the game window's current size 
 * and the desired aspect ratio. 
 * @author Adrian Lopez 
 * Code from: http://gamedesigntheory.blogspot.ie/2010/09/controlling-aspect-ratio-in-unity.html
 * 
 */ 
public class CameraAspect : MonoBehaviour
{
	public bool heightOnly;

	void Start ()
	{
		// set the desired aspect ratio (the values in this example are
		// hard-coded for 16:9, but you could make them into public
		// variables instead so you can set them at design time)
		float targetaspect = 16.0f / 9.0f;
		
		// determine the game window's current aspect ratio
		float windowaspect = (float)Screen.width / (float)Screen.height;
		
		// current viewport height should be scaled by this amount
		float scaleheight = windowaspect / targetaspect;
		
		// obtain camera component so we can modify its viewport
		Camera camera = GetComponent<Camera> ();
		
		// if scaled height is less than current height, add letterbox
		if (scaleheight < 1.0f) {  
			Rect rect = camera.rect;

			if (!heightOnly) {
				rect.width = 1.0f;
				rect.x = 0;
			}

			rect.height = scaleheight;
			rect.y = (1.0f - scaleheight) / 2.0f;
			
			camera.rect = rect;
		} else { // add pillarbox
			float scalewidth = 1.0f / scaleheight;
			
			Rect rect = camera.rect;

			if (!heightOnly) {
				rect.width = scalewidth;
				rect.x = (1.0f - scalewidth) / 2.0f;
			}

			rect.height = 1.0f;
			rect.y = 0;
			
			camera.rect = rect;
		}
	}

}
