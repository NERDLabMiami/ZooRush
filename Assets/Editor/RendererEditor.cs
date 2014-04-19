using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System;
using System.Reflection;
using System.Collections;

[CustomEditor (typeof(MeshRenderer))]
[CanEditMultipleObjects]

/**
* Exposes the Sorting Layer Name and Sorting Layer Order properties of Mesh Renderer Components in the inspector.
*
*/
public class RendererEditor : Editor
{
		MeshRenderer thisRenderer;	
		string[] sortingLayerNames;
		int currentLayer;
		int order;
	
	
		void OnEnable ()
		{
				thisRenderer = target as MeshRenderer;
				sortingLayerNames = GetSortingLayerNames ();
				for (int i = 0; i < sortingLayerNames.Length; i++) {
						if (thisRenderer.sortingLayerName.Equals (sortingLayerNames [i])) {
								currentLayer = i;
								break;
						}
				}
				order = thisRenderer.sortingOrder;
		}
		
		public override void OnInspectorGUI ()
		{
				base.OnInspectorGUI ();
				
				EditorGUILayout.BeginHorizontal ();
		
				EditorGUI.BeginChangeCheck ();

				EditorGUILayout.LabelField ("Sorting Layer Name");

				currentLayer = EditorGUILayout.Popup (currentLayer, sortingLayerNames);
				
				
				if (EditorGUI.EndChangeCheck ()) {
						thisRenderer.sortingLayerName = sortingLayerNames [currentLayer];
				}
		
				EditorGUILayout.EndHorizontal ();
		
				EditorGUILayout.BeginHorizontal ();
		
				EditorGUI.BeginChangeCheck ();
		
				order = EditorGUILayout.IntField ("Sorting Order", thisRenderer.sortingOrder);
		
				if (EditorGUI.EndChangeCheck ()) {
						thisRenderer.sortingOrder = order;
				}
		
				EditorGUILayout.EndHorizontal ();
		}
	
		public string[] GetSortingLayerNames ()
		{
		
				Type internalEditorUtilityType = typeof(InternalEditorUtility);
		
				PropertyInfo sortingLayersProperty = internalEditorUtilityType.GetProperty ("sortingLayerNames", BindingFlags.Static | BindingFlags.NonPublic);
		
				return (string[])sortingLayersProperty.GetValue (null, new object[0]);
		
		}
}
