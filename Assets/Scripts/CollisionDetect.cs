using UnityEngine;
using System.Collections;

public class CollisionDetect : MonoBehaviour
{	
	public bool isInfection;
	private bool playerCollision;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void OnCollisionEnter2D (Collision2D coll)
	{
	
		if (coll.gameObject.name == "BoyZoo") {
			playerCollision = true;
			if (isInfection) {
				collider2D.isTrigger = true; 
				Obstacle parent = transform.parent.GetComponent<Obstacle> ();
				parent.collisionDetected ();
			}

		}

	}

}