using UnityEngine;

public class SpeedRobot : Robot
{
	public PlusOneBallAnimator plusOneSprite;
	protected override void OnDeath()
	{
		var sprite = (PlusOneBallAnimator)Instantiate(plusOneSprite, transform.position, Quaternion.identity);
		sprite.scale = 0.5f;
		sprite.textToAppend = " Speed Power";
		FindObjectOfType<SpeedPower>().IncreaseCharges();
		base.OnDeath();
	}
}

