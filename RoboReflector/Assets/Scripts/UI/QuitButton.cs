using UnityEngine;

public class QuitButton : MonoBehaviour 
{

	void OnPress(bool isDown)
	{
		if (isDown)
		{
			Application.LoadLevel(0);
		}
	}
}

