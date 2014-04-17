using UnityEngine;

public class BallCountUpdater : MonoBehaviour
{
	private UILabel label;

	void Start ()
	{
		label = GetComponent<UILabel>();
		BallManager.OnBallResetEventHandler += UpdateBallCount;
	}

	void UpdateBallCount(int newCount)
	{
		label.text = "Balls:" + newCount;
	}
}

