using UnityEngine;

public class Robot : MonoBehaviour
{
	public int hitsToTake;
	private SpriteRenderer spriteRenderer;
	void Start()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
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
				spriteRenderer.color = Color.red;
				break;
			}

		}
	}
}

