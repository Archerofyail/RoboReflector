using System.Collections;
using UnityEngine;

public class AboutButton : MonoBehaviour
{
	public Transform aboutDialog;
	public float bringDownTime = 1.5f;
	public float speed = 50f;
	void OnPress(bool isDown)
	{
		if (!isDown)
		{
			bringDownTime = BackButtonHandler.bringUpTime;
			speed = -BackButtonHandler.speed;
			StartCoroutine("BringDownAbout");
			BackButtonHandler.isAboutDown = true;
		}
	}

	IEnumerator BringDownAbout()
	{
		float timer = 0;
		while (timer < bringDownTime)
		{
			aboutDialog.Translate(0, speed * Time.deltaTime, 0);
			timer += Time.deltaTime;
			yield return null;
		}
	}
}

