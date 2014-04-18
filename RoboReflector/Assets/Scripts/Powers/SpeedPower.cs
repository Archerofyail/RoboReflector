using UnityEngine;

public class SpeedPower : Power
{

	private Rigidbody2D ball;

	protected override void Start()
	{
		ball = FindObjectOfType<Ball>().rigidbody2D;
		base.Start();
	}

	protected override void OnPress(bool isDown)
	{
		if (isDown && charges > 0)
		{
			ball.velocity += ball.velocity.normalized * 15f;
		}
		base.OnPress(isDown);
	}
}

