using UnityEngine;

public class GameOverMainMenuButton : MonoBehaviour 
{

	void OnPress(bool isDown)
	{
		if (!isDown)
		{
			Application.LoadLevel(0);
		}
	}
}

