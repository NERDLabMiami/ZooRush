using UnityEngine;
using System.Collections;

/**
 * Toggles a value between true and false 
 * 
 */ 
public class ToggleButton : Button
{

	public SpriteRenderer toggleSprite;
	private SpriteRenderer buttonSprite;
	private bool toggleValue; //if this property controlled by this button is true or false

	void Start ()
	{
		buttonSprite = GetComponent<SpriteRenderer> ();
		if (!toggleValue) {
			toggleSprite.color = Color.clear;
		}
	}

	public override void reset ()
	{
		lightButton ();
	}

	public override void onPressUp ()
	{
		lightButton ();
		setToggleValue (!toggleValue);
	}

	public override void onPressDown ()
	{
		dimButton ();
	}

	private void dimButton ()
	{
		buttonSprite.color = Color.grey;
	}

	private void lightButton ()
	{
		buttonSprite.color = Color.white;
	}

	public bool setToggleValue (bool value)
	{
		toggleValue = value;

		if (toggleValue) {
			toggleSprite.color = Color.white;
		} else {
			toggleSprite.color = Color.clear;
		}

		return toggleValue;
	}

	public bool getToggleValue ()
	{
		return toggleValue;
	}

}
