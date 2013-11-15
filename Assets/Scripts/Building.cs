using UnityEngine;
using System.Collections;

public class Building : MonoBehaviour
{
	Animator animate;
	private string characterName;
	
	// Use this for initialization
	void Start ()
	{
		animate = GetComponent<Animator> ();
		characterName = "BoyZoo";
	}
	
	void OnCollisionEnter2D (Collision2D coll)
	{
		if (coll.gameObject.name == characterName) {
			animate.SetTrigger ("Open");
		}
		
	}
	
}
