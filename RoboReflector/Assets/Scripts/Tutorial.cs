using System.Linq;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
	public GameObject[] tutorials;
	//private SpriteRenderer spriteRenderer;
	private int index = 0;

	void Awake()
	{
		Time.timeScale = 0;
	}

	void Start()
	{
		//spriteRenderer = GetComponent<SpriteRenderer>();
		if (tutorials.Length >= 1)
		{
			//spriteRenderer.sprite = tutorials[index];
			tutorials[0].SetActive(true);
		}
		if (PlayerPrefs.GetInt("HasTutorialRun") == 1)
		{
			Destroy(gameObject);
		}
		PlayerPrefs.SetInt("HasTutorialRun", 1);
		TouchHandler.OnTouchEndedEventHandler += OnTouchDown;
	}

	void OnDestroy()
	{
		TouchHandler.OnTouchEndedEventHandler -= OnTouchDown;
		Time.timeScale = 1;
	}

	void CheckForNull()
	{
		if (tutorials.All(step => !step))
		{
			Destroy(gameObject);
		}
	}

	void OnTouchDown(Vector2 pos)
	{
		index++;
		//spriteRenderer.sprite = tutorials[index];

		if (index < tutorials.Length)
		{
			tutorials[index].SetActive(true);
		}
		Invoke("CheckForNull", 0.1f);
		Destroy(tutorials[index - 1]);
	}
}

