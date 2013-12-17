using UnityEngine;
using System.Collections;

public class StoryModeHandler : MonoBehaviour
{
	public static string NextSceneName;
	public GameObject[] slides;
	private GameObject currentSlide;
	private int slideIndex;
	
	void Start ()
	{
		slideIndex = 0;
		currentSlide = Instantiate (slides [slideIndex], new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
	}
	
	void Update ()
	{
		if (InputManager.enter) {
			if (slideIndex < slides.Length - 1) {
				slideIndex++;
				Destroy (currentSlide);
				currentSlide = Instantiate (slides [slideIndex], new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
			} else {
				LoadLevel.levelToLoad = NextSceneName;
				Application.LoadLevel ("Loading");
			}
		}
	}
}
