﻿using UnityEngine;

public class RetryButton : MonoBehaviour 
{

	void OnPress(bool isDown)
	{
		if (!isDown)
		{
			Application.LoadLevel(1);
		}
	}
}

