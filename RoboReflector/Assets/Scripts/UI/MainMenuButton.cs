using UnityEngine;

public class MainMenuButton : MonoBehaviour
{

	private GameObject confirmMenu;

	void Start()
	{
		confirmMenu = GameObject.Find("ConfirmQuit");
		confirmMenu.SetActive(false);
	}

	void OnPress(bool isDown)
	{
		if (isDown)
		{
			confirmMenu.SetActive(true);
		}
	}
}

