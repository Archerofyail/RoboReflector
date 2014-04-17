using UnityEngine;

public class Robot : MonoBehaviour
{
	public int minHitsToTake;
	public int maxHitsToTake;

	private int hitsToTake;
	private SpriteRenderer spriteRenderer;
	void Start()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		hitsToTake = Random.Range(minHitsToTake, maxHitsToTake);
		SetSpriteColor();
	}

	void Update()
	{

	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.transform.tag == "Ball")
		{
			hitsToTake--;
			SetSpriteColor();
			if (hitsToTake <= 0)
			{
				Destroy(gameObject, 0.1f);
			}
			CamShake.intensity += 0.2f;
		}
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

