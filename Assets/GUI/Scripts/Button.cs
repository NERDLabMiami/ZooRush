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

	private Renderer[] allRenderers;
	private Collider2D[] allColliders;

	public void disableButton ()
	{
		setEnabled (false);
	}

	public void enableButton ()
	{
		setEnabled ();
	}

	private void setEnabled (bool enable = true)
	{
		if (allRenderers == null) {
			allRenderers = GetComponentsInChildren<Renderer> ();
		}
		if (allColliders == null) {
			allColliders = GetComponentsInChildren<Collider2D> ();
		}

		foreach (Renderer rend in allRenderers) {
			rend.enabled = enable;
		}
		foreach (Collider2D coll in allColliders) {
			coll.enabled = enable;
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
			NextSceneHandler.loadGameLevelWithConditions (BreadCrumbs.nextScene);
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

	private void resume ()
	{
		GameState.requestPlay ();
		Destroy (transform.parent.gameObject);
	}

	private void retry ()
	{
		directToScene ();
	}

	private void other ()
	{
		if (otherButtonClass != null) {
			otherButtonClass.otherButtonAction (this);
		}
	}

}
