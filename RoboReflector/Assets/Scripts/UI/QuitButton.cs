using UnityEngine;

public class QuitButton : MonoBehaviour 
{

	void OnPress(bool isDown)
	{
		if (!isDown)
		{
			Time.timeScale = 1;
			Application.LoadLevel(0);
		}
	}
}

