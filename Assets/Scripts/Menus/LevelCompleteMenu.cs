using UnityEngine;
using System.Text;
using System.Collections;

public class LevelCompleteMenu : MonoBehaviour
{
	public Animator[] starSprites;
	public TextMesh time;
	public TextMesh infections;
	int[] scores;
	
	void Start ()
	{
		int[] scores = GameObject.FindObjectOfType<ScoreKeeper> ().getScore ();
		time.text = TimeText (scores [5]);
		infections.text = (scores [0] + scores [1] + scores [2]).ToString ();
		int starCount = scores [6];
		for (int i = 0; i < starCount; i++) {
			activateStar (starSprites [i]);
		}
	}
	
	private void activateStar (Animator star)
	{
		star.SetTrigger ("Activate");
	}

	private void deactivateStart (Animator star)
	{
		star.SetTrigger ("Deactivate");
	}

	private string TimeText (int timeVal)
	{
		int minutes = timeVal / 60;
		int seconds = timeVal % 60;
		StringBuilder timeText = new StringBuilder ();
		if (minutes % 100 <= 9 && minutes <= 99) {
			timeText.Append ("0");
			timeText.Append (minutes);
		} else {
			if (minutes <= 99) {
				timeText.Append (minutes);
			} else {
				timeText.Append ("99");
			}
		}
		
		timeText.Append (":");
		
		if (seconds % 100 <= 9 && minutes <= 100f) {
			timeText.Append ("0");
			timeText.Append (seconds);
		} else {
			if (minutes <= 100f) {
				timeText.Append (seconds);
			} else {
				timeText.Append ("59+");
			}
		}
		
		return timeText.ToString ();
	}
}
