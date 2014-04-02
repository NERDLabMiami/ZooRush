using UnityEngine;
using System.Collections;

/** Keeps track of the current Level's score.
 * @author Ebtissam Wahman
 */ 
public class ScoreKeeper : MonoBehaviour
{
	private float timeElapsed;
	private int redInfectionCount;
	private int yellowInfectionCount;
	private int greenInfectionCount;
	private int animalCount;

	private int doctorVisits;

	private int waterBottleCount;
	private int pillCount;

	private SceneManager sceneManager;
	private GameObject timeDisplay;

	void Start ()
	{
		sceneManager = GameObject.FindObjectOfType<SceneManager> ();
		timeDisplay = GameObject.Find ("GUI - Time");
		redInfectionCount = 0;
		yellowInfectionCount = 0;
		greenInfectionCount = 0;
		
		waterBottleCount = 0;
		pillCount = 0;
	}

	void Update ()
	{
		if (GameState.checkForState (GameState.States.Play)) {
			timeElapsed += Time.deltaTime;
		}
	}

	public void addToCount (string obj)
	{
		switch (obj) {
		case "Red":
			redInfectionCount += 1;
			break;
		case "Yellow":
			yellowInfectionCount += 1;
			break;
		case "Green":
			greenInfectionCount += 1;
			break;
		case "Water Bottle":
			waterBottleCount += 1;
			break;
		case "Pill":
			pillCount += 1;
			break;
		case "Animal":
			animalCount += 1;
			break;
		default:
			break;
		}
	}

	public int[] getScore ()
	{
		int[] score = new int[] {
			redInfectionCount,
			yellowInfectionCount,
			greenInfectionCount,
			waterBottleCount,
			pillCount,
			(int)timeElapsed,
			starsCalc (),
			animalCount
		};
		return score;
	}

	private void displayTime ()
	{
		string timeText = "00:00";
		if (timeElapsed >= 1f) {
			int minutes = ((int)(timeElapsed)) / 60;
			int seconds = ((int)(timeElapsed)) % 60;

			if (minutes % 100 <= 9 && minutes <= 99) {
				timeText = "0" + minutes;
			} else {
				if (minutes <= 99) {
					timeText = "" + minutes;
				} else {
					timeText = "99";
				}
			}

			timeText += ":";

			if (seconds % 100 <= 9 && minutes <= 100f) {
				timeText += "0" + seconds;
			} else {
				if (minutes <= 100f) {
					timeText += "" + seconds;
				} else {
					timeText += "59+";
				}
			}
		}
		timeDisplay.GetComponent<TextMesh> ().text = timeText;
	}
	
	private int starsCalc ()
	{
		int stars = 3;
		if (timeElapsed - sceneManager.targetTimeVar > 0) {
			stars--;
		}
		if (timeElapsed - (sceneManager.multiplier1 * sceneManager.targetTimeVar) > 0) {
			stars--;
		}
		if (timeElapsed - (sceneManager.multiplier2 * sceneManager.targetTimeVar) > 0) {
			stars--;
		}
		if (stars > 0) {
			if (totalInfectionsTouched () > 0) {
				stars--;
			}
		}
		return stars;
	}

	public int totalInfectionsTouched ()
	{
		return redInfectionCount + yellowInfectionCount + greenInfectionCount;
	}

	public int totalPowerUpsTouched ()
	{
		return waterBottleCount + pillCount;
	}
}
