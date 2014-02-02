using UnityEngine;
using System.Collections;

public class LevelFailModel : MonoBehaviour
{

	public static string failReason;
	public static string levelFailed;
	private TextMesh title;
	private GameObject retry;
	private GameObject quit;
	private RaycastHit hit; 
	private bool buttonClicked;
	private string[] titleOptions = {	"You Had A Crisis!", 
										"You Were Hit By A Car!", 
										"You Had A Bad Infection!",
										"Ouch! Watch Out Next Time!"};

	void Start ()
	{
		title = GameObject.Find ("Title Text").GetComponent<TextMesh> ();
		retry = GameObject.Find ("Retry");
		quit = GameObject.Find ("Quit");
		buttonClicked = false;
		switch (failReason) {
		case "Fainted":
			title.text = titleOptions [0];
			break;
		case "Hit":
			title.text = titleOptions [1];
			break;
		case "Infected":
			title.text = titleOptions [2];
			break;
		default:
			title.text = titleOptions [3];
			break;
		}
	}
	
	void Update ()
	{
		if (!buttonClicked) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			if (Physics.Raycast (ray, out hit)) {
				if (Input.GetMouseButtonUp (0)) {
					if (hit.transform.gameObject == retry) {
						buttonClicked = true;
						LoadLevel.levelToLoad = levelFailed;
						Application.LoadLevel ("Loading");
					} else {
						if (hit.transform.gameObject == quit) {
							buttonClicked = true;
							Application.LoadLevel ("Splash");
						}
					}
				}
			}
		}
	}
}
