using UnityEngine;

public class ResetButton : MonoBehaviour 
{

	void Start()
	{
		TouchHandler.OnTouchEndedEventHandler += TouchEnded;
	}

	void TouchEnded (Vector2 touchWorldPos)
	{
		if (collider2D.OverlapPoint(touchWorldPos))
		{
			Application.LoadLevel(0);
			DebugLog.LogMessage("reloaded level");
		}	
	}
}

