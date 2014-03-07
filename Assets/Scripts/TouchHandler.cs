using UnityEngine;
using System.Collections;


/** Interface for objects that can receive touches.
 * @author Ebtissam Wahman
 */ 
public abstract class TouchHandler : MonoBehaviour
{
	public abstract void objectTouched ();
	public abstract void objectUntouched ();
	protected abstract IEnumerator waitToResetTouch ();
}
