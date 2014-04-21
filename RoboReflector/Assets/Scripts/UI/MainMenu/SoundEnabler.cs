using UnityEngine;

public class SoundEnabler : MonoBehaviour 
{


	void Start()
	{
		if (PlayerPrefs.GetInt("PlayMusic") == 0)
		{
			GetComponent<UICheckbox>().isChecked = false;
		}
	}

	void OnActivate(bool isChecked)
	{
		if (isChecked)
		{
			PlayerPrefs.SetInt("PlayMusic", 1);
		}
		else
		{
			PlayerPrefs.SetInt("PlayMusic", 0);
		}
	}
}

