using UnityEngine;
using System.Collections;


/**
 * Scene Manager for Endless Mode Levels. Unlike regular levels, endless mode 
 * allows thw player to catch multiple animals within a limited amounnt of time.
 */ 
public class EndlessSceneManager : MonoBehaviour
{
	private Animal.AnimalValues currentAnimal;

	private int[] animalUsageCount = new int[(int)Animal.AnimalValues.COUNT];
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
		changeAnimal ();
	}
	
	void Update ()
	{
		if (!GameState.checkForState (GameState.States.Start)) {
		}
		if (GameState.checkForState (GameState.States.Play)) {

		}
	}

	private void changeAnimal ()
	{
		int newAnimalValue = Random.Range (0, (int)Animal.AnimalValues.COUNT); //chooses a random animal
		while (newAnimalValue == (int)currentAnimal) { //makes sure it's not the same as the previous animal
			newAnimalValue = Random.Range (0, (int)Animal.AnimalValues.COUNT); 
		}
		currentAnimal = (Animal.AnimalValues)newAnimalValue;
		animalUsageCount [(int)currentAnimal]++; //updates the count for this animal type
		animalAnimator.SetInteger ("Animal", (int)currentAnimal);
		animalAnimator.SetTrigger ("Change");
	}

	public void introduceAnimal ()
	{
		animalObject.transform.position = new Vector3 (characterObject.transform.position.x,
		                                              -9.5f,
		                                              characterObject.transform.position.z);
		changeAnimal ();
		StartCoroutine (getAnimalIntoView ());
	}

	private IEnumerator getAnimalIntoView ()
	{
		Vector3 finalPosition = new Vector3 (-0.51f, -3.33f, characterObject.transform.position.z);
		while (animalObject.transform.position.x < -0.5f) {
			animalObject.transform.position = Vector3.Lerp (animalObject.transform.position, finalPosition, Time.deltaTime * 2f);
			yield return new WaitForFixedUpdate ();
		}
	}
}
