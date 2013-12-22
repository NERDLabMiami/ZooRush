using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;

public class LevelEditor : EditorWindow
{
	private SceneManager sceneManager;
	private bool levelEditorEnable = false;
	private bool scoreVarFoldout = false;
	
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
		if (sceneManager == null) {
			sceneManager = GameObject.FindObjectOfType<SceneManager> ();
		}
	}

	void OnGUI ()
	{
		EditorGUILayout.BeginHorizontal ();
		{
			EditorGUILayout.LabelField ("Enable Editor");
			levelEditorEnable = EditorGUILayout.Toggle (levelEditorEnable);
		}
		EditorGUILayout.EndHorizontal ();
		if (levelEditorEnable) {
			if (sceneManager != null) {
				EditorGUILayout.Space ();
				EditorGUILayout.BeginVertical ();
				{
					EditorGUILayout.LabelField ("Scene Manager Properties for " + Application.loadedLevelName);
				
					sceneManager.NextSceneName = EditorGUILayout.TextField ("Next Scene Name:", sceneManager.NextSceneName);
					sceneManager.isEndless = EditorGUILayout.ToggleLeft ("Endless Mode", sceneManager.isEndless);
					sceneManager.tutEnabled = EditorGUILayout.ToggleLeft ("Tutorial Mode", sceneManager.tutEnabled);
					scoreVarFoldout = EditorGUILayout.Foldout (scoreVarFoldout, "Scoring Variables");
					
					if (scoreVarFoldout) {	
						sceneManager.targetTimeVar = EditorGUILayout.FloatField ("Target Time (3 stars):", sceneManager.targetTimeVar);	
						sceneManager.multiplier1 = EditorGUILayout.FloatField ("Time multiplier (2 stars):", sceneManager.multiplier1);
						sceneManager.multiplier2 = EditorGUILayout.FloatField ("Time multiplier (1 star):", sceneManager.multiplier2);
					}
				}
				EditorGUILayout.EndVertical ();
				EditorGUILayout.Space ();
			}
		}
		EditorGUILayout.BeginHorizontal ();
		{
			if (GUILayout.Button ("Reset Player Prefs")) {
				PlayerPrefs.DeleteAll ();
			}
		}
		EditorGUILayout.EndHorizontal ();
	}
	
	
	
}
