using UnityEngine;

public class ResetBallButton : MonoBehaviour
{

	private void OnPress(bool isDown)
	{
		if (!isDown && !BallManager.IsLaunching)
		{
			FindObjectOfType<BallManager>().ResetBall();
			Debug.Log("Resetting ball...");
		}
	}

}

