using UnityEngine;
using System.Collections;

public class Infection : ObjectModel
{
		public AudioClip clip;
		private PainIndicator painIndicator;
		public int painPoints;
		public string infectionType;

		private bool stopWatchActive;
		private bool interacted;

		new void Start ()
		{
				base.Start (); 

				interacted = false;
				painIndicator = GameObject.FindObjectOfType<PainIndicator> ();
				GetComponent<Animator> ().SetInteger ("DelayVal", Random.Range (0, 3));
				int infectionVal = 0;
				switch (infectionType) {
				case "Red":
						infectionVal = 2;
						break;
				case "Yellow":
						infectionVal = 1;
						break;
				case "Green":
						infectionVal = 0;
						break;
				default:
						infectionVal = 0;
						break;
				}
				GetComponent<Animator> ().SetInteger ("InfectionType", infectionVal);
		}

		protected override void resetOtherValues ()
		{
				interacted = false;
				GetComponent<Animator> ().SetTrigger ("Reset");
				GetComponentInChildren<CollisionDetect> ().resetTouch ();
		}

		public void addToScore ()
		{
				ScoreKeeper scoreKeeper = GameObject.FindObjectOfType<ScoreKeeper> ();
				if (scoreKeeper) {
						scoreKeeper.addToCount (infectionType);
				}
		}

		public override void collisionDetected ()
		{
				GetComponent<Animator> ().SetTrigger ("Flash");

		}

		public override void interactWithCharacter (GameObject character)
		{
				if (!interacted) {
						GameObject.FindObjectOfType<AudioController> ().objectInteraction (clip);
						GameObject.FindObjectOfType<CharacterSpeech> ().SpeechBubbleDisplay ("I hate infections!");
						addToScore ();
						if (painIndicator) {
								painIndicator.addPoints (painPoints);
						}
						if (character.transform.position.y < -2.5f) {
								character.rigidbody2D.AddForce (new Vector2 (-350f, 50f));
						} else {
								character.rigidbody2D.AddForce (new Vector2 (-350f, -50f));
						}
						character.GetComponent<PlayerControls> ().resetSpeed ();
						LevelGUIController levelGUI = GameObject.FindObjectOfType<LevelGUIController> ();
						if (levelGUI) {
								levelGUI.displayStopwatch (infectionType);
						}
				}
		}

		private void leaveDelayState ()
		{
				GetComponent<Animator> ().SetTrigger ("StopDelay");
		}

		public bool touched ()
		{
				return interacted;
		}
}
