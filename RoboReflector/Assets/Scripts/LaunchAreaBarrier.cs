using UnityEngine;

public class LaunchAreaBarrier : MonoBehaviour
{
	private SpriteRenderer spriteRenderer;
	private int rowsRemoved;
	void Start()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		BallManager.OnBallResetEventHandler += OnBallReset;
	}

	void OnDestroy()
	{
		BallManager.OnBallResetEventHandler -= OnBallReset;
	}

	void OnBallReset(int newCount)
	{
		if (collider2D)
		{
			collider2D.isTrigger = true;
		}
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.transform.tag == "Laser")
		{
			other.gameObject.SetActive(false);
			var texture = spriteRenderer.sprite.texture;
			var pixels = texture.GetPixels();
			var rowsToRemove = Mathf.Clamp(3, 0, (pixels.Length / texture.width) - (rowsRemoved));
			for (int i = texture.width * rowsRemoved; i < ((texture.width * rowsRemoved) + (texture.width * rowsToRemove)); i++)
			{
				
				var color = pixels[i];
				pixels[i] = new Color(color.r, color.g, color.b, 0);
				
			}
			rowsRemoved += rowsToRemove;
			rowsRemoved = Mathf.Clamp(rowsRemoved, 0, pixels.Length / texture.width);
			texture.SetPixels(pixels);
			texture.Apply();
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag == "Ball")
		{
			collider2D.isTrigger = false;
			Log.LogMessage("Barrier is now on");
		}
		
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Laser")
		{
			other.gameObject.SetActive(false);
		}
	}
}

