using System;
using System.Collections;
using UnityEngine;

public enum GameState
{
	BeforeLaunch,
	BallMoving,
	EndGame,
}

public class GameStateManager : MonoBehaviour
{
	public int score;
	public GameState currentGameState = GameState.BeforeLaunch;

	#region Flick Stuff
	private Vector2 flickStartPos;
	private Vector2 flickLastPos;


	private bool isTouching;
	#endregion

	private float ballNotMovedTime;
	private float ballResetAfterStopTime = 1.5f;
	
	public Ball ball;
	public Transform ballStartPos;

	public int ballCount = 3;

	public GameStateManager()
	{
		TouchHandler.OnTouchStartedEventHandler += OnTouchDown;
		TouchHandler.OnTouchMovedEventHandler += OnTouchMoved;
		TouchHandler.OnTouchEndedEventHandler += OnTouchEnd;
	}

	void Start()
	{
		Input.multiTouchEnabled = false;
		ball.transform.position = ballStartPos.position;
		
		StartCoroutine(UpdateGame());
		//DebugLog.LogMessage("Started StateManager");
	}

	void OnDestroy()
	{
		//TouchHandler.OnTouchStartedEventHandler -= OnTouchDown;
		//TouchHandler.OnTouchMovedEventHandler -= OnTouchMoved;
		//TouchHandler.OnTouchEndedEventHandler -= OnTouchEnd;
		DebugLog.LogMessage("Unsubscribed from events");
	}

	IEnumerator UpdateGame()
	{
		while (true)
		{
			if (Input.GetKeyDown(KeyCode.Escape))
			{
				Application.Quit();
			}
			switch (currentGameState)
			{
				case GameState.BeforeLaunch:
				{
					
					break;
				}
				case GameState.BallMoving:
				{
					if (ball.rigidbody2D.velocity.sqrMagnitude <= 0.5f)
					{
						ballNotMovedTime += Time.deltaTime;
						if (ballNotMovedTime > ballResetAfterStopTime)
						{
							ResetBall();
						}
					}
					break;
				}
				case GameState.EndGame:
				{
					break;
				}
				default:
				throw new ArgumentOutOfRangeException();
			}
			yield return null;
		}
	}

	void OnTouchDown(Vector2 pos)
	{
		if (currentGameState == GameState.BeforeLaunch)
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
		if (currentGameState == GameState.BeforeLaunch)
		{
			if (isTouching)
			{
				flickStartPos = pos;
			}
		}
	}

	void OnTouchMoved(Vector2 pos)
	{
		//DebugLog.LogMessage("Moving Ball, state is " + currentGameState);
		if (currentGameState == GameState.BeforeLaunch)
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
		DebugLog.LogMessage("Entered OnTouchEnd, state is " + currentGameState);

		if (currentGameState == GameState.BeforeLaunch)
		{
			if (isTouching)
			{
				DebugLog.LogMessage("Launched ball, delta is " + (pos - flickStartPos));
				ball.rigidbody2D.AddForce((pos - flickStartPos) * 300);
				flickStartPos = Vector2.zero;
				currentGameState = GameState.BallMoving;
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
		currentGameState = GameState.BeforeLaunch;
	}
}

