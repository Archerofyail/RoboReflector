using System.Collections;
using UnityEngine;

public class AboutButton : MonoBehaviour
{
	public Transform aboutDialog;
	public Vector3 initialAboutPos;
	public Vector3 finalAboutPos = new Vector3(-49, -167f);
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
		initialAboutPos = aboutDialog.position;
		float timer = 0;
		while (timer < bringDownTime)
		{
			aboutDialog.position = new Vector3(aboutDialog.position.x,
				Mathf.SmoothStep(initialAboutPos.y, finalAboutPos.y, timer / bringDownTime), aboutDialog.position.z);
			timer += Time.deltaTime;
			yield return null;
		}
	}
}

