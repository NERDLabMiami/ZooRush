using UnityEngine;
using System.Collections;

public class EndlessModeEndMenu : MonoBehaviour
{
	public EndlessSceneManager sceneManager;
	private bool[] animalCountDisplayed = new bool[(int)EndlessSceneManager.AnimalValues.COUNT];
	public GameObject[] animalIcons; //Animal Icons must be entered in the order of EndlessSceneManager.AnimalValues
	public GameObject countTextObject;
	public TextMesh countText;
	public TextMesh totalCountText;
	private int currentTotalCount;
	private int currentHighScore;
	public TextMesh endTitleText;
	public TextMesh newHighScore;
	private Vector2 countTextStartPosition = new Vector2 (-8.84565f, -4.840044f);
	private Vector2 localIconWaitPosition = new Vector2 (-4, -4);
	private Vector2 localIconDisappearPosition = new Vector2 (11, -4);
	private Vector2 localCountWaitPosition = new Vector2 (-2.529923f, -4.840044f);
	private Vector2 localCountAddPosition = new Vector2 (3.5f, -3);
	private Vector2 localCountDisappearPosition = new Vector2 (10, -4.840044f);
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
		currentAnimal = 0;
		currentTotalCount = 0;
		totalCountText.text = "0";
		newHighScore.renderer.enabled = false;

		if (PlayerPrefs.HasKey (Application.loadedLevelName)) {
			currentHighScore = PlayerPrefs.GetInt (Application.loadedLevelName);
		} else {
			currentHighScore = 0;
			PlayerPrefs.SetInt (Application.loadedLevelName, currentHighScore);
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
			endTitleText.text = string.Format ("The {0} got away!", AnimalNames [(int)sceneManager.currentAnimal]);
			if (sceneManager.totalCaughtCount > currentHighScore) {
				PlayerPrefs.SetInt (Application.loadedLevelName, sceneManager.totalCaughtCount);
			}
		}

		if (sceneManager.animalCaughtCount [currentAnimal] > 0) {
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
		totalCountText.text = string.Format ("{0:N0}", currentTotalCount);
		StartCoroutine (moveAnimalIconToDisappearPosition (animalIcons [currentAnimal]));
		StartCoroutine (moveCountTextToDisappearPosition ());

	}
}
