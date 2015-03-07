using System.Collections;
using System.Linq;
using UnityEngine;

public class BallManager : MonoBehaviour
{

	public GameObject gameOverMenu;

	#region Flick Stuff
	private Vector2 flickStartPos;
	private Vector2 flickLastPos;


	public static bool IsTouching { get; private set; }
	public static bool IsLaunching { get; private set; }

	#endregion

	private static BallManager ballManager;

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

	void OnDestroy()
	{
		UnSubscribeToEvents();
	}

	void Start()
	{
		IsLaunching = true;
		ballManager = this;
		ballCount = initialBallCount;
		Input.multiTouchEnabled = false;
		ball.transform.position = ballStartPos.position;
		StartGame.GameStartEventHandler += GameStart;
		if (OnBallResetEventHandler != null)
		{
			OnBallResetEventHandler(ballCount);
		}
	}

	public static void ReLaunch()
	{
		IsLaunching = true;
		ballManager.StopCoroutine("UpdateGame");
		ballManager.ball.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
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

	IEnumerator ResetBallPos()
	{
		ball.GetComponent<Collider2D>().enabled = false;
		var posDiff = new Vector2(ball.transform.position.x - ballStartPos.position.x, ball.transform.position.y - ballStartPos.position.y);
		while (Mathf.Abs(posDiff.x) > 0.01f && Mathf.Abs(posDiff.y) > 0.01f)
		{
			ball.transform.position = Vector3.Lerp(ball.transform.position, ballStartPos.position, 0.1f);
			posDiff = new Vector2(ball.transform.position.x - ballStartPos.position.x, ball.transform.position.y - ballStartPos.position.y);
			yield return null;
		}
		ball.GetComponent<Collider2D>().enabled = true;
	}

	IEnumerator UpdateGame()
	{
		while (true)
		{

			if (ball.GetComponent<Rigidbody2D>().velocity.sqrMagnitude <= 0.05f && !IsLaunching)
			{
				//Log.LogMessage("Ball not moving, resetting in " + (ballResetAfterStopTime - ballNotMovedTime) + " seconds");
				ballNotMovedTime += Time.deltaTime;
				if (ballNotMovedTime > ballResetAfterStopTime)
				{
					//Debug.Log("Resetting ball...");
					ResetBall();
				}
			}
			yield return null;
		}
	}

	void OnTouchDown(Vector2 pos)
	{
		if (IsLaunching)
		{
			//if (ball.touchTrigger.OverlapPoint(pos))
			{
				flickStartPos = pos;
				IsTouching = true;
			}
		}
	}

	void OnTouchStationary(Vector2 pos)
	{
		if (IsLaunching)
		{
			if (IsTouching)
			{
				flickStartPos = pos;
			}
		}
	}

	void OnTouchMoved(Vector2 pos)
	{
		if (IsLaunching)
		{
			if (IsTouching)
			{
				if (Vector2.Distance(flickLastPos, pos) < 0.5f)
				{
					flickStartPos = pos;
				}
				flickLastPos = pos;
			}
		}
	}
	void OnTouchEnd(Vector2 pos)
	{
		if (IsLaunching && IsTouching && !EnemyManager.Blocks.Any(block => block.IsMoving))
		{
			if (Vector2.Distance(pos, flickStartPos) > 1)
			{
				ball.GetComponent<Rigidbody2D>().AddForce((pos - flickStartPos) * 300);
				flickStartPos = Vector2.zero;
				IsLaunching = false;
				IsTouching = false;
				StartCoroutine("UpdateGame");
			}
		}
	}

	public void ResetBall()
	{

		ballCount--;
		if (ballCount < 0)
		{
			gameOverMenu.SetActive(true);
		}
		else
		{
			StopCoroutine("UpdateGame");
			StartCoroutine(ResetBallPos());
			ball.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
			ballNotMovedTime = 0f;
			IsLaunching = true;
			if (OnBallCountUpdatedEventHandler != null)
			{
				OnBallCountUpdatedEventHandler(ballCount);
			}
			if (OnBallResetEventHandler != null)
			{
				OnBallResetEventHandler(ballCount);
			}

		}
	}
}

