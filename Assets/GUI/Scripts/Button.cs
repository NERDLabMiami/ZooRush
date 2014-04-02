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
	public OtherButtonClass
		otherButtonClass;

	public void disableButton ()
	{
		Renderer[] allRenderers = GetComponentsInChildren<Renderer> ();
		Collider2D[] allColliders = GetComponentsInChildren<Collider2D> ();
		foreach (Renderer rend in allRenderers) {
			rend.enabled = false;
		}
		foreach (Collider2D coll in allColliders) {
			coll.enabled = false;
		}
	}

	public void enableButton ()
	{
		Renderer[] allRenderers = GetComponentsInChildren<Renderer> ();
		Collider2D[] allColliders = GetComponentsInChildren<Collider2D> ();
		foreach (Renderer rend in allRenderers) {
			rend.enabled = true;
		}
		foreach (Collider2D coll in allColliders) {
			coll.enabled = true;
		}
	}

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
			//Assume Breadcrums.nextScene has been assigned
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
		//TODO Move to new Gams state machine
		GameState.requestPlay ();
		Destroy (transform.parent.gameObject);
	}

	private void retry ()
	{
		//TODO Update Scene Name and any other Preparations/data needed before reloading this scene
		directToScene ();
	}

	private void other ()
	{
		if (otherButtonClass != null) {
			otherButtonClass.otherButtonAction (this);
		}
	}

}
