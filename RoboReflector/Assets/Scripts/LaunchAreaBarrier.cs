using UnityEngine;

public class LaunchAreaBarrier : MonoBehaviour 
{

	void Start()
	{
		BallManager.OnBallResetEventHandler += OnBallReset;
	}

	void OnBallReset(int newCount)
	{
		collider2D.isTrigger = true;
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag == "Ball")
		{
			collider2D.isTrigger = false;
			DebugLog.LogMessage("Barrier is now on");
		}
	}
}

