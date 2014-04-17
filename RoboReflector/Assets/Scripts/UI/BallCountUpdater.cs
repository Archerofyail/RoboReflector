using System.Globalization;
using UnityEngine;

public class BallCountUpdater : MonoBehaviour
{
	private UILabel label;

	void Start ()
	{
		label = GetComponent<UILabel>();
		BallManager.OnBallCountUpdatedEventHandler += UpdateBallCount;
	}

	void UpdateBallCount(int newCount)
	{
		label.text = newCount.ToString(CultureInfo.InvariantCulture);
	}
}

