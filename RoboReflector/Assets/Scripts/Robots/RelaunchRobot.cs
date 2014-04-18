using UnityEngine;

public class RelaunchRobot : Robot
{
	public PlusOneBallAnimator sprite;
	protected override void OnDeath()
	{
		var plusOne = (PlusOneBallAnimator) Instantiate(sprite, transform.position, Quaternion.identity);
		plusOne.scale = 0.5f;
		plusOne.textToAppend = " Relaunches";
		base.OnDeath();
	}
}

