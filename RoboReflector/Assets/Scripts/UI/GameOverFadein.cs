using System.Collections;
using UnityEngine;

public class GameOverFadein : MonoBehaviour
{
	public float fadeInMax = 1f;
	public GameObject menu;
	public delegate void OnGameOverEvent();
	public static event OnGameOverEvent OnGameOverEventHandler;

	void Start ()
	{
		StartCoroutine("FadeCoverIn");
		if (ScoreManager.Score > PlayerPrefs.GetInt("HighScore"))
		{
			PlayerPrefs.SetInt("HighScore", ScoreManager.Score);
		}

		if (OnGameOverEventHandler != null)
		{
			OnGameOverEventHandler();
		}
	}

	IEnumerator FadeCoverIn()
	{
		float timer = 0;
		UISprite spriteRenderer = GetComponent<UISprite>();
		while (timer < fadeInMax)
		{
			timer += Time.deltaTime;
			var color = spriteRenderer.color;
			spriteRenderer.color = new Color(color.r, color.g, color.b, Mathf.Clamp01(timer));
			yield return null;
		}
		menu.SetActive(true);
	}
}

