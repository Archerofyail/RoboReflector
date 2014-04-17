using System.Globalization;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreUpdater : MonoBehaviour 
{

	private UILabel label;

	void Start()
	{
		label = GetComponent<UILabel>();
		ScoreManager.ScoreUpdateEventHandler += UpdateScore;
	}

	void UpdateScore(int newCount)
	{
		label.text = newCount.ToString(CultureInfo.InvariantCulture);
	}
}

