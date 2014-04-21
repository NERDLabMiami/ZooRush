using UnityEngine;
using System.Collections;


/**
 * Scene Manager for Endless Mode Levels. Unlike regular levels, endless mode 
 * allows thw player to catch multiple animals within a limited amounnt of time.
 */ 
public class EndlessSceneManager : MonoBehaviour
{

	private enum AnimalValues
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
	private AnimalValues currentAnimal;

	private float[] maxTime = {
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
	public Animal animalController;
	public Animator animalAnimator;

	public CameraFollow cameraFollow;
	
	void Start ()
	{
		currentAnimal = (AnimalValues)Random.Range (0, (int)AnimalValues.COUNT); //chooses a random animal

	}
	
	void Update ()
	{
	
	}
}
