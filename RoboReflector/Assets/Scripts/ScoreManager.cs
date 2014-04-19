using UnityEngine;

public class ScoreManager : MonoBehaviour
{
	public static int Score { get; set; }
	public static float scoreMultiplier;
	public delegate void ScoreUpdateEvent(int newScore);

	public static event ScoreUpdateEvent ScoreUpdateEventHandler;

	public delegate void MultiplierUpdateEvent(float newScore);

	public static event MultiplierUpdateEvent MultiplierUpdateEventHandler;

	void Start()
	{
		BallManager.OnBallResetEventHandler += OnBallReset;
		Log.LogMessage("Score manager Subscribed to BallReset");
	}

	void OnDestroy()
	{
		BallManager.OnBallResetEventHandler -= OnBallReset;
	}

	public static void IncreaseScore(int score)
	{
		Log.LogMessage("Increased score by " + (score * scoreMultiplier));
		Score += (int)(score * scoreMultiplier);
		scoreMultiplier += 0.5f;
		if (MultiplierUpdateEventHandler != null)
		{
			MultiplierUpdateEventHandler(scoreMultiplier);
		}
		if (ScoreUpdateEventHandler != null)
		{
			ScoreUpdateEventHandler(Score);
		}
	}

	void OnBallReset(int count)
	{
		Log.LogMessage("multiplier reset");
		scoreMultiplier = Mathf.Clamp(scoreMultiplier / 4, 1, 500);
		if (MultiplierUpdateEventHandler != null)
		{
			MultiplierUpdateEventHandler(scoreMultiplier);
		}
	}
}

