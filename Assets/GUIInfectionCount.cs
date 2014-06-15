using UnityEngine;
using System.Collections;

public class GUIInfectionCount : MonoBehaviour
{ 
		private int infectionCount;
		public TextMesh textMesh;

		public GameObject[] infectionPrefabs;
		public Transform[] infectionPlacementMarkers;
		public TextMesh moreInfectionsIndicator;


		// Use this for initialization
		void Start ()
		{
				textMesh.text = "";
				moreInfectionsIndicator.text = "";

//				startInfectionDisplay (10, new int[]{1,4,5});
		}

		public void startInfectionDisplay (int infectionTally, int[] infectionScore)
		{
				infectionCount = infectionTally;
				if (infectionCount > 0) {
						textMesh.text = string.Format ("{0} Infections!", infectionCount);
						displayInfections (infectionScore);
						if (infectionCount > infectionPlacementMarkers.Length) {
								moreInfectionsIndicator.text = "...";
						}
				} else {
						textMesh.text = "No Infections!";
				}
		}

		private void displayInfections (int[] infectionScores)
		{
				int displayCount = 0;
				for (int i =0; i < infectionScores.Length; i++) {
						for (int j = infectionScores[i]; j > 0; j--) {
								if (displayCount < infectionPlacementMarkers.Length) {
										GameObject.Instantiate (infectionPrefabs [i], infectionPlacementMarkers [displayCount].position, Quaternion.identity);
										displayCount++;
								}
						}
				}
		}
		
		public void dismiss ()
		{
				textMesh.text = "";
				moreInfectionsIndicator.text = "";
		}

		
}
