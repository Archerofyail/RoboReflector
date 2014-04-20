using UnityEngine;

public class ResetBallButton : MonoBehaviour
{

	private void OnPress(bool isDown)
	{
		if (!isDown)
		{
			FindObjectOfType<BallManager>().ResetBall();
		}
	}

}

