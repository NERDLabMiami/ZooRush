using UnityEngine;
using System.Collections;

public class InstructionLevel : OtherButtonClass
{

	private int instructionIndex;
	public GameObject left;
	public GameObject right;
	public Camera[] slides;

	void Start ()
	{
		instructionIndex = 0;
	}

	void Update ()
	{
		if (instructionIndex == 0) {
			left.SetActive (false);
		} else {
			left.SetActive (true);
		}

		if (instructionIndex == slides.Length - 1) {
			right.SetActive (false);
		} else {
			right.SetActive (true);
		}
	}

	private void moveCameras (bool left)
	{
		if (left && instructionIndex > 0) {
			instructionIndex = instructionIndex - 1;
			foreach (Camera slide in slides) {
				Rect slideRect = slide.rect;
				slideRect.x++;
				slide.rect = slideRect;
			}
		} else {
			if (!left && instructionIndex < slides.Length - 1) {
				instructionIndex = instructionIndex + 1;
				foreach (Camera slide in slides) {
					Rect slideRect = slide.rect;
					slideRect.x--;
					slide.rect = slideRect;
				}
			}
		}
	}

	public override void otherButtonAction (Button thisButton)
	{
		moveCameras (thisButton.GetComponent<NextButton> ().left);
	}
}
