using UnityEngine;

public class Block : MonoBehaviour
{
	private bool isMoving;
	private SpriteRenderer spriteRenderer;
	public Sprite brightSprite;
	public Sprite normalSprite;
	void Start()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();

		TouchHandler.OnTouchStartedEventHandler += OnTouchDown;
		TouchHandler.OnTouchMovedEventHandler += OnTouchMoved;
		TouchHandler.OnTouchEndedEventHandler += OnTouchEnd;
	}

	void OnDestroy()
	{
		spriteRenderer.sprite = normalSprite;
		TouchHandler.OnTouchStartedEventHandler -= OnTouchDown;
		TouchHandler.OnTouchMovedEventHandler -= OnTouchMoved;
		TouchHandler.OnTouchEndedEventHandler -= OnTouchEnd;
	}


	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.transform.tag == "Laser")
		{
			other.gameObject.SetActive(false);
		}
	}

	void OnTouchDown(Vector2 pos)
	{
		if (collider2D.OverlapPoint(pos))
		{
			isMoving = true;
		}
	}

	void OnTouchMoved(Vector2 pos)
	{
		if (isMoving)
		{
			transform.position = pos;
		}
	}

	void OnTouchEnd(Vector2 pos)
	{
		if (isMoving)
		{
			Destroy(this);
		}
	}

}

