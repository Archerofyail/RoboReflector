using UnityEngine;

public class PauseButton : MonoBehaviour
{
	private GameObject menu;
	void Start ()
	{
		menu = GameObject.Find("PauseMenu");
		menu.SetActive(false);
	}
	
	void OnPress (bool isDown) 
	{
		if (!isDown)
		{
			menu.SetActive(true);
			Time.timeScale = 0;
		}
	}
}

