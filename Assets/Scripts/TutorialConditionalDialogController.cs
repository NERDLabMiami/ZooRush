using UnityEngine;
using System.Collections;

public class TutorialConditionalDialogController : MonoBehaviour
{
	private SceneManager sceneManager;
	public GameObject stopWatchDialog;
	public GameObject animalCaptureDialog;
	public GameObject enteredCrisisDialog;
	private bool stopWatchExplained;
	private bool animalExplained;


	void Start ()
	{
		sceneManager = GameObject.FindObjectOfType<SceneManager> ();
	}
	
	void Update ()
	{
	
	}
}
