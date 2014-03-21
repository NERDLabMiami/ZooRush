using UnityEngine;
using System.Collections;

public class AnimatedText : MonoBehaviour
{
//	private string[] fullText;
//	private TextMesh[] textMeshes;
//
//	private bool clear;
//	public bool complete;
//	private bool start;
//
//	void Awake ()
//	{
//		textMeshes = new TextMesh[3];
//		textMeshes [0] = GameObject.Find ("Dialog Text - Line 1").GetComponent<TextMesh> ();
//		textMeshes [1] = GameObject.Find ("Dialog Text - Line 2").GetComponent<TextMesh> ();
//		textMeshes [2] = GameObject.Find ("Dialog Text - Line 3").GetComponent<TextMesh> ();
//	
//		foreach (TextMesh mesh in textMeshes) {
//			mesh.text = "";
//		}
//
//		clear = true;
//		complete = false;
//		start = false;
//	} 
//
//	void FixedUpdate ()
//	{
//		if (!complete && start) {
//			if (clear) {
//				StartCoroutine (Animate ());
//			}
//		}
//	}
//	
//
//	public void DisplayText (string[] textArray)
//	{
//		foreach (TextMesh mesh in textMeshes) {
//			mesh.text = "";
//		}
//		fullText = textArray;
//		complete = false;
//		start = true;
//	}
//
//
//	private IEnumerator Animate ()
//	{
//		clear = false;
//		int index = 0;
//		while (index < 3) {
//			for (int i = 0; i < fullText [index].Length; i++) {
//				textMeshes [index].text = fullText [index].Substring (0, i + 1);
//				yield return new WaitForSeconds (0.05f);
//			}
//			index++;
//		}
//		complete = true;
//		clear = true;
//	}
}
