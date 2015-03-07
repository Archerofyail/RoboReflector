using UnityEngine;

public class CancelButton : MonoBehaviour
{
	public ResumeButton resume;
	public MainMenuButton mainMenuButton;
	void OnPress(bool isDown)
	{
		if (!isDown)
		{
			resume.disableYourself = false;
			mainMenuButton.disableYourself = false;
			transform.parent.gameObject.SetActive(false);
		}
	}
}

