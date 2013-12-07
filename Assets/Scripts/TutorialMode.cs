using UnityEngine;
using System.Collections;
/** Text and object position handler for tutorial mode.
 * @author: Ebtissam Wahman
 * 
 */ 
public class TutorialMode : MonoBehaviour
{
	private bool tutorialEnabled;
	
	private bool dialogPresent;
	private bool infectionDialog;
	private bool powerUpDialog;
	private bool painBarDialog;
	private bool distanceBarDialog;
	private bool introDialog;
	private bool firstAidDialog;
	private int i;

	SceneManager sceneManager;
	
	private GameObject tutText;
	private GameObject objectScanner;
	
	public Sprite[] speechBubbles;

	void Start ()
	{
		sceneManager = GetComponent<SceneManager> ();
		tutorialEnabled = sceneManager.tutEnabled;
		dialogPresent = false;
		introDialog = false;
		infectionDialog = false;
		powerUpDialog = false;
		painBarDialog = false;
		distanceBarDialog = false;
		firstAidDialog = false;
		i = 0;
		tutText = GameObject.Find ("GUI - Tutorial Text");
		objectScanner = GameObject.Find ("Tutorial - Object Scanner");
		tutText.GetComponent<TextMesh> ().text = "";
		tutText.GetComponentInChildren<SpriteRenderer> ().enabled = false;
	}
	
	void Update ()
	{
		if (tutorialEnabled) {
			RaycastHit2D detected = Physics2D.Raycast (objectScanner.transform.position, Vector2.up);
			//First check for intro dialog
			//Second check for pain dialog
			//Then check for the presence of any other objects
			if (!introDialog && !dialogPresent) {
				tutText.GetComponentInChildren<SpriteRenderer> ().enabled = true;
				tutText.transform.localPosition = new Vector3 (-3f, 0.5f, tutText.transform.localPosition.z);
				tutText.GetComponentInChildren<SpriteRenderer> ().sprite = speechBubbles [2]; // speech bubble without pointer
				sceneManager.isPlaying = false;
				dialogSequene (introDialogScript, ref introDialog);
			} else {
				if (!distanceBarDialog && !dialogPresent) {
					tutText.transform.localPosition = new Vector3 (-3f, -2.2f, tutText.transform.localPosition.z);
					tutText.GetComponentInChildren<SpriteRenderer> ().enabled = true;
					if (i > 0) {
						tutText.GetComponentInChildren<SpriteRenderer> ().sprite = speechBubbles [0]; // speech bubble points bottom left
					} else {
						tutText.GetComponentInChildren<SpriteRenderer> ().sprite = speechBubbles [2]; // speech bubble without pointer
					}

					sceneManager.isPlaying = false;
					dialogSequene (distanceBarDialogScript, ref distanceBarDialog);
				} else {
					if (!painBarDialog && !dialogPresent) {
						tutText.transform.localPosition = new Vector3 (-4.5f, 3.8f, tutText.transform.localPosition.z);
						tutText.GetComponentInChildren<SpriteRenderer> ().enabled = true;
						tutText.GetComponentInChildren<SpriteRenderer> ().sprite = speechBubbles [1]; // speech bubble points to top right
						sceneManager.isPlaying = false;
						dialogSequene (painBarDialogScript, ref painBarDialog);
					} else {

						if (detected.collider != null && !dialogPresent) {


							if (detected.collider.name.Contains ("Doctor")) {
								if (!firstAidDialog) {
									tutText.transform.localPosition = new Vector3 (0.5f, 4f, tutText.transform.localPosition.z);
									tutText.GetComponentInChildren<SpriteRenderer> ().sprite = speechBubbles [0]; // speech bubble points bottom left
									tutText.GetComponentInChildren<SpriteRenderer> ().enabled = true;
									sceneManager.isPlaying = false;
									dialogSequene (fisrtAidDialogScript, ref firstAidDialog);
								}
							}

							if (detected.collider.name.Contains ("Infection")) {
								if (!infectionDialog) {
									tutText.transform.position = new Vector3 (detected.transform.position.x + 1.5f, detected.transform.position.y + 2f, tutText.transform.position.z);
									tutText.GetComponentInChildren<SpriteRenderer> ().sprite = speechBubbles [0]; // speech bubble points bottom left
									tutText.GetComponentInChildren<SpriteRenderer> ().enabled = true;
									sceneManager.isPlaying = false;
									dialogSequene (infectionDialogScript, ref infectionDialog);
								}
							}
					
							if (detected.collider.name.Contains ("Power Up")) {
								if (!powerUpDialog) {
									tutText.transform.position = new Vector3 (detected.transform.position.x + 1.5f, detected.transform.position.y + 2f, tutText.transform.position.z);
									tutText.GetComponentInChildren<SpriteRenderer> ().sprite = speechBubbles [0]; // speech bubble points bottom left
									tutText.GetComponentInChildren<SpriteRenderer> ().enabled = true;
									sceneManager.isPlaying = false;
									dialogSequene (powerUpDialogScript, ref powerUpDialog);
								}
							}
						}
					}
				}
			}
		}

	}

	private string[] introDialogScript = {
		"Welcome to Zoo Rush!","It's time to catch the animals."

		#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBPLAYER
		, "Press ↑ and ↓ to move."
		#endif

#if UNITY_IPHONE || UNITY_ANDROID || UNITY_WP8
		"Tap the top half of the screen to move up.",
		"Tap the bottom half of the screen to move down."
#endif

	};

	private string[] distanceBarDialogScript = {
		"As you run, you will pick up speed.","Check your distance to the animal\nhere."
	};

	private string[] painBarDialogScript = {
		"With sickle-cell anemia, you have to\nkeep a close eye on your health.","Don’t let the crisis meter fill up."
	};

	private string[] fisrtAidDialogScript = {
		"Health centers can help you manage\nyour pain and reduce the crisis meter."
	};

	private string[] infectionDialogScript = {
		"Look out for infections!","They slow you down and bring you\ncloser to a crisis.","Some infections are more dangerous\nthan others…"
	};

	private string[] powerUpDialogScript = {
		"To avoid getiing too close to a crisis,","Try drinking water or visiting a\nhealth center to manage the pain!"
	};

	private void dialogSequene (string[] dialog, ref bool finished)
	{
		tutText.GetComponent<TextMesh> ().text = dialog [i];
		if (InputManager.enter) {
			i++;
		}
		if (i >= dialog.Length) {
			tutText.GetComponent<TextMesh> ().text = "";
			tutText.GetComponentInChildren<SpriteRenderer> ().enabled = false;
			dialogPresent = true;
			sceneManager.isPlaying = true;
			finished = true;
			i = 0;
			dialogPresent = false;
		}
	}
}
