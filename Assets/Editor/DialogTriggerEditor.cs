using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(DialogTrigger))]
public class DialogTriggerEditor : Editor
{
	private DialogTrigger thisDialog;
	private int characterLimit = 12;
	public string[] textDisplay;

	public override void OnInspectorGUI ()
	{
		DrawDefaultInspector ();
		
		thisDialog = target as DialogTrigger;
		
		for (int i = 0; i < thisDialog.textDisplay.Length; i++) {
			if (thisDialog.textDisplay [i].Length > characterLimit) {
				thisDialog.textDisplay [i] = thisDialog.textDisplay [i].Substring (0, characterLimit);
			}
		}
		
		TextMesh[] texts = thisDialog.gameObject.GetComponentsInChildren<TextMesh> ();
		foreach (TextMesh text in texts) {
			if (text.name.Contains ("Line 1")) {
				text.text = thisDialog.textDisplay [0];
			}
		}
		
	}
}
