using UnityEngine;
using System.Collections;

public class GUITimeObject : MonoBehaviour
{

		public TextMesh textMesh;
		private int countUpToTime;
	
		private bool go = false;
		public bool finished = false;
	
		void Start ()
		{
				if (!textMesh) {
						Debug.Log ("This timer requires a GUIText component");
						enabled = false;
						return;
				}
				textMesh.text = "";
		}
	
	
		void FixedUpdate ()
		{
		
				//This is for testing, make sure to remove it after your done.
				if (Input.GetMouseButtonDown (0)) {
						if (go && !finished) {
								StopTimer ();
						}
				}
		
		}

		IEnumerator countUp ()
		{
				int currentTimeCount = 0;
				int minutes = 0;
				int seconds = 0;
        
				while (go && currentTimeCount < countUpToTime) {
						audio.Play ();
						currentTimeCount++;
						minutes = currentTimeCount / 60;
						seconds = currentTimeCount % 60;
						textMesh.text = string.Format ("{0:00}:{1:00}", minutes, seconds);
						yield return new WaitForSeconds (0.1f);
				}
				
				finished = true;
		}
		
		public void StartTimer (int timeLimit)
		{
				countUpToTime = timeLimit;
				go = true;
				StartCoroutine ("countUp");
		}
	
		public void StopTimer ()
		{
				StopCoroutine ("countUp");
				StartCoroutine ("displayFinalTime");
		}

		IEnumerator displayFinalTime ()
		{
				go = false;
				finished = true;
				yield return new WaitForSeconds (0.1f);		
				int minutes = countUpToTime / 60;
				int seconds = countUpToTime % 60;
				textMesh.text = string.Format ("{0:00}:{1:00}", minutes, seconds);
				
		}

		public void dismiss ()
		{
				textMesh.text = "";
		}

}
