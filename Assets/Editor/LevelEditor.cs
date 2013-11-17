using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;

public class LevelEditor : EditorWindow
{
	bool levelEditorEnable = false;
	
	[MenuItem("Zoo Rush Tools/Level Editor %l")]
	private static void showEditor ()
	{
		EditorWindow.GetWindow<LevelEditor> (false, "Level Editor");
	}
	
	[MenuItem("Zoo Rush Tools/Level Editor %l", true)]
	private static bool showEditorValidator ()
	{ // Enables or disables the menu option
		return true;
	}
	
	void Update ()
	{
	
	}

	void OnGUI ()
	{
		EditorGUILayout.BeginHorizontal ();
		{
			EditorGUILayout.LabelField ("Enable Editor");
			levelEditorEnable = EditorGUILayout.Toggle (levelEditorEnable);
		}
		EditorGUILayout.EndHorizontal ();
	}
	
	
	
}
