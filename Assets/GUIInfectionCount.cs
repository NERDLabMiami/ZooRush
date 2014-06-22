using UnityEngine;
using System.Collections;

public class GUIInfectionCount : MonoBehaviour
{ 
		private int infectionCount;
		public TextMesh textMesh;

		public GameObject[] infectionPrefabs;
		public Transform[] infectionPlacementMarkers;
		public TextMesh moreInfectionsIndicator;
		
		public bool finished = false;

		public void startInfectionDisplay (int infectionTally, int[] infectionScore)
		{
				textMesh.text = "";
				moreInfectionsIndicator.text = "";
				infectionCount = infectionTally;
				if (infectionCount > 0) {
						textMesh.text = string.Format ("{0} Infections!", infectionCount);
						displayInfections (infectionScore);
						if (infectionCount > infectionPlacementMarkers.Length) {
								moreInfectionsIndicator.text = "...";
						}
				} else {
						textMesh.text = "No Infections!";
						StartCoroutine ("declareFinished");
				}
		}

		private void displayInfections (int[] infectionScores)
		{
				int displayCount = 0;
				for (int i =0; i < infectionScores.Length; i++) {
						for (int j = infectionScores[i]; j > 0; j--) {
								if (displayCount < infectionPlacementMarkers.Length) {
										GameObject infectionInstance = GameObject.Instantiate (infectionPrefabs [i], infectionPlacementMarkers [displayCount].position, Quaternion.identity) as GameObject;
										infectionInstance.transform.parent = transform;
										displayCount++;
								}
						}
				}
				StartCoroutine ("declareFinished");
		}
		
		public void dismiss ()
		{
				textMesh.text = "";
				moreInfectionsIndicator.text = "";
		}

		private IEnumerator declareFinished ()
		{
				yield return new WaitForSeconds (2.5f);
				finished = true;
		}

		
}
