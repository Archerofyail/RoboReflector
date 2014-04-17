using UnityEngine;

public class ScoreManager : MonoBehaviour
{
	public static int Score { get; set; }
	public static float scoreMultiplier;

	void Start()
	{
		BallManager.OnBallResetEventHandler += OnBallReset;
	}

	public static void IncreaseScore(int score)
	{
		
	}

	void OnBallReset(int count)
	{
		scoreMultiplier = 1;
	}
}

