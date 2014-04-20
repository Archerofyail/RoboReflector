using System;
using UnityEngine;

public class MultiplierUpdater : MonoBehaviour 
{

	private UILabel label;

	void Start()
	{
		label = GetComponent<UILabel>();
		ScoreManager.MultiplierUpdateEventHandler += UpdateMultiplier;
	}

	void OnDestroy()
	{
		ScoreManager.MultiplierUpdateEventHandler -= UpdateMultiplier;
	}

	void UpdateMultiplier(float newCount)
	{
		label.text = Math.Round(newCount, 2) + "x";
	}
}

