using UnityEngine;
using System.Collections;
using System;

public class LevelSelect : MonoBehaviour
{

	GameObject[] cameras;
	Camera leftLevel;
	Camera rightLevel;
	Camera currentLevel;

	string[] levelNames = {
		"Splash",
		"Level1-Tutorial",
		"Level2-Zoo",
		"Level3-Suburbs",
		"Level4-Suburbs",
		"Level5-Suburbs",
		"Level6-Beach",
		"Level7-Beach",
		"Level8-Downtown",
		"Level9-Downtown",
		"Level10-Downtown"
	};

	public Sprite[] starImages;

	void Start ()
	{
		cameras = GameObject.FindGameObjectsWithTag ("option");
		updateStarScores ();
	}
	
	void Update ()
	{
		updateLevelCameras ();
		if (Input.GetButtonUp ("Fire1") || Input.GetMouseButtonUp (0)) {
			goToLevel ();
		}
	}

	void goToLevel ()
	{
		int levelNumber = Int32.Parse (currentLevel.name.Substring (currentLevel.name.LastIndexOf ('a') + 1));
		NextSceneHandler.loadGameLevelWithConditions (levelNames [levelNumber]);
	}

	void updateLevelCameras ()
	{
		foreach (GameObject camera in cameras) {
			float camLocation = Mathf.Round (camera.camera.rect.x * 1000);
			if (Mathf.Approximately (camLocation, 350f)) {
				currentLevel = camera.camera;
			}
		}
	}

	void updateStarScores ()
	{
		foreach (GameObject camera in cameras) {
			GameObject stars = camera.GetComponentInChildren<SpriteRenderer> ().gameObject.transform.parent.gameObject; //get's the stars game object
			int levelNumber = Int32.Parse (camera.name.Substring (camera.name.LastIndexOf ('a') + 1));
			setStars (levelNumber, stars);
		}
	}

	private void setStars (int level, GameObject starsObject)
	{
		int stars = ((PlayerPrefs.HasKey (levelNames [level] + "Stars")) ? PlayerPrefs.GetInt (levelNames [level] + "Stars") : 0);
		
		if (stars > 0) {//# of stars > 0
			SpriteRenderer[] starShapes = starsObject.GetComponentsInChildren<SpriteRenderer> ();
			foreach (SpriteRenderer star in starShapes) {
				if (star.name.Contains ("Star 1")) {
					if (stars >= 1) {
						star.sprite = starImages [1];
					} else {
						star.sprite = starImages [0];
					}
				}
				if (star.name.Contains ("Star 2")) {
					if (stars >= 2) {
						star.sprite = starImages [1];
					} else {
						star.sprite = starImages [0];
					}
				}
				if (star.name.Contains ("Star 3")) {
					if (stars >= 3) {
						star.sprite = starImages [1];
					} else {
						star.sprite = starImages [0];
					}
				}
			}
		}
	}
}
