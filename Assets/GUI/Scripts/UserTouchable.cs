using UnityEngine;
using System.Collections;

public abstract class UserTouchable : MonoBehaviour
{
	public abstract void onPressUp ();
	public abstract void reset ();
	public abstract void onPressDown ();
}
