using UnityEngine;
using System.Collections;

public class LevelSelectButtonController : MonoBehaviour, OtherButtonClass
{
		public Button left;
		public Button right;
		public Button center;
		public LevelSelectCameraControls cameraControl;
		public LevelSelect levelSelectController;
	
		public void otherButtonAction (Button thisButton)
		{
				if (thisButton == left) {
						cameraControl.moveRight ();
						return;
				}
				if (thisButton == right) {
						cameraControl.moveLeft ();
						return;
				}
				if (thisButton == center) {
						levelSelectController.goToLevel ();
						return;
				}
		}
}
