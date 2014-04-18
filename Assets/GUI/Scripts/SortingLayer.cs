using UnityEngine;
using System.Collections;

/** Handles sortling Layer values for non sprite renderers.
 * @author: Ebtissam Wahman
 */ 
public class SortingLayer : MonoBehaviour
{
	public string sortingLayerName;
	public int sortingOrder;
	public bool matchParentSprite;

	void Start ()
	{
		if (matchParentSprite) {
			renderer.sortingLayerName = transform.parent.gameObject.renderer.sortingLayerName;
			renderer.sortingOrder = transform.parent.gameObject.renderer.sortingOrder + 1;
		} else {
			renderer.sortingLayerName = sortingLayerName;
			renderer.sortingOrder = sortingOrder;
		}
	}

	void FixedUpdate ()
	{
		if (matchParentSprite) {
			renderer.sortingLayerName = transform.parent.gameObject.renderer.sortingLayerName;
			renderer.sortingOrder = transform.parent.gameObject.renderer.sortingOrder + 1;
		}
	}
}
