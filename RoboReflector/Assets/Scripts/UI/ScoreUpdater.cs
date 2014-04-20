using System.Globalization;
using UnityEngine;

public class ScoreUpdater : MonoBehaviour 
{

	private UILabel label;

	void Start()
	{
		label = GetComponent<UILabel>();
		ScoreManager.ScoreUpdateEventHandler += UpdateScore;
	}

	void OnDestroy()
	{
		ScoreManager.ScoreUpdateEventHandler -= UpdateScore;
	}

	void UpdateScore(int newCount)
	{
		label.text = newCount.ToString(CultureInfo.InvariantCulture);
	}
}

