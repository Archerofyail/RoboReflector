using System.Collections;
using UnityEngine;

public class LaunchAreaBarrier : MonoBehaviour
{
	private SpriteRenderer spriteRenderer;
	public Transform ball;
	private int rowsRemoved;

	void Start()
	{
		BallManager.OnBallResetEventHandler += OnBallReset;
		spriteRenderer = GetComponent<SpriteRenderer>();
		StartCoroutine("CheckForBall");
	}

	void OnDestroy()
	{
		BallManager.OnBallResetEventHandler -= OnBallReset;
	}

	void OnBallReset(int newCount)
	{
		gameObject.layer = LayerMask.NameToLayer("Shield_Launching");
	}

	IEnumerator CheckForBall()
	{
		while (true)
		{
			if (ball.position.y < transform.position.y)
			{
				gameObject.layer = LayerMask.NameToLayer("Shield_Launching");
			}
			else
			{
				gameObject.layer = LayerMask.NameToLayer("Shield_Free");
			}
			yield return null;
		}
	}
	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.transform.tag == "Laser")
		{
			other.gameObject.SetActive(false);
			var texture = spriteRenderer.sprite.texture;
			var pixels = texture.GetPixels();
			var rowsToRemove = Mathf.Clamp(10, 0, (pixels.Length / texture.width) - (rowsRemoved));
			for (int i = texture.width * rowsRemoved; i < ((texture.width * rowsRemoved) + (texture.width * rowsToRemove)); i++)
			{
				
				var color = pixels[i];
				pixels[i] = new Color(color.r, color.g, color.b, 0);
				
			}
			rowsRemoved += rowsToRemove;
			rowsRemoved = Mathf.Clamp(rowsRemoved, 0, pixels.Length / texture.width);
			texture.SetPixels(pixels);
			texture.Apply();
			if (rowsRemoved == pixels.Length / texture.width)
			{
				Destroy(gameObject);
			}
		}
	}
}

