using UnityEngine;

public class Robot : MonoBehaviour
{
	public int minHitsToTake;
	public int maxHitsToTake;

	private int hitsToTake;
	private SpriteRenderer spriteRenderer;
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
			HitByBall();
		}
	}

	protected virtual void HitByBall()
	{
		hitsToTake--;

		if (hitsToTake <= 0)
		{
			if (!explosion)
			{
				explosion = (GameObject)Resources.Load("Explosion");
			}
			Instantiate(explosion, transform.position, Quaternion.identity);
			Destroy(gameObject, 0.01f);
		}
		else
		{
			SetSpriteColor();
		}
		CamShake.intensity += 0.2f;
	}

	void SetSpriteColor()
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

