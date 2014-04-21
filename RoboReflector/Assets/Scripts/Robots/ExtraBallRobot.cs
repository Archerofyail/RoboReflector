using UnityEngine;

public class ExtraBallRobot : Robot
{
	public GameObject plusOneSprite;

	protected override void OnDeath()
	{
		//Log.LogMessage("Instantiated plus one sprite");
		BallManager.IncreaseBallCount();
		Instantiate(plusOneSprite, transform.position - Vector3.forward, Quaternion.identity);
		base.OnDeath();
	}
}

