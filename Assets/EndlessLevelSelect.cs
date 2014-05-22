using UnityEngine;
using System.Collections;
using System;

public class EndlessLevelSelect : LevelSelect
{
		public void Awake ()
		{
				//disables any locked levels
				cameras = GameObject.FindGameObjectsWithTag ("option");
				foreach (GameObject currentCamera in cameras) {
						PlayerPrefs.SetInt (currentCamera.name, 1);
						if (PlayerPrefs.GetInt (currentCamera.name, 0) == 0) {
								currentCamera.SetActive (false);
						}
				}
		}
	
		public void Start ()
		{
				cameras = GameObject.FindGameObjectsWithTag ("option");
				updateLevelCameras ();
		}
	
		public override void goToLevel ()
		{
				Debug.Log ("CALLED");
				if (currentLevel != null) {
						NextSceneHandler.nextLevel (currentLevel.name);
				}
		}
	
		public override void updateLevelCameras ()
		{
				foreach (GameObject camera in cameras) {
						float camLocation = Mathf.Round (camera.camera.rect.x * 1000);
						if (Mathf.Approximately (camLocation, 350f)) {
								currentLevel = camera.camera;
								return;
						}
				}
				currentLevel = null;
		}

}
