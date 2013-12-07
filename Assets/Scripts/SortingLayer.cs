using UnityEngine;
using System.Collections;

/** Handles sortling Layer values for non sprite renderers.
 * @author: Ebtissam Wahman
 */ 
public class SortingLayer : MonoBehaviour
{
	public string sortingLayerName;
	public int sortingOrder;

	void Start ()
	{
		renderer.sortingLayerName = sortingLayerName;
		renderer.sortingOrder = sortingOrder;
	}
}
