using UnityEngine;
using System.Collections;

public class BackgroundAnimal : MonoBehaviour
{
	public bool doubleSize;

	private int animalChosen;
	//Animals are numbered as follows:
	/**
	 * 0 - Bear
	 * 1 - Cheetah
	 * 2 - Crocodile
	 * 3 - Elephant
	 * 4 - Flamingo
	 * 5 - Gorilla
	 * 6 - Ostrich
	 * 7 - Penguin
	 * 8 - Rhino
	 * 9 - Tortoise
	 */ 

	private float[] animalSizes = {
		1.25f, //Size of Bear
		1.55f, //Size of Cheetah
		1.178346f, //Size of Crocodile
		2.115119f, //Size of Elephant
		1.54606f, //Size of Flamingo
		1f, //Size of Gorilla
		1.560923f, //Size of Ostritch
		0.8997142f, //Size of Penguin
		1.467667f, //Size of Rhino
		0.548549f //Size of Tortoise
	};

	void Start ()
	{
		animalChosen = Random.Range (0, 10);
		GetComponent<Animator> ().SetInteger ("Animal", animalChosen);
		GetComponent<Animator> ().SetTrigger ("Change");
		if (doubleSize) {
			transform.localScale = new Vector3 (animalSizes [animalChosen] * 2, animalSizes [animalChosen] * 2, 1f);
		} else {
			transform.localScale = new Vector3 (animalSizes [animalChosen], animalSizes [animalChosen], 1f);
		}
		if (transform.parent.localScale.x > 0) {
			rigidbody2D.velocity = new Vector2 (8f, 0);
		} else {
			rigidbody2D.velocity = new Vector2 (-8f, 0);
		}
	}

}
