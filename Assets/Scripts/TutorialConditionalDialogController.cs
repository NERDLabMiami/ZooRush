using UnityEngine;
using System.Collections;

public class TutorialConditionalDialogController : MonoBehaviour
{
	private SceneManager sceneManager;
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
