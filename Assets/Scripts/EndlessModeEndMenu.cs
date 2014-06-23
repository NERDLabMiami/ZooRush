using UnityEngine;
using System.Collections;

public class EndlessModeEndMenu : MonoBehaviour
{
		public EndlessSceneManager sceneManager;

		public GameObject countTextObject;
		public TextMesh countText;
		public TextMesh totalCountText;
		private int currentTotalCount;
		private int currentHighScore;
		public TextMesh endTitleText;
		public TextMesh newHighScore;
		private int currentAnimal;

		public string[] AnimalNames =
	{
		"Bear",
		"Cheetah",
		"Crocodile",
		"Elephant",
		"Flamingo",
		"Gorilla",
		"Ostrich",
		"Penguin",
		"Rhino",
		"Tortoise"
	};

		void Start ()
		{
				//Set Starting Values
				currentAnimal = 0;
				currentTotalCount = 0;
				totalCountText.text = "0";
				newHighScore.renderer.enabled = false;

				if (PlayerPrefs.HasKey (Application.loadedLevelName + "Score")) {
						currentHighScore = PlayerPrefs.GetInt (Application.loadedLevelName + "Score");
				} else {
						currentHighScore = 0;
						PlayerPrefs.SetInt (Application.loadedLevelName + "Score", currentHighScore);
				}
		}
	
		void Update ()
		{
				if (currentAnimal < 9 && animalCountDisplayed [currentAnimal]) {
						currentAnimal++;
						launchStat ();
				}
		}

		private void displayNewHighScore ()
		{
				newHighScore.renderer.enabled = true;
		}

		public void launchStat ()
		{
				Debug.Log ("Launch Stat Called");
				
				if (currentAnimal == 0) {
						GameScoresManager.instance.submitEndlessLevelScore (sceneManager.totalCaughtCount, Application.loadedLevelName);
						
						if (sceneManager.totalCaughtCount > currentHighScore) {
								PlayerPrefs.SetInt (Application.loadedLevelName + "Score", sceneManager.totalCaughtCount);
						}
						if (!sceneManager.fainted) { 
								endTitleText.text = string.Format ("The {0} got away!", AnimalNames [(int)sceneManager.currentAnimal]);
						}
				}

				if (sceneManager.animalCaughtCount [currentAnimal] >= 0) {
						StartCoroutine (moveAnimalIconToWaitPosition (animalIcons [currentAnimal]));

						countText.text = "+" + sceneManager.animalCaughtCount [currentAnimal];
						countTextObject.transform.localPosition = countTextStartPosition;
						StartCoroutine (moveCountTextToWaitPosition ());
				} else {
						animalCountDisplayed [currentAnimal] = true;
				}
		}


		private IEnumerator moveAnimalIconToWaitPosition (GameObject animalIcon)
		{
				while (animalIcon.transform.localPosition.x < localIconWaitPosition.x - 0.01) {
						animalIcon.transform.localPosition = Vector2.Lerp (animalIcon.transform.localPosition, localIconWaitPosition, Time.deltaTime * 10);
						yield return new WaitForFixedUpdate ();
				}
		}

		private IEnumerator moveAnimalIconToDisappearPosition (GameObject animalIcon)
		{
				while (animalIcon.transform.localPosition.x < localIconDisappearPosition.x - 0.01) {
						animalIcon.transform.localPosition = Vector2.Lerp (animalIcon.transform.localPosition, localIconDisappearPosition, Time.deltaTime * 10);
						yield return new WaitForFixedUpdate ();
				}
				animalCountDisplayed [currentAnimal] = true;
				if (currentAnimal == 9) {
						if (sceneManager.totalCaughtCount > currentHighScore) {
								displayNewHighScore ();
						}
				}
		}

		private IEnumerator moveCountTextToWaitPosition ()
		{
				while (countTextObject.transform.localPosition.x < localCountWaitPosition.x - 0.01) {
						countTextObject.transform.localPosition = Vector2.Lerp (countTextObject.transform.localPosition, localCountWaitPosition, Time.deltaTime * 10);
						yield return new WaitForFixedUpdate ();
				}

				StartCoroutine (updateTotalCount ());
		}

		private IEnumerator moveCountTextToDisappearPosition ()
		{
				while (countTextObject.transform.localPosition.x < localCountDisappearPosition.x - 0.01) {
						countTextObject.transform.localPosition = Vector2.Lerp (countTextObject.transform.localPosition, localCountDisappearPosition, Time.deltaTime * 10);
						yield return new WaitForFixedUpdate ();
				}


		}

		private IEnumerator updateTotalCount ()
		{
				Debug.Log ("Update Total Count Called");
				while (countTextObject.transform.localPosition.x < localCountAddPosition.x - 0.01) {
						countTextObject.transform.localPosition = Vector2.Lerp (countTextObject.transform.localPosition, localCountAddPosition, Time.deltaTime * 10);
						yield return new WaitForFixedUpdate ();
				}
				currentTotalCount += sceneManager.animalCaughtCount [currentAnimal];
				totalCountText.text = string.Format ("{0:N0} Animals Counts", currentTotalCount);

		}
}
