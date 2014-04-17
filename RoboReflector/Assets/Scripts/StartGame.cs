using UnityEngine;

public class StartGame : MonoBehaviour 
{
	#region Start Timings

	private int index;
	private float startTimer;
	public float readyTimerMax = 1f;
	public float goTimerMax = 0.8f;

	#endregion

	private SpriteRenderer spriteRenderer;
	public Sprite readySprite;
	public Sprite goSprite;

	public delegate void GameStartEvent();

	public static event GameStartEvent GameStartEventHandler;

	void Start()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	void Update()
	{
		startTimer += Time.deltaTime;
		if (startTimer < readyTimerMax)
		{
			spriteRenderer.sprite = readySprite;
		}
		else if (startTimer < (readyTimerMax + goTimerMax))
		{
			spriteRenderer.sprite = goSprite;
		}
		else if (startTimer > (readyTimerMax + goTimerMax))
		{
			if (GameStartEventHandler != null)
			{
				GameStartEventHandler();
			}
			Destroy(gameObject);
		}

	}
}

