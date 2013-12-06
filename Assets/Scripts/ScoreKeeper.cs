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

	private int doctorVisits;

	private int waterBottleCount;
	private bool pillUsed;

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
		pillUsed = false;
	}

	void Update ()
	{
		if (sceneManager.isPlaying && !sceneManager.levelStartWait) {
			timeElapsed += Time.deltaTime;
		}
		displayTime ();
	}

	public void addToCount (GameObject obj)
	{
		if (obj.name.Contains ("Infection")) {
			if (obj.name.Contains ("Red")) {
				redInfectionCount += 1;
			} else {
				if (obj.name.Contains ("Yellow")) {
					yellowInfectionCount += 1;
				} else { // infection is green
					greenInfectionCount += 1;
				}
			}
			
		} else {
			if (obj.name.Contains ("Power Up")) {
				if (obj.name.Contains ("Water Bottle")) {
					waterBottleCount += 1;
				} else {
					pillUsed = true;
				}
			}
		}
	}

	public bool pillBottleUsed ()
	{
		return pillUsed;
	}

	public int[] getScore ()
	{
		int[] score = new int[] {
			redInfectionCount,
			yellowInfectionCount,
			greenInfectionCount,
			waterBottleCount,
			(pillUsed) ? 1 : 0,
			(int)timeElapsed,
			starsCalc ()
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
			int totalInfections = redInfectionCount + yellowInfectionCount + greenInfectionCount;
			int totalPowerUps = waterBottleCount + (pillUsed ? 1 : 0);
			if (totalInfections > totalPowerUps) {
				stars--;
			}
		}
		return stars;
	}
}
