using UnityEngine;

public class ResumeButton : MonoBehaviour
{
	public bool disableYourself = false;
	void OnPress(bool isDown)
	{
		if (!isDown && ! disableYourself)
		{
			transform.parent.gameObject.SetActive(false);
			Time.timeScale = 1;
		}
	}
}

