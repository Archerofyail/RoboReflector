using UnityEngine;

public class PauseButton : MonoBehaviour
{
	public GameObject menu;

	void OnPress (bool isDown) 
	{
		if (!isDown)
		{
			menu.SetActive(true);
			Time.timeScale = 0;
		}
	}
}

