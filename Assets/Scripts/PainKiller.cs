using UnityEngine;
using System.Collections;

/**
 * Touch Handler for Pain Killer Button
 * @author: Ebtissam Wahman
 */ 
public class PainKiller : TouchHandler
{
	private int pillCount; // current count of Pills
	private PainIndicator painIndicator; //pointer to the Pain Indicator 
	private SceneManager sceneManager; //pointer to scene manager
	private ScoreKeeper scoreKeeper; //pointer to score keeper
	private int painPoints = 100; //amount of points we subtract from the pain indicator
	private bool interacted; //if an interaction has been registered
	public AudioClip clip; //audio clip that plays on clicking the pill
	private AudioController audioController; //pointer to the scene audio controller

	void Start ()
	{
		scoreKeeper = GameObject.FindObjectOfType<ScoreKeeper> ();
		sceneManager = GameObject.FindObjectOfType<SceneManager> ();
		painIndicator = GameObject.FindObjectOfType<PainIndicator> ();
		audioController = GameObject.FindObjectOfType<AudioController> ();
		pillCount = PlayerPrefs.GetInt ("PILLS"); //at the start of the level we get our current pill count
		sceneManager.updatePillCount (pillCount);
	}

	/**
	 * When the pain killer button is pressed, we want to update the pill count,
	 * update the pain indicator, and add the pill usage to the score keeper.
	 */ 
	public override void objectTouched ()
	{
		if (!interacted && Input.GetMouseButtonUp (0)) {
			interacted = true;
			audioController.objectInteraction (clip);
			if (pillCount > 0) {
				pillCount--;
				sceneManager.updatePillCount (pillCount); //tells the scene manager to update our pill count
				scoreKeeper.addToCount ("Pill");
				painIndicator.subtractPoints (painPoints);
			}
			StartCoroutine (waitToResetTouch ()); //resets the intereacted tracker after a small period of time
		}
	}

	public override void objectUntouched ()
	{

	}

	public void incrementPillCount ()
	{
		//TODO Add +1 GUI Animation and pill shake/glow as well as temporary smiley face to character face
		if (pillCount < 3) {
			pillCount = pillCount + 1;
			sceneManager.updatePillCount (pillCount);
		}
	}

	public void savePillCount ()
	{
		PlayerPrefs.SetInt ("PILLS", pillCount); //updates our global value for the pill count
	}

	protected override IEnumerator waitToResetTouch ()
	{
		yield return new WaitForSeconds (0.2f);
		interacted = false;
	}
}
