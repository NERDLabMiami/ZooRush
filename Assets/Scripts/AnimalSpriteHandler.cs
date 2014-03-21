using UnityEngine;
using System.Collections;

public class AnimalSpriteHandler : MonoBehaviour
{

	Sprite sprite;

	// Use this for initialization
	void Start ()
	{
		sprite = GameObject.FindObjectOfType<Animal> ().gameObject.GetComponent<SpriteRenderer> ().sprite;
		GetComponent<SpriteRenderer> ().sprite = sprite;
	}
}
