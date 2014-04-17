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
		DebugLog.LogMessage("Update ballcount UI, is now " + newCount);

		label.text = newCount.ToString(CultureInfo.InvariantCulture);
	}
}

