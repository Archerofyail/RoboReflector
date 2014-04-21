using UnityEngine;

public class StartGame : MonoBehaviour
{
	#region Start Timings

	private int index;
	private float startTimer;
	public float readyTimerMax = 1f;
	public float goTimerMax = 0.8f;

	#endregion

	private UILabel label;
	public Sprite readySprite;
	public Sprite goSprite;

	public delegate void GameStartEvent();

	public static event GameStartEvent GameStartEventHandler;

	void Start()
	{
		label = GetComponent<UILabel>();
	}

	void Update()
	{
		if (Time.timeScale > 0.5f)
		{
			startTimer += Time.deltaTime;

			if (startTimer < readyTimerMax)
			{
				label.text = "READY?";
			}
			else if (startTimer < (readyTimerMax + goTimerMax))
			{
				label.text = "GO";
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
}

