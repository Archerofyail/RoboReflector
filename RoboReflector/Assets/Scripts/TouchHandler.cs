using System;
using UnityEngine;

public class TouchHandler : MonoBehaviour
{
	private Touch currentTouch;
	public GameObject touchEventIcon;
	public delegate void OnTouchStartedEvent(Vector2 startPos);
	public static event OnTouchStartedEvent OnTouchStartedEventHandler;

	public delegate void OnTouchMovedEvent(Vector2 newPos);
	public static event OnTouchMovedEvent OnTouchMovedEventHandler;

	public delegate void OnTouchEndedEvent(Vector2 lastPos);
	public static event OnTouchEndedEvent OnTouchEndedEventHandler;

	public delegate void OnTouchStationaryEvent(Vector2 lastPos);
	public static event OnTouchStationaryEvent OnTouchStationaryEventHandler;

	void Start()
	{

	}

	void Update()
	{
		if (Input.touches.Length > 0)
		{
			currentTouch = Input.GetTouch(0);
			if (currentTouch.phase != TouchPhase.Moved && currentTouch.phase != TouchPhase.Stationary)
			{
				Instantiate(touchEventIcon,
					new Vector2(Camera.main.ScreenToWorldPoint(currentTouch.position).x,
						Camera.main.ScreenToWorldPoint(currentTouch.position).y), Quaternion.identity);
				DebugLog.LogMessage("pos is" + Camera.main.ScreenToWorldPoint(currentTouch.position));

			}
			switch (currentTouch.phase)
			{
				case TouchPhase.Began:
				{
					if (OnTouchStartedEventHandler != null)
					{
						OnTouchStartedEventHandler(Camera.main.ScreenToWorldPoint(currentTouch.position));
					}
					break;
				
				}
				case TouchPhase.Moved:
				{
					if (OnTouchMovedEventHandler != null)
					{
						OnTouchMovedEventHandler(Camera.main.ScreenToWorldPoint(currentTouch.position));
					}
					break;
				}
				case TouchPhase.Stationary:
				{
					if (OnTouchStationaryEventHandler != null)
					{
						OnTouchStationaryEventHandler(currentTouch.position);
					}
					break;
				}
				case TouchPhase.Ended:
				{
					if (OnTouchEndedEventHandler != null)
					{
						OnTouchEndedEventHandler(Camera.main.ScreenToWorldPoint(currentTouch.position));
					}
					break;
				}
				case TouchPhase.Canceled:
				{
					DebugLog.LogMessage("Touch cancelled");
					break;
				}
				default:
				throw new ArgumentOutOfRangeException();
			}
		}
	}
}

