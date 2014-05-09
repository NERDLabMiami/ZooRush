using UnityEngine;
using System.Collections;


/**
 * Scene Manager for Endless Mode Levels. Unlike regular levels, endless mode 
 * allows thw player to catch multiple animals within a limited amounnt of time.
 */ 
public class EndlessSceneManager : MonoBehaviour
{
		public bool failed;
		public bool fainted;
		private bool endCalled;


		public enum AnimalValues
		{
				Bear = 0,
				Cheetah,
				Crocodile,
				Elephant,
				Flamingo,
				Gorilla,
				Ostrich,
				Penguin,
				Rhino,
				Tortoise,
				COUNT
	}
		;//Animal values set as enums for better readability

		public AnimalValues currentAnimal;

		private int[] animalUsageCount;
		public int[] animalCaughtCount;
		public int totalCaughtCount;
		public static float[] maxTime = {
		3, //Bear
		2, //Cheetah
		4, //Crocodile
		4, //Elephant
		2.5f, //Flamingo
		3, //Gorilla
		2.5f, //Ostritch
		2.5f, //Penguin
		4, //Rhino
		4 //Tortoise
	}; //maximum amount of time tha player has to catch each animal in seconds

		public GameObject animalObject;
		public GameObject characterObject;
		public EndlessAnimal animalController;
		public Animator animalAnimator;

		public GameObject endMenu;
		public TextMesh animalCaughtText;

		void OnEnable ()
		{
				GameState.StateChanged += OnStateChanged;
		}
	
		void OnDisable ()
		{
				GameState.StateChanged -= OnStateChanged;
		}

		private void OnStateChanged ()
		{
				switch (GameState.currentState) {
				case GameState.States.Intro:
						break;
				case GameState.States.Transition:
						break;
				case GameState.States.Lose:
						break;
				default:
						break;
				}
		}

		void Start ()
		{
				animalUsageCount = new int[10];
				animalCaughtCount = new int[10];
				for (int i = 0; i < 10; i++) {
						animalUsageCount [i] = 0;
						animalCaughtCount [i] = 0;
				}
				introduceAnimal ();
				failed = false;
				endCalled = false;

				totalCaughtCount = 0;
		}
	
		void FixedUpdate ()
		{
				if (!GameState.checkForState (GameState.States.Lose) && failed) {
						GameState.requestLose ();
				}
		}

		private void changeAnimal ()
		{
				int newAnimalValue = Random.Range (0, (int)AnimalValues.COUNT); //chooses a random animal
				while (newAnimalValue == (int)currentAnimal) { //makes sure it's not the same as the previous animal
						newAnimalValue = Random.Range (0, (int)AnimalValues.COUNT); 
				}
				currentAnimal = (AnimalValues)newAnimalValue;
				animalUsageCount [(int)currentAnimal]++; //updates the count for this animal type
				animalAnimator.SetInteger ("Animal", (int)currentAnimal);
				animalAnimator.SetTrigger ("Change");
		}

		public void introduceAnimal ()
		{
				Debug.Log ("INTRODUCE ANIMAL CALLED");
				animalObject.rigidbody2D.isKinematic = true;
				animalObject.transform.position = new Vector3 (characterObject.transform.position.x,
		                                              -10f,
		                                              characterObject.transform.position.z);
				changeAnimal ();
				StartCoroutine (getAnimalIntoView ());
		}

		private IEnumerator getAnimalIntoView ()
		{
				Debug.Log ("GET ANIMAL INTO VIEW CALLED");

				Vector3 finalPosition = new Vector3 (-0.5f, -3.33f, characterObject.transform.position.z);
				while (animalObject.transform.position.x < -0.51f) {
						animalObject.transform.position = Vector3.Lerp (animalObject.transform.position, finalPosition, Time.deltaTime * 2f);
						yield return new WaitForFixedUpdate ();
				}
				animalObject.rigidbody2D.isKinematic = false;
				StartCoroutine (timeOut ());

		}

		public void addToAnimalsCaught ()
		{
				totalCaughtCount++;
				animalCaughtCount [(int)currentAnimal]++;
				if (totalCaughtCount % 3 == 0) {
						GameObject.FindObjectOfType<EndlessModePainKiller> ().incrementPillCount ();
				}
				animalCaughtText.text = "Animals Rescued x" + totalCaughtCount;
		}

		private IEnumerator timeOut ()
		{
				Debug.Log ("Timeout Started");
				float currentTimePassed = 0;
				GameObject.FindObjectOfType<NetLauncher> ().resetThrowCount ();
				
				while (!failed && !animalController.caught && currentTimePassed < maxTime [(int)currentAnimal]) {
						Debug.Log ("WAITING");
						currentTimePassed += Time.deltaTime;
						yield return new WaitForFixedUpdate ();
				}
				if (!animalController.caught) {
						StartCoroutine (getAwayAndThenReset ());
				} else {
						Debug.Log ("ANIMAL WAS CAUGHT");
				}
		}

		private IEnumerator getAwayAndThenReset ()
		{
				animalController.getAway ();
				yield return new WaitForSeconds (1);
//		introduceAnimal ();
		}

		public IEnumerator callEndMenu (bool fainted = false)
		{
				Debug.Log ("END MENU CALLED");
				if (!endCalled) {
						Debug.Log ("END MENU ACTIVATING");
						endCalled = true;
						if (!fainted) {
								while (objInView(animalObject.renderer)) {
										yield return new WaitForFixedUpdate ();
								}
						}

						totalCaughtCount = 0;
						foreach (int num in animalCaughtCount) {
								totalCaughtCount += num;
						}

						if (fainted) {
								Debug.Log ("FAINTED CALLED");
								endMenu.GetComponent<EndlessModeEndMenu> ().endTitleText.text = "Thwarted by a crisis!";
						}

						endMenu.transform.localPosition = Vector3.zero;
						endMenu.GetComponent<Animator> ().SetTrigger ("Open");

						yield return new WaitForSeconds (0.3f);

						GameObject.FindObjectOfType<EndlessModeEndMenu> ().launchStat ();
				}
		}

		private bool objInView (Renderer obj)
		{
				Plane[] planes = GeometryUtility.CalculateFrustumPlanes (Camera.main);
				return GeometryUtility.TestPlanesAABB (planes, obj.bounds);
		}

		public void updatePillCount (int pillCount)
		{
				string theCount = "x" + pillCount;
				TextMesh pillCountText = GameObject.Find ("Pill Count").GetComponentInChildren<TextMesh> ();
				pillCountText.text = theCount;
		}

}
