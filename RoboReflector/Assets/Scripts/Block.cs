using UnityEngine;

public class Block : MonoBehaviour
{
	public bool IsMoving { get; private set; }
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
		if (collider2D.OverlapPoint(pos) && !BallManager.IsLaunching)
		{
			gameObject.layer = LayerMask.NameToLayer("Block_Moving");
			IsMoving = true;
		}
	}

	void OnTouchMoved(Vector2 pos)
	{
		if (IsMoving)
		{
			transform.position = pos;
		}
	}

	void OnTouchEnd(Vector2 pos)
	{
		if (IsMoving)
		{
			IsMoving = false;
			gameObject.layer = 1 << 0;
			Destroy(this);
		}
	}

}

