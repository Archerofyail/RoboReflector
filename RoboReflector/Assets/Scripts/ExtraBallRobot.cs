public class ExtraBallRobot : Robot 
{

	protected override void HitByBall()
	{
		BallManager.BallCount++;
		base.HitByBall();
	}
}

