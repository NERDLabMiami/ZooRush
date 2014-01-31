using UnityEngine;
using System.Collections;

public class Vehicle : ObjectModel
{
	public bool isCar;
	public AudioClip clip;
 
	void Start ()
	{
	
	}
	
	void Update ()
	{
	
	}

	protected override void resetOtherValues ()
	{
		
	}

	public override void collisionDetected ()
	{
		stopMoving ();
		if (isCar) {
			GameObject.FindObjectOfType<SceneManager> ().hitByVehicle = true;
		} else {
			GameObject.FindObjectOfType<PainIndicator> ().subtractPoints (40);
		}
	}
	
	public override void interactWithCharacter (Collider2D character)
	{
		GameObject.FindObjectOfType<AudioController> ().objectInteraction (clip);
		character.GetComponent<PlayerControls> ().resetSpeed ();
	}
	
	private void stopMoving ()
	{
		rigidbody2D.velocity = new Vector2 (0, 0);
	}
}
