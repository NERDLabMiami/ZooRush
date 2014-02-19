using UnityEngine;
using System.Collections;

public class AnimatedText : MonoBehaviour
{
	private string[] fullText;
	private TextMesh[] textMeshes;
	private int[] lengthShown;

	private bool clear;
	public bool complete;
	private bool start;

	void Awake ()
	{
		textMeshes = new TextMesh[3];
		textMeshes [0] = GameObject.Find ("Dialog Text - Line 1").GetComponent<TextMesh> ();
		textMeshes [1] = GameObject.Find ("Dialog Text - Line 2").GetComponent<TextMesh> ();
		textMeshes [2] = GameObject.Find ("Dialog Text - Line 3").GetComponent<TextMesh> ();
	
		foreach (TextMesh mesh in textMeshes) {
			mesh.text = "";
		}

		lengthShown = new int[] {0,0,0};

		clear = true;
		complete = false;
		start = false;
	} 

	void FixedUpdate ()
	{
		if (!complete && start) {
			if (!textMeshes [0].text.Equals (fullText [0]) && clear) {
				StartCoroutine (Animate (0));
			} else {
				if (!textMeshes [1].text.Equals (fullText [1]) && clear) {
					StartCoroutine (Animate (1));
				} else {
					if (!textMeshes [2].text.Equals (fullText [2]) && clear) {
						StartCoroutine (Animate (2));
					} else {
						if (textMeshes [0].text.Equals (fullText [0]) && textMeshes [1].text.Equals (fullText [1]) && textMeshes [2].text.Equals (fullText [2])) {
							complete = true;
							GameObject.FindObjectOfType<StoryModeHandler> ().stopSpeechSprites ();
							start = false;
						}
					}
				}
			}
		}
	}

	public void DisplayText (string[] textArray)
	{
		foreach (TextMesh mesh in textMeshes) {
			mesh.text = "";
		}
		fullText = textArray;
		lengthShown = new int[] {0,0,0};
		complete = false;
		start = true;
	}


	private IEnumerator Animate (int lineNumber)
	{
		clear = false;
		while (lengthShown [lineNumber] < fullText [lineNumber].Length) {
			textMeshes [lineNumber].text += fullText [lineNumber] [lengthShown [lineNumber]++];
			yield return new WaitForSeconds (0.05f);
		}
//
//		yield return new WaitForSeconds (0.05f);
//		textMeshes [lineNumber].text = fullText [lineNumber].Substring (0, lengthShown [lineNumber]);
//		if (lengthShown [lineNumber] < fullText [lineNumber].Length) {
//			lengthShown [lineNumber]++;
//		}
		clear = true;

	}
}
