using UnityEngine;

public class CancelButton : MonoBehaviour 
{

	void OnPress(bool isDown)
	{
		if (!isDown)
		{
			transform.parent.gameObject.SetActive(false);
		}
	}
}

