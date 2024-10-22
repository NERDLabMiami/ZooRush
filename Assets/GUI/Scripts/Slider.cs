﻿/** 
 * Code based off of Sebastian Lague's code for sliders in touch interfaces.
 * contact:  seblague@yahoo.com
 */ 
using UnityEngine;
using System.Collections;

[RequireComponent (typeof(BoxCollider))]
public class Slider : MonoBehaviour
{
	public Transform knob; //the part of the slider that will be dragged
	public string sliderName; //The Label of the value being changed 
	
	public int[] valueRange; //minimum and maximum values for slider values
	public int decimalPlaces; //number of decimal places to display
	private float initialSliderPercent; //starting slider percentage
	
	private Vector3 targetPos; //destination position for the slider knob
	private float sliderPercent; //current perecentage of the slider
	private float sliderLength; //length of the slider
	private float leftEdge;//leftmost edge of the slider

	void Start ()
	{
		sliderLength = GetComponent<BoxCollider> ().size.x;
		sliderPercent = PlayerPrefs.GetFloat ("Sensitivity", 1);
		leftEdge = knob.position.x - sliderLength / 2f;
		targetPos = knob.position + Vector3.right * (sliderLength / -2 + sliderLength * sliderPercent);
		knob.position = targetPos; 
	}
	
	void Update ()
	{
		knob.position = Vector3.Lerp (knob.position, targetPos, Time.deltaTime * 7);
		sliderPercent = calculatePercent ();
	}

	private float calculatePercent ()
	{
		return (knob.position.x - leftEdge) / sliderLength;
	}
	
	void OnTouchStay (Vector3 point)
	{
		targetPos = new Vector3 (point.x, targetPos.y, targetPos.z);
	}

	void OnTouchExit (Vector3 point)
	{
		PlayerPrefs.SetFloat ("Sensitivity", sliderPercent);
	}

	public float GetSliderPercent ()
	{
		return sliderPercent;
	}
}
