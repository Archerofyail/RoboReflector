using System.Collections;
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
		//if (PlayerPrefs.GetInt("HasTutorialRun") == 1)
		//{
		//	Destroy(gameObject);
		//}
		PlayerPrefs.SetInt("HasTutorialRun", 1);
		TouchHandler.OnTouchEndedEventHandler += OnTouchDown;
	}

	void OnDestroy()
	{
		TouchHandler.OnTouchEndedEventHandler -= OnTouchDown;
		Time.timeScale = 1;
	}

	IEnumerator CheckForNull()
	{
		float timer = 0;
		float start = Time.realtimeSinceStartup;
		while (timer < 0.1f)
		{
			timer += Time.realtimeSinceStartup - start;
			yield return null;
		}
		if (tutorials.All(step => step == null))
		{
			print("All tutorials were null");
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
		if (index < tutorials.Length + 1)
		{
			Destroy(tutorials[index - 1]);			
		}
		print("Moved to next tutorial");
		StartCoroutine("CheckForNull");
	}
}

