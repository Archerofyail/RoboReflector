using System.Collections;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
	public AudioClip[] files;
	void Start ()
	{
		if (PlayerPrefs.GetInt("PlayMusic") == 1)
		{
			audio.clip = files[Random.Range(0, files.Length)];
			audio.Play();
		}
		GameOverFadein.OnGameOverEventHandler += StartFade;
	}
	
	void StartFade ()
	{
		GameOverFadein.OnGameOverEventHandler -= StartFade;
		StartCoroutine("FadeOut");
	}

	IEnumerator FadeOut()
	{
		float timer = 0;
		while (timer < 3)
		{
			audio.volume -= Time.deltaTime / 3;
			timer += Time.deltaTime;
			yield return null;
		}
	}
}

