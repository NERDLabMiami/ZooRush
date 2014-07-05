using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Credits : HelpMenuSet
{
	public Animator[] credits;

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
		initialPosition = credits [0].transform.localPosition;
		recycle = false;

//		StartCoroutine (sendOutCredits ());
	}

	public override void activate ()
	{
		activated = true;
		recycle = false;
		//Shuffle Credits List
		for (int i = 0; i < credits.Length; i++) {
			Animator temp = credits [i];
			int randomIndex = Random.Range (i, credits.Length);
			credits [i] = credits [randomIndex];
			credits [randomIndex] = temp;
		}
		transform.parent = Camera.main.transform;
		transform.localPosition = Vector3.zero;
		StartCoroutine ("sendOutCredits");
	}

	public override void reset ()
	{
		recycle = false;
		foreach (Animator credit in credits) {
			credit.rigidbody2D.velocity = Vector2.zero;
			credit.transform.localPosition = initialPosition;
		}
	}

	public override void dismiss ()
	{
		activated = false;
		transform.parent = null;
		transform.position = originalPosition;
	}
	
	public IEnumerator sendOutCredits ()
	{
		currentAnimator = 0;
		credits [currentAnimator].rigidbody2D.velocity = new Vector2 (-4f, 0);
		while (currentAnimator < credits.Length - 1) {
			if (inView (credits [currentAnimator].renderer) && credits [currentAnimator].transform.position.x < Camera.main.transform.position.x) {
				currentAnimator++;
				credits [currentAnimator].rigidbody2D.velocity = new Vector2 (-4f, 0);
			}
			yield return new WaitForFixedUpdate ();
		}
		recycle = true;
	}

	void Update ()
	{
		if (recycle) {
			if (inView (credits [currentAnimator].renderer) && credits [currentAnimator].transform.position.x < Camera.main.transform.position.x) {
				currentAnimator = (currentAnimator + 1) % credits.Length;
				credits [currentAnimator].transform.localPosition = initialPosition;
			}
		}
	}

	private bool inView (Renderer obj)
	{
		Plane[] planes = GeometryUtility.CalculateFrustumPlanes (Camera.main);
		return GeometryUtility.TestPlanesAABB (planes, obj.bounds);
	}
}
