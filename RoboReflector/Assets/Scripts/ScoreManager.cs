using UnityEngine;

public class ScoreManager : MonoBehaviour
{
	public static int Score { get; set; }
	private static float scoreMultiplier;
	public static float ScoreMultiplier
	{
		get
		{
			return scoreMultiplier;
		}
		set
		{

			scoreMultiplier = Mathf.Clamp(value, 1, 150f);
			if (MultiplierUpdateEventHandler != null)
			{
				MultiplierUpdateEventHandler(scoreMultiplier);
			}
		}
	}
	public delegate void ScoreUpdateEvent(int newScore);

	public static event ScoreUpdateEvent ScoreUpdateEventHandler;

	public delegate void MultiplierUpdateEvent(float newScore);

	public static event MultiplierUpdateEvent MultiplierUpdateEventHandler;

	void Start()
	{
		Score = 0;
		BallManager.OnBallResetEventHandler += OnBallReset;
		//Log.LogMessage("Score manager Subscribed to BallReset");
	}

	void OnDestroy()
	{
		BallManager.OnBallResetEventHandler -= OnBallReset;
	}

	public static void IncreaseScore(int score)
	{
		//Log.LogMessage("Increased score by " + (score * scoreMultiplier));
		Score += Mathf.Clamp((int)(score * ScoreMultiplier), 0, 10000000);
		ScoreMultiplier += 0.5f;
		if (ScoreUpdateEventHandler != null)
		{
			ScoreUpdateEventHandler(Score);
		}
	}

	void OnBallReset(int count)
	{
		//Log.LogMessage("multiplier reset");
		ScoreMultiplier = Mathf.Clamp(ScoreMultiplier / 4, 1, 500);
		if (MultiplierUpdateEventHandler != null)
		{
			MultiplierUpdateEventHandler(ScoreMultiplier);
		}
	}
}

