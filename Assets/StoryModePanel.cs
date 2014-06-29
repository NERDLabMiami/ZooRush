using UnityEngine;
using System;
using System.Collections;

public class StoryModePanel : MonoBehaviour
{

	public GameObject[] sequence;
	public TextAsset sceneTextSource;
	public TextMesh[] textMeshes;

	private string[] sceneText;
	private int currentTextIndex = 0;
	private int currentPanelIndex = 0;
	private bool activated = false;


	private void prepareText ()
	{
		sceneText = sceneTextSource.text.Split ("\n" [0]);
	}

	private void moveIntoView ()
	{
		if (activated && currentPanelIndex + 1 < sequence.Length) { //move current panel out of the way and make way for the next panel
			sequence [currentPanelIndex].transform.parent = transform;
			sequence [currentPanelIndex].transform.localPosition = Vector3.zero;
			currentPanelIndex++;
		}
		if (currentPanelIndex < sequence.Length) {
			sequence [currentPanelIndex].transform.parent = Camera.main.transform;
			sequence [currentPanelIndex].transform.localPosition = Vector3.zero;
		}
	}

	private void activate ()
	{
		prepareText ();
		moveIntoView ();
		activated = true;
	}

	public bool next ()
	{
		if (!activated) {
			activate ();
		} else {
			moveIntoView ();
		}

		if (currentTextIndex < sceneText.Length) {
			foreach (TextMesh tMesh in textMeshes) {
				tMesh.text = "";
			}
			for (int numberOfLines = Int32.Parse (sceneText [currentTextIndex]), i = 0; numberOfLines > 0; numberOfLines--, i++) {
				currentTextIndex++;
				textMeshes [i].text = sceneText [currentTextIndex];
			}
			currentTextIndex++;
			return true;
		}
		return false;
	}
}
