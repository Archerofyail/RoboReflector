using UnityEngine;

public class RelaunchRobot : Robot
{
	public PlusOneBallAnimator ballAnimator;
	protected override void OnDeath()
	{
		var plusOne = (PlusOneBallAnimator)Instantiate(ballAnimator, transform.position, Quaternion.identity);
		plusOne.scale = 0.5f;
		plusOne.textToAppend = " Relaunches";
		FindObjectOfType<ReLaunchPower>().IncreaseCharges();
		base.OnDeath();
	}
}

