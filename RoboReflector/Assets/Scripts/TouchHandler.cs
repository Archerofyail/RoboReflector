using System;
using System.Collections;
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

	private Vector2 lastMousePos;

	void Start()
	{
		StartCoroutine(Input.mousePresent ? UpdateMouse() : UpdateTouch());
	}

	IEnumerator UpdateMouse()
	{
		while (true)
		{
			if (Input.mousePresent)
			{
				if (Input.GetMouseButtonDown(0))
				{
					if (OnTouchStartedEventHandler != null)
					{
						OnTouchStartedEventHandler(Camera.main.ScreenToWorldPoint(Input.mousePosition));
					}
				}
				if (Input.GetMouseButton(0))
				{
					Vector2 currentPos = GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);
					if (lastMousePos != currentPos)
					{
						if (OnTouchMovedEventHandler != null)
						{
							OnTouchMovedEventHandler(currentPos);
						}
					}
					else
					{
						if (OnTouchStationaryEventHandler != null)
						{
							OnTouchStationaryEventHandler(currentPos);
						}
					}
					lastMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				}
				if (Input.GetMouseButtonUp(0))
				{
					if (OnTouchEndedEventHandler != null)
					{
						OnTouchEndedEventHandler(GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition));
					}
				}
			}
			yield return null;
		}
	}

	IEnumerator UpdateTouch()
	{
		while (true)
		{
			if (Input.touchCount > 0)
			{
				currentTouch = Input.GetTouch(0);
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
						//Log.LogMessage("Touch cancelled");
						break;
					}
					default:
						throw new ArgumentOutOfRangeException();
				}
			}
			yield return null;
		}
	}
}

