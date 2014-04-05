using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using OpenKit;
using System;
using Facebook;

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

	public SceneManager sceneManager;

	void Start ()
	{
		redInfectionCount = 0;
		yellowInfectionCount = 0;
		greenInfectionCount = 0;
		waterBottleCount = 0;
		pillCount = 0;
	}

	void Update ()
	{
		if (GameState.checkForState (GameState.States.Play) || GameState.checkForState (GameState.States.Launch)) {
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

	public void scoreStore ()
	{
		if ((PlayerPrefs.GetInt (Application.loadedLevelName + "Stars", 0) < starsCalc ())) {
			PlayerPrefs.SetInt (Application.loadedLevelName + "Stars", starsCalc ());
			PlayerPrefs.SetInt (Application.loadedLevelName + "RedInfections", redInfectionCount);
			PlayerPrefs.SetInt (Application.loadedLevelName + "YellowInfections", yellowInfectionCount);
			PlayerPrefs.SetInt (Application.loadedLevelName + "GreenInfections", greenInfectionCount);
			PlayerPrefs.SetInt (Application.loadedLevelName + "WaterBottles", waterBottleCount);
			PlayerPrefs.SetInt (Application.loadedLevelName + "PillBottle", pillCount);
			PlayerPrefs.SetFloat (Application.loadedLevelName + "Time", timeElapsed);
		}
	}
}
