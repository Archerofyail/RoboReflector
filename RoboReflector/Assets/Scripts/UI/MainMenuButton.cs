using UnityEngine;

public class MainMenuButton : MonoBehaviour
{

	public GameObject confirmMenu;
	public bool disableYourself = false;

	void OnPress(bool isDown)
	{
		if (!isDown && !disableYourself)
		{
			confirmMenu.SetActive(true);
			disableYourself = true;
			transform.parent.GetComponentInChildren<ResumeButton>().disableYourself = true;
		}
	}
}

