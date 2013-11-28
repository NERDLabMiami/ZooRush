using UnityEngine;
using System.Collections;

public class PainBar : MonoBehaviour
{
	public float painRate; // using 0.1f atm
	public float painPoints;
	public float maxPainBarSize;
	public Sprite[] healthStates;
	
	private SceneManager sceneManager;
	
	void Start ()
	{
		sceneManager = FindObjectOfType<SceneManager> ();
		transform.localScale = new Vector3 (0f, transform.localScale.y, transform.localScale.z);
		painPoints = 0f;
		maxPainBarSize = 0.32f;
		if (painRate <= 0) {
			painRate = 0.1f;
		}
	}
	
	void FixedUpdate ()
	{
		if (sceneManager.isPlaying) {
			painPoints += (Time.time * painRate);
		}
	}
	
	void Update ()
	{
		if (painPoints > 100f) {
			painPoints = 100f;
		}
		if (painPoints < 0) {
			painPoints = 0;
		}
		
		if (painPoints <= 100f) { // Change health bar sized based on current pain points


			transform.localScale = new Vector3 ((painPoints / 100f) * maxPainBarSize, 
			                                    transform.localScale.y, 
			                                    transform.localScale.z);
		}
		if (painPoints < 33f) { // Change health to Green
			GetComponent<SpriteRenderer> ().sprite = healthStates [0];
		}
		if (painPoints > 33f && painPoints < 75f) { // Change health to Yellow
			GetComponent<SpriteRenderer> ().sprite = healthStates [1];
		}
		if (painPoints > 75f) { // Change health to Red
			GetComponent<Animator> ().SetTrigger ("Crisis");
			GetComponent<SpriteRenderer> ().sprite = healthStates [2];
		}
	}

	public void objectInteraction (GameObject obj)
	{
		if (obj.name.Contains ("Infection")) {
			if (obj.name.Contains ("Red")) {
				painPoints += 35f;
			} else {
				if (obj.name.Contains ("Yellow")) {
					painPoints += 20f;
				} else { // infection is green
					painPoints += 5f;
				}
			}

		} else {
			if (obj.name.Contains ("Power Up")) {
				if (obj.name.Contains ("Water Bottle")) {
					painPoints -= 25f;
				} else {
					painPoints -= 75f;
				}
			}
		}
	}
}
