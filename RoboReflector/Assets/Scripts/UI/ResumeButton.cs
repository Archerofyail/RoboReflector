using UnityEngine;

public class ResumeButton : MonoBehaviour 
{

	void OnPress(bool isDown)
	{
		if (isDown)
		{
			transform.root.gameObject.SetActive(false);
			Time.timeScale = 1;
		}
	}
}

