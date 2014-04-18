using UnityEngine;

public class StartGameButton : MonoBehaviour 
{

	void OnPress(bool isDown)
	{
		if (isDown)
		{
			Application.LoadLevel(1);
		}
	}
}

