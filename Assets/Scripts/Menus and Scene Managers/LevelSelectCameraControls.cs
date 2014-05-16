using UnityEngine;
using System.Collections;


public class LevelSelectCameraControls : MonoBehaviour
{
		Camera[] cameras;
		float rightMax = 0;
		Camera rightMost;
		Camera leftMost;
		float width;
		public float xInput, mousePosition;
		public bool move;
		public LevelSelect levelSelectController;
		public TextMesh[] leftCenterRightMeshes;

		void Start ()
		{
//				PlayerPrefs.DeleteAll ();
				cameras = GameObject.FindObjectsOfType<Camera> ();
				foreach (Camera camera in cameras) {
						if (camera.gameObject.tag == "option") {
								if (camera.rect.x > rightMax) {
										rightMax = camera.rect.x;
										rightMost = camera;
								}
								if (Mathf.Approximately (camera.rect.x, 0.025f)) {
										leftMost = camera;
								}
						}
				}
				changeScoreValues ();
				move = false;
				width = 0.325f;
		}
	
		void FixedUpdate ()
		{
				xInput = Input.GetAxis ("Horizontal");
				if (!move) {
						if (xInput > 0) {
								moveLeft ();
						}
						if (xInput < 0) {
								moveRight ();
						}
				}
		}

		private void changeScoreValues ()
		{
				bool[] leftCenterRightUpdated = {false, false, false};
				foreach (Camera camera in cameras) {
						if (camera.gameObject.tag == "option") {
								int index = currentCameraIndex (camera);
								if (index < 3) {
										leftCenterRightUpdated [index] = true;
										updateHighScore (index, camera.name);
								}
						}
				}
				for (int i = 0; i < 3; i++) {
						if (!leftCenterRightUpdated [i]) {
								leftCenterRightMeshes [i].text = "";
						}
				}
		}

		private int currentCameraIndex (Camera cam)
		{
				if (Mathf.Round (cam.rect.x * 1000) == 25) { // left
						return 0;
				}

				if (Mathf.Round (cam.rect.x * 1000) == 675) { //right
						return 2;
				}

				if (Mathf.Round (cam.rect.x * 1000) == 350) { //center
						return 1;
				}

				return 3;
		}
			
		private void updateHighScore (int leftCenterRight, string levelName)
		{
				int score = 0;
				if (PlayerPrefs.HasKey (levelName + "Score")) {
						score = PlayerPrefs.GetInt (levelName + "Score");
				}
				Debug.Log ("Updating score for " + levelName + "score is " + score);
				leftCenterRightMeshes [leftCenterRight].text = "x" + score;
		}

		public void moveLeft ()
		{
				if (Mathf.Round (rightMost.rect.x * 1000) >= 675f) {
			
						StartCoroutine (moveCameras (true));
				}
		}

		public void moveRight ()
		{
				if (Mathf.Round (leftMost.rect.x * 1000) <= 25f) {
						if (!move) {
								StartCoroutine (moveCameras (false));
						}
				}
		}

		private IEnumerator moveCameras (bool left)
		{
				move = true;
				float[] newValues = new float[cameras.Length];
				float[] newValuesVelocity = new float[cameras.Length];
				float newRightMost = rightMost.rect.x - width;
				float newLeftMost = leftMost.rect.x + width;

				for (int i = 0; i < cameras.Length; i++) {
						if (cameras [i].gameObject.tag == "option") {
								newValues [i] = cameras [i].rect.x;
								if (left) {
										newValues [i] -= width + 0.1f;
								} else {
										newValues [i] += width + 0.1f;
								}
						}
				}

				while (true) {
						for (int i = 0; i < cameras.Length; i++) {
								if (cameras [i].gameObject.tag == "option") {
										Rect temp = cameras [i].rect;
										temp.x = Mathf.SmoothDamp (temp.x, newValues [i], ref newValuesVelocity [i], 1.5f);
										cameras [i].rect = temp;
								}
						}
						if (left) {
								if (rightMost.rect.x <= newRightMost) {

										for (int i = 0; i < cameras.Length; i++) {
												if (cameras [i].gameObject.tag == "option") {
														Rect temp = cameras [i].rect;
														temp.x = newValues [i] + 0.1f;
														cameras [i].rect = temp;
												}
										}
										break;
								}
						} else {
								if (leftMost.rect.x >= newLeftMost) {
										for (int i = 0; i < cameras.Length; i++) {
												if (cameras [i].gameObject.tag == "option") {
														Rect temp = cameras [i].rect;
														temp.x = newValues [i] - 0.1f;
														cameras [i].rect = temp;
												}
										}
										break;
								}
						}
				}
				changeScoreValues ();
				yield return new WaitForSeconds (0.15f);
				levelSelectController.updateLevelCameras ();
				
				move = false;

		}
}
