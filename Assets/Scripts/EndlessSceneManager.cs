using UnityEngine;
using System.Collections;

/**
 * Scene Manager for Endless Mode Levels. Unlike regular levels, endless mode 
 * allows thw player to catch multiple animals within a limited amounnt of time.
 */ 
public class EndlessSceneManager : SceneManager
{
		public bool failed;
		private bool endCalled;
		private Transform[] startingPositions;

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

		public Animator animalAnimator;

		public GameObject endMenu;
		public TextMesh animalCaughtText;

		
		protected override void OnStateChanged ()
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
				GameObject[] objects = GameObject.FindGameObjectsWithTag ("marker");
				startingPositions = new Transform[objects.Length];
				for (int i = 0; i < startingPositions.Length; i++) {
						startingPositions [i] = objects [i].transform;
				}
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
				if (endCalled && !GameState.checkForState (GameState.States.Lose)) {
						GameState.currentState = GameState.States.Transition;
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
				
				animalObject.transform.localScale = new Vector3 (1.25f, 1.25f, 1);
				
				animalObject.transform.position = startingPositions [Random.Range (0, startingPositions.Length)].position;
//				animalObject.transform.position = new Vector3 (characterObject.transform.position.x,
//		                                              -10f,
//		                                              characterObject.transform.position.z);
				
				changeAnimal ();
				((EndlessAnimal)animalController).reset ();
				animalObject.rigidbody2D.isKinematic = true;
				StartCoroutine (getAnimalIntoView ());
		}

		private IEnumerator getAnimalIntoView ()
		{
				Debug.Log ("GET ANIMAL INTO VIEW CALLED");

				Vector3 finalPosition = new Vector3 (animalObject.transform.position.x + 6.5f, animalObject.transform.position.y + 5.25f, characterObject.transform.position.z);
				Vector3 velocity = Vector3.zero;
				while (Mathf.Abs(animalObject.transform.position.x - finalPosition.x) > 0.1f) {
//						Debug.Log ("Getting Animal into View");
						animalObject.transform.position = Vector3.SmoothDamp (animalObject.transform.position, finalPosition, ref velocity, 0.2f);
						yield return new WaitForFixedUpdate ();
				}
				animalObject.rigidbody2D.isKinematic = false;
				((EndlessAnimal)animalController).randomYVelocityChange ();
				((EndlessAnimal)animalController).randomXVelocityChange ();
				StartCoroutine (timeOut ());

		}

		public void addToAnimalsCaught ()
		{
				totalCaughtCount++;
				animalCaughtCount [(int)currentAnimal]++;
				if (totalCaughtCount % 3 == 0) {
						GameObject.FindObjectOfType<EndlessModePainKiller> ().incrementPillCount ();
				}
				animalCaughtText.text = "" + totalCaughtCount;
		}

		private IEnumerator timeOut ()
		{
				Debug.Log ("Timeout Started");
				float currentTimePassed = 0;
				GameObject.FindObjectOfType<NetLauncher> ().resetThrowCount ();
				
				while (!failed && !((EndlessAnimal)animalController).stopAllCoroutines && (currentTimePassed < maxTime [(int)currentAnimal])) {
//						Debug.Log ("WAITING");
						if (((EndlessAnimal)animalController).stopAllCoroutines) {
								Debug.Log ("OH NO ANIMAL WAS CAUGHT");
								break;
						}
						currentTimePassed += Time.deltaTime;
						yield return new WaitForFixedUpdate ();
				}
				if (!((EndlessAnimal)animalController).stopAllCoroutines) {
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
//				Debug.Log ("END MENU CALLED");
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

		
		
}
