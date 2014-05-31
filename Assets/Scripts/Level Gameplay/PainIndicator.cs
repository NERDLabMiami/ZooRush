using UnityEngine;
using System.Collections;

public abstract class PainIndicator : MonoBehaviour
{
		public AudioClip clip;
		public float painRate;
		public float painPoints;

		protected Sprite[] healthFaces;
		protected SpriteRenderer sprite;
		protected AudioController audioController;
		protected SceneManager sceneManager;
		protected Animator animator;

		protected bool waiting;

		public abstract void subtractPoints (int points);
		public abstract void addPoints (int points);

		public abstract void objectInteraction (GameObject obj);

		public void setPoints (int points)
		{
				painPoints = points;
		}

}
