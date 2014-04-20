using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Credits : MonoBehaviour
{
	public List<Animator> credits;

	private Vector3 initialPosition;
	private int currentAnimator;
	private bool recycle;

	void Start ()
	{
		int animalValue = 0;
		foreach (Animator anim in credits) {
			switch (anim.name) {
			case "Clay":
				animalValue = 5;
				break;
			case "Ebby":
				animalValue = 9;
				break;
			case "Franklin":
				animalValue = 7;
				break;
			case "Isabella":
				animalValue = 2;
				break;
			case "Marcelo":
				animalValue = 3;
				break;
			case "Shareen":
				animalValue = 1;
				break;
			default:
				break;
			}
			anim.SetInteger ("Animal", animalValue);
			anim.SetTrigger ("Change");
		}
		recycle = false;
		initialPosition = credits [0].transform.position;
		//Shuffle Credits List
		for (int i = 0; i < credits.Count; i++) {
			Animator temp = credits [i];
			int randomIndex = Random.Range (i, credits.Count);
			credits [i] = credits [randomIndex];
			credits [randomIndex] = temp;
		}
		StartCoroutine (sendOutCredits ());
	}
	
	private IEnumerator sendOutCredits ()
	{
		currentAnimator = 0;
		credits [currentAnimator].rigidbody2D.velocity = new Vector2 (4f, 0);
		while (currentAnimator < credits.Count - 1) {
			if (inView (credits [currentAnimator].renderer) && credits [currentAnimator].transform.position.x > Camera.main.transform.position.x) {
				currentAnimator++;
				credits [currentAnimator].rigidbody2D.velocity = new Vector2 (4f, 0);
			}
			yield return new WaitForFixedUpdate ();
		}
		recycle = true;
	}

	void Update ()
	{
		if (recycle) {
//			Debug.Log ("Current Animator is " + currentAnimator);
			if (inView (credits [currentAnimator].renderer) && credits [currentAnimator].transform.position.x > Camera.main.transform.position.x) {
				currentAnimator = (currentAnimator + 1) % credits.Count;
				credits [currentAnimator].transform.position = initialPosition;
			}
		}
	}

	private bool inView (Renderer obj)
	{
		Plane[] planes = GeometryUtility.CalculateFrustumPlanes (Camera.main);
		return GeometryUtility.TestPlanesAABB (planes, obj.bounds);
	}
}
