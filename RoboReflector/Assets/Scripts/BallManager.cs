using System.Collections;
using UnityEngine;

public class BallManager : MonoBehaviour
{

	#region Flick Stuff
	private Vector2 flickStartPos;
	private Vector2 flickLastPos;


	private bool isTouching;
	private bool isLaunching = true;
	#endregion



	private float ballNotMovedTime;
	private float ballResetAfterStopTime = 1.5f;

	public Ball ball;
	public Transform ballStartPos;
	public int initialBallCount;
	public static int ballCount;
	private Vector2 ballVelocity;
	public delegate void OnBallResetEvent(int count);

	public static event OnBallResetEvent OnBallResetEventHandler;

	public static event OnBallResetEvent OnBallCountUpdatedEventHandler;

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
		ballCount = initialBallCount;
		Input.multiTouchEnabled = false;
		ball.transform.position = ballStartPos.position;
		StartGame.GameStartEventHandler += GameStart;
		if (OnBallResetEventHandler != null)
		{
			OnBallResetEventHandler(ballCount);
		}
	}

	public static void IncreaseBallCount()
	{
		ballCount++;
		if (OnBallCountUpdatedEventHandler != null)
		{
			OnBallCountUpdatedEventHandler(ballCount);
		}
	}

	void GameStart()
	{
		SubscribeToEvents();

		StartGame.GameStartEventHandler -= GameStart;
	}

	IEnumerator UpdateGame()
	{
		while (true)
		{
			
				if (ball.rigidbody2D.velocity.magnitude <= 0.2f)
				{
					DebugLog.LogMessage("Ball not moving, resetting in " + (ballResetAfterStopTime - ballNotMovedTime) + " seconds");
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
			if (ball.touchTrigger.OverlapPoint(pos))
			{
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
				ball.rigidbody2D.AddForce((pos - flickStartPos) * 300);
				flickStartPos = Vector2.zero;
				isLaunching = false;
				isTouching = false;
				StartCoroutine("UpdateGame");
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
		if (OnBallCountUpdatedEventHandler != null)
		{
			OnBallCountUpdatedEventHandler(ballCount);
		}
		if (OnBallResetEventHandler != null)
		{
			OnBallResetEventHandler(ballCount);
		}
		StopCoroutine("UpdateGame");
	}
}

