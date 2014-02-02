using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(SortingLayer))]
public class SortingLayerEditor : Editor
{
	private SortingLayer thisSort;
	private int selection;
	private string[] sortingLayers = {
		"Default",
		"Boundary",
		"Background",
		"Buildings",
		"Play Area",
		"DistanceIndicator",
		"GUI Content",
		"GUI Outline",
		"GUI Text Overlay"
	};
		
	
	public override void OnInspectorGUI ()
	{
		if (thisSort == null) {
			thisSort = target as SortingLayer;
		}

		EditorGUILayout.BeginHorizontal ();
		{
			EditorGUILayout.LabelField ("Sorting Layer");
			for (int i = 0; i < sortingLayers.Length; i++) {
				if (thisSort.renderer.sortingLayerName.Equals (sortingLayers [i])) {
					selection = i;
				}
			}
			selection = EditorGUILayout.Popup (selection, sortingLayers);
			thisSort.renderer.sortingLayerName = sortingLayers [selection];
			thisSort.sortingLayerName = sortingLayers [selection];
		}
		EditorGUILayout.EndHorizontal ();
		
		EditorGUILayout.BeginHorizontal ();
		{
			EditorGUILayout.LabelField ("Order in Layer: ");
			thisSort.renderer.sortingOrder = EditorGUILayout.IntField (thisSort.renderer.sortingOrder);
			thisSort.sortingOrder = thisSort.renderer.sortingOrder;
		}
		EditorGUILayout.EndHorizontal ();
		Repaint ();
	}
}
