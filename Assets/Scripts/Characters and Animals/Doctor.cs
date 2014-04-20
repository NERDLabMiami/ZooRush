using UnityEngine;
using System.Collections;

public class Doctor : MonoBehaviour
{
	public GameObject speechBubble;
	public Sprite[] doctorSprite; //index 0 - normal state, index 1 - wink state
	private SpriteRenderer sprite; 

	void Start ()
	{
		sprite = GetComponent<SpriteRenderer> ();
		speechBubble.SetActive (false);
	}

	public void react ()
	{
		sprite.sprite = doctorSprite [1];
		speechBubble.SetActive (true);
	}

	public void reset ()
	{
		sprite.sprite = doctorSprite [0];
		speechBubble.SetActive (false);
	}
}
