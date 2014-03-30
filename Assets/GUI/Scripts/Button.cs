using UnityEngine;
using System.Collections;

/**
 * Button action controls for all button objects. This controls the action that occurs after the button is clicked and animated.
 * Subclasses typically deal with the animation/visual aspects of the button, while this class deals with the actual functionality.
 * 
 */ 
public abstract class Button : UserTouchable
{
	[HideInInspector]
	public int
		buttonType;
	[HideInInspector]
	public string
		SceneName; //Name of the scene this button directs to.
	[HideInInspector]
	public string
		otherType; //Descriptor of button if is of type "other"
	[HideInInspector]
	public OtherButtonClass
		otherButtonClass;

	protected void buttonAction ()
	{
		switch (buttonType) {
		case 0: //Direct To Scene By Name
			BreadCrumbs.nextScene = SceneName;
			BreadCrumbs.previousScene = Application.loadedLevelName;
			directToScene ();
			break;
		case 1://Direct To Next Scene
			//it's assummed that Breadcrumbs.nextScene is assigned in a different script
			BreadCrumbs.previousScene = Application.loadedLevelName;
			directToNextScene ();
			break;
		case 2://Resume
			resume ();
			break;
		case 3://Retry
			BreadCrumbs.nextScene = Application.loadedLevelName;
			retry ();
			break;
		case 4://Other
			other ();
			break;
		default:
			break;
		}
	}

	private void directToScene ()
	{

		Application.LoadLevel (BreadCrumbs.nextScene);
	}

	private void directToNextScene ()
	{
		//TODO Preparations/data needed before loading next scene
		//TODO Update to appropriate Scene Name
		directToScene ();
	}

	private void resume ()
	{

	}

	private void retry ()
	{
		//TODO Update Scene Name and any other Preparations/data needed before reloading this scene
		directToScene ();
	}

	private void other ()
	{
		switch (otherType) {
		case "Toggle":
			Debug.Log ("Toggle Button Pressed");
			//Toggle buttons handle their own data functionality since they are straight forward
			break;
		case "Pause":
			//TODO Pause Buttons are only enabled when the scene is not already paused so we need code that reflects that
			break;
		case "Dialog":
			otherButtonClass.otherButtonAction ();
			break;
		case "State Change":
			Debug.Log ("State Change Button Pressed");
			otherButtonClass.otherButtonAction (this);
			break;
		default:
			break;
		}
	}

}
