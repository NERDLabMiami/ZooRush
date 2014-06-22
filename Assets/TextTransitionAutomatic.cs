using UnityEngine;
using System.Collections;

public class TextTransitionAutomatic : MonoBehaviour
{
		public TextMesh textMesh;
		private string[] textToDisplay;
		private int currentTextIndex;

		private string[] dialogText = {
		"Drink water to avoid\na sickle cell crisis",
		"Infections can only be\ncured by visiting the\nhospital",
		"Failed!\nIt's time to stop\nchasing this one, it's long\ngone by now.",
		"Chin up, you'll probably\ncatch it next time!"
	};

		void Start ()
		{
				currentTextIndex = -1;
				textToDisplay = new string[2];
		}

		public void activate ()
		{
				switch (GUILevelFailedMenuController.failReason) {
				case "Fainted":
						textToDisplay [0] = dialogText [0];
						break;
				case "Infected":
						textToDisplay [0] = dialogText [1];
						break;
				case "TimeOut":
						textToDisplay [0] = dialogText [2];
						break;
				default:
						textToDisplay [0] = dialogText [3];
						break;
				}
				textToDisplay [1] = "With sickle cell disease,\npain is the most common\nreason for emergency\nroom and hospital visits.";
				next ();
				StartCoroutine ("waitForNext");

		}
		private IEnumerator waitForNext ()
		{
				yield return new WaitForSeconds (5f);
				next ();
		}

		private void next ()
		{
				currentTextIndex++;
				if (currentTextIndex < textToDisplay.Length) {
						textMesh.text = textToDisplay [currentTextIndex];
						StartCoroutine ("waitForNext");
				}
		}
}
