using UnityEngine;

public class RetryButton : MonoBehaviour 
{

	void OnPressed(bool isDown)
	{
		if (!isDown)
		{
			Application.LoadLevel(1);
		}
	}
}

