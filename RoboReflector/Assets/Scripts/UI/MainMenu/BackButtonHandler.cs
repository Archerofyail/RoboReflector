using System.Collections;
using UnityEngine;

public class BackButtonHandler : MonoBehaviour
{
	public static bool isAboutDown;
	public static float bringUpTime = 0.7f;
	public static float speed = 1200f;
	public Transform aboutDialog;

	void Update () 
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (isAboutDown)
			{
				StartCoroutine("HideAbout");
			}
			else
			{
				Application.Quit();
			}
		}
	}

	IEnumerator HideAbout()
	{
		float timer = 0;
		while (timer < bringUpTime)
		{
			aboutDialog.Translate(0, speed * Time.deltaTime, 0);
			timer += Time.deltaTime;
			yield return null;
		}
	}
}

