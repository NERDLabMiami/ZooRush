using UnityEngine;
using System.Collections;

/** Main controller for most level-based events.
 * 
 * @author: Ebtissam Wahman
 */ 
public class SceneManagerStory : SceneManager
{
		public float distanceDiffMin; 		//Minimum distance needed between character and animal
		public float currentDistanceDiff; 	//Current distance between character and animal
	
		protected float timeOutDistance; //Distance at which it is almost impossible to catch the animal

		void Start ()
		{
				timeOutDistance = GameObject.FindObjectOfType<SceneRepeater> ().getSceneWidth () * 0.90f;
				cameraFollow = GameObject.FindObjectOfType<CameraFollow> ();
				timedOut = false; 
				tutEnabled = PlayerPrefs.GetInt ("TUTORIAL", 0) == 0;

				isPlaying = true;
				pauseAudio = false;
				fainted = false;
				characterObject = GameObject.FindGameObjectWithTag ("character");
				animalObject = GameObject.FindGameObjectWithTag ("animal");
				animalController = GameObject.FindObjectOfType<Animal> ();
				distanceDiffMin = 7f;
				currentDistanceDiff = Mathf.Abs (animalObject.transform.position.x - characterObject.transform.position.x);
				if (tutEnabled) {
						gameObject.AddComponent<TutorialConditionalDialogController> ();
				}
				GameState.requestStart ();
		}
	

		private void OnStateChanged ()
		{
				switch (GameState.currentState) {
				case GameState.States.Intro:
						StartCoroutine (introSequence ());
						break;
				case GameState.States.Transition:
						if (animalController.caught) {
								GameState.requestWin ();
						}
						if (fainted) {
								GameState.requestLose ();
						}
						if (timedOut) {
								if (cameraFollow.cameraSettled) {
										cameraFollow.moveCameraToCharacterOffset (currentDistanceDiff + 5f);
										StartCoroutine (timeOutSequence ());
								}
						}
						break;
				case GameState.States.Win:
						if (tutEnabled) {
								PlayerPrefs.SetInt ("TUTORIAL", 1);
						}
						GameObject.FindObjectOfType<PainKiller> ().savePillCount ();
						BreadCrumbs.nextScene = NextSceneName;
						break;
				default:
						break;
				}
		}

		private IEnumerator introSequence ()
		{
				while (currentDistanceDiff < 25f) {
						yield return new WaitForFixedUpdate ();
				}
				animalObject.transform.localPosition = new Vector3 (animalObject.transform.localPosition.x + 25f, animalObject.transform.localPosition.y, animalObject.transform.localPosition.z);
				GameState.requestPlay ();
				if (!tutEnabled) {
						GameObject.FindObjectOfType<CharacterSpeech> ().SpeechBubbleDisplay ("Ok! Let's go!");
				} else {
						GameObject.FindObjectOfType<CharacterSpeech> ().SpeechBubbleDisplay ("The first day\nis the hardest.");
				}
				cameraFollow.moveCameraToCharacterOffset (5f);

		}

		private IEnumerator timeOutSequence ()
		{
				while (!cameraFollow.cameraSettled) {
						yield return new WaitForFixedUpdate ();
				}
				while (objInView (animalController.renderer)) {
						((AnimalStory)animalController).setSpeed ();
						yield return new WaitForFixedUpdate ();
				}
				animalController.speed = Vector2.zero;
				GameState.requestLose ();
		}
	
		void Update ()
		{
				if (!GameState.checkForState (GameState.States.Start)) {
						currentDistanceDiff = Mathf.Abs (animalObject.transform.localPosition.x - characterObject.transform.localPosition.x);
				}
				if (GameState.checkForState (GameState.States.Play)) {
						if (currentDistanceDiff > timeOutDistance) {
								timedOut = true;
								GameState.requestTransition ();
						}
						if (currentDistanceDiff < distanceDiffMin) {
								GameState.requestLaunch ();
						}
				}
		}

		private bool objInView (Renderer obj)
		{
				Plane[] planes = GeometryUtility.CalculateFrustumPlanes (Camera.main);
				return GeometryUtility.TestPlanesAABB (planes, obj.bounds);
		}

}


