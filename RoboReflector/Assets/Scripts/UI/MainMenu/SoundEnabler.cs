using UnityEngine;

public class SoundEnabler : MonoBehaviour 
{


	void Start()
	{
		if (PlayerPrefs.GetInt("IsFirstRun") == 0)
		{
			PlayerPrefs.SetInt("IsFirstRun", 1);
			PlayerPrefs.SetInt("PlayMusic", 1);
		}
		if (PlayerPrefs.GetInt("PlayMusic", 0) == 0)
		{
			GetComponent<UICheckbox>().isChecked = false;
		}
	}

	void OnActivate(bool isChecked)
	{
		if (isChecked)
		{
			PlayerPrefs.SetInt("PlayMusic", 1);
			//Debug.Log("Set playmusic to 1");
		}
		else
		{
			//Debug.Log("Set playmusic to 0");
			PlayerPrefs.SetInt("PlayMusic", 0);
		}
	}
}

