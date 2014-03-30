using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor (typeof(Button),true)]
[CanEditMultipleObjects]

public class ButtonEditor : Editor
{
	private Button thisButton;
	private int currentButtonType;
	private string sceneName;
	private string otherType;
	private OtherButtonClass otherButtonLink;

	private string[] buttonTypes = {
		"Direct To Scene By Name",
		"Direct To Next Scene",
		"Resume",
		"Retry",
		"Other"
	};

	void OnEnable ()
	{
		thisButton = target as Button;
		currentButtonType = thisButton.buttonType;
		sceneName = thisButton.SceneName;
	}

	public override void OnInspectorGUI ()
	{
		base.OnInspectorGUI ();

		EditorGUILayout.BeginHorizontal ();
		
		EditorGUI.BeginChangeCheck ();
		
		EditorGUILayout.LabelField ("Button Type: ");
		
		currentButtonType = EditorGUILayout.Popup (currentButtonType, buttonTypes);

		if (currentButtonType == 0) {
			EditorGUILayout.EndHorizontal ();
			EditorGUILayout.BeginHorizontal ();
			EditorGUILayout.LabelField ("Direct To Scene Name: ");
			sceneName = EditorGUILayout.TextField (thisButton.SceneName);
		}

		if (currentButtonType == 4) {
			EditorGUILayout.EndHorizontal ();
			EditorGUILayout.BeginHorizontal ();
			EditorGUILayout.LabelField ("Other Button Class Link: ");
			otherButtonLink = EditorGUILayout.ObjectField (thisButton.otherButtonClass, typeof(OtherButtonClass), true) as OtherButtonClass;
		}
		
		if (EditorGUI.EndChangeCheck ()) {
			thisButton.buttonType = currentButtonType;
			if (currentButtonType == 0) {
				thisButton.SceneName = sceneName;
			}
			if (currentButtonType == 4) {
				thisButton.otherButtonClass = otherButtonLink;
			}
			EditorUtility.SetDirty (thisButton);
		}
		
		EditorGUILayout.EndHorizontal ();

	}

}
