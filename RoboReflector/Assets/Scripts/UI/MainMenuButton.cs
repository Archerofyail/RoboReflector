using UnityEngine;

public class MainMenuButton : MonoBehaviour
{

	public GameObject confirmMenu;

	void OnPress(bool isDown)
	{
		if (!isDown)
		{
			confirmMenu.SetActive(true);
		}
	}
}

