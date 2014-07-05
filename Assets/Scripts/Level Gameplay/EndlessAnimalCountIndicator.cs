using UnityEngine;
using System.Collections;

public class EndlessAnimalCountIndicator : MonoBehaviour
{
	public EndlessSceneManager sceneManager;
	public SpriteRenderer currentAnimalIcon;
	public TextMesh animalCaughtCountText;
	public Sprite[] animalSprites;
	public TextMesh totalAnimalCaughtCountText;
	public GameObject newHighScoreIndicator;
	public Transform startingPoint;
	public Transform displayPoint;
	public Transform endingPoint;
	private int currentTotal = 0;

	public void activate ()
	{
		StartCoroutine ("displayCounts");
	}

	private IEnumerator displayCounts ()
	{
		int currentAnimal = 0;

		while (currentAnimal < sceneManager.animalCaughtCount.Length) {
			if (sceneManager.animalCaughtCount [currentAnimal] > 0) {
				transform.position = startingPoint.position;
				changeAnimalIcon (currentAnimal);
				float startTime = Time.time; // Time.time contains current frame time, so remember starting point

				while (Time.time-startTime<=0.5f) {
					transform.localPosition = Vector3.Lerp (startingPoint.localPosition, displayPoint.localPosition, (Time.time - startTime) * 2.0f);
					yield return 1;
				}
				 
				yield return new WaitForSeconds (0.5f);
				currentTotal += sceneManager.animalCaughtCount [currentAnimal];
				totalAnimalCaughtCountText.text = string.Format ("{0} Animals Caught!", currentTotal);
				startTime = Time.time;

				while (Time.time-startTime<=0.5f) {
					transform.localPosition = Vector3.Lerp (displayPoint.localPosition, endingPoint.localPosition, (Time.time - startTime) * 2.0f);
					yield return 1;
				}
				yield return new WaitForSeconds (0.5f);	
			}
			currentAnimal++;
		}

		newHighScoreIndicator.SetActive (sceneManager.newHighScore);
	}

	private void changeAnimalIcon (int animalIndex)
	{
		currentAnimalIcon.sprite = animalSprites [animalIndex];
		animalCaughtCountText.text = string.Format ("x {0}", sceneManager.animalCaughtCount [animalIndex]);
	}

}
