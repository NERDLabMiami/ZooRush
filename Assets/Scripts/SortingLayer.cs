using UnityEngine;
using System.Collections;


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
