using System;
using System.Collections;
using UnityEngine;

public class BackButtonHandler : MonoBehaviour
{
	public static bool isAboutDown;
	public static float bringUpTime = 0.7f;
	public static float speed = 1200f;
	public Transform aboutDialog;
	public Vector3 initialAboutPos;
	private Vector3 finalAboutPos = new Vector3(-49, 887f);

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
		initialAboutPos = aboutDialog.position;
		float timer = 0;
		while (timer < bringUpTime)
		{
			aboutDialog.position = new Vector3(aboutDialog.position.x,
				Mathf.SmoothStep(initialAboutPos.y, finalAboutPos.y, timer / bringUpTime), aboutDialog.position.z);
			timer += Time.deltaTime;
			yield return null;
			
		}
		isAboutDown = false;
	}
}

