using System.Collections;
using UnityEngine;

public class GameOverFadein : MonoBehaviour
{
	public float fadeInMax = 1f;
	public GameObject menu;

	void Start ()
	{
		StartCoroutine("FadeCoverIn");
		PlayerPrefs.SetInt("HighScore", ScoreManager.Score);
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

