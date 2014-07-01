using UnityEngine;
using System.Collections;

public abstract class HelpMenuSet : MonoBehaviour
{
	public abstract void activate ();
	public abstract void dismiss ();
	public abstract void reset ();
}
