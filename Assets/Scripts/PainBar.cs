using UnityEngine;
using System.Collections;

public class PainBar : MonoBehaviour
{
	public float painPoints;
	public float maxPainBarSize;
	public Sprite[] healthStates;
	
	void Start ()
	{
		transform.localScale = new Vector3 (0f, transform.localScale.y, transform.localScale.z);
		painPoints = 0f;
		maxPainBarSize = 3.25f;
	}
	
	void FixedUpdate ()
	{
		painPoints += (Time.time * 3f);
	}
	
	void Update ()
	{
	
		gameObject.transform.localScale = new Vector3 ((painPoints / 100f) * maxPainBarSize, 
											transform.localScale.y, 
											transform.localScale.z);
	}
}
