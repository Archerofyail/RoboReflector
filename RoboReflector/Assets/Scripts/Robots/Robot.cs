using UnityEngine;

public class Robot : MonoBehaviour
{
	public int minHitsToTake = 1;
	public int maxHitsToTake = 4;
	public int scoreWorth = 200;
	protected int hitsToTake;
	protected SpriteRenderer spriteRenderer;
	public GameObject explosion;


	void Start()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		hitsToTake = Random.Range(minHitsToTake, maxHitsToTake);
		SetSpriteColor();
	}


	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.transform.tag == "Ball")
		{
			HitByBall(other);
		}
	}

	protected virtual void HitByBall(Collision2D other)
	{
		hitsToTake--;
		ScoreManager.IncreaseScore(scoreWorth);
		if (hitsToTake <= 0)
		{
			ScoreManager.IncreaseScore(scoreWorth);
			if (!explosion)
			{
				explosion = (GameObject)Resources.Load("Explosion");
			}
			other.rigidbody.AddForce(other.contacts[0].normal.normalized * 3);
			Instantiate(explosion, transform.position, Quaternion.identity);
			DebugLog.LogMessage("Robot Destroyed");
			OnDeath();
			Destroy(gameObject, 0.01f);
		}
		else
		{
			SetSpriteColor();
		}
		CamShake.intensity += 0.2f;
	}

	protected virtual void OnDeath()
	{
	}

	private void SetSpriteColor()
	{
		switch (hitsToTake)
		{
			case 1:
			{
				spriteRenderer.color = Color.green;
				break;
			}
			case 2:
			{
				spriteRenderer.color = Color.blue;
				break;
			}

			case 3:
			{
				spriteRenderer.color = Color.red;
				break;
			}

		}
	}
}

