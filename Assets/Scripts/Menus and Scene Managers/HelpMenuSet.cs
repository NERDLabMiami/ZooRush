using UnityEngine;
using System.Collections;

public abstract class HelpMenuSet : MonoBehaviour
{
		public TextAsset textSource;
		protected Vector3 originalPosition;
		public string[] displayText;
		public bool activated;

		public abstract void activate ();
		public abstract void dismiss ();
		public abstract void reset ();

		void Awake ()
		{
				originalPosition = transform.position;
				displayText = textSource.text.Split ("\n" [0]);
				for (int i = 0; i < displayText.Length; i++) {
						displayText [i] = StringFunctions.lineWrap (displayText [i], 20, true);
				}
		}

}
