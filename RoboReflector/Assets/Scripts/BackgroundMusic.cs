using System.Collections;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
	public string[] fileNames;
	public bool overridePrefs = false;
	void Start ()
	{
		if (PlayerPrefs.GetInt("PlayMusic") == 1 || overridePrefs)
		{
			GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("Sounds/" + fileNames[Random.Range(0, 4)]);
			GetComponent<AudioSource>().Play();
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
			GetComponent<AudioSource>().volume -= Time.deltaTime / 3;
			timer += Time.deltaTime;
			yield return null;
		}
	}
}

