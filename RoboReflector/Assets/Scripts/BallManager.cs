using System;
using System.Collections;
using UnityEngine;

public class BallManager : MonoBehaviour
{
	public int score;

	#region Flick Stuff
	private Vector2 flickStartPos;
	private Vector2 flickLastPos;


	private bool isTouching;
	private bool isLaunching;
	#endregion



	private float ballNotMovedTime;
	private float ballResetAfterStopTime = 1.5f;
	
	public Ball ball;
	public Transform ballStartPos;

	public int ballCount = 3;

	public void SubscribeToEvents()
	{
		TouchHandler.OnTouchStartedEventHandler += OnTouchDown;
		TouchHandler.OnTouchMovedEventHandler += OnTouchMoved;
		TouchHandler.OnTouchEndedEventHandler += OnTouchEnd;
	}

	public void UnSubscribeToEvents()
	{
		TouchHandler.OnTouchStartedEventHandler -= OnTouchDown;
		TouchHandler.OnTouchMovedEventHandler -= OnTouchMoved;
		TouchHandler.OnTouchEndedEventHandler -= OnTouchEnd;
	}

	void Start()
	{
		Input.multiTouchEnabled = false;
		ball.transform.position = ballStartPos.position;
		StartGame.GameStartEventHandler += GameStart;

	}

	void GameStart()
	{
		SubscribeToEvents();
		StartCoroutine(UpdateGame());
		StartGame.GameStartEventHandler -= GameStart;
	}

	void OnDestroy()
	{
		DebugLog.LogMessage("Unsubscribed from events");
	}

	IEnumerator UpdateGame()
	{
		while (true)
		{

			if (ball.rigidbody2D.velocity.sqrMagnitude <= 0.5f)
			{
				ballNotMovedTime += Time.deltaTime;
				if (ballNotMovedTime > ballResetAfterStopTime)
				{
					ResetBall();
				}
			}
			yield return null;
		}
	}

	void OnTouchDown(Vector2 pos)
	{
		if (isLaunching)
		{
			if (ball.collider2D.OverlapPoint(pos))
			{
				DebugLog.LogMessage("Touched Ball");
				
				flickStartPos = pos;
				isTouching = true;
			}
		}
	}

	void OnTouchStationary(Vector2 pos)
	{
		if (isLaunching)
		{
			if (isTouching)
			{
				flickStartPos = pos;
			}
		}
	}

	void OnTouchMoved(Vector2 pos)
	{
		if (isLaunching)
		{
			if (isTouching)
			{
				if (Vector2.Distance(flickLastPos, pos) < 0.5f)
				{
					flickStartPos = pos;
				}

					flickLastPos = pos;
				ball.transform.position = pos;
			}
		}
	}
	void OnTouchEnd(Vector2 pos)
	{
		if (isLaunching)
		{
			if (isTouching)
			{
				DebugLog.LogMessage("Launched ball, delta is " + (pos - flickStartPos));
				ball.rigidbody2D.AddForce((pos - flickStartPos) * 300);
				flickStartPos = Vector2.zero;
				isLaunching = false;
				isTouching = false;
			}

		}
	}

	void ResetBall()
	{
		ballCount--;
		ball.transform.position = ballStartPos.position;
		ball.rigidbody2D.velocity = Vector2.zero;
		ballNotMovedTime = 0f;
		isLaunching = true;
	}
}

