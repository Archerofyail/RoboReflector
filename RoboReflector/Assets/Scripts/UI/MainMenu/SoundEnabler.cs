using UnityEngine;

public class SoundEnabler : MonoBehaviour
{

	private bool isFirst;
	void Start()
	{
		if (PlayerPrefs.GetInt("IsFirstRun") == 0)
		{
			PlayerPrefs.SetInt("IsFirstRun", 1);
			PlayerPrefs.SetInt("PlayMusic", 1);

		}
		Invoke("IsMusicEnabled", 0.01f);
	}

	void IsMusicEnabled()
	{
		GetComponent<UICheckbox>().isChecked = PlayerPrefs.GetInt("PlayMusic", 0) != 0;
	}

	void OnActivate(bool isChecked)
	{
		
		if (isFirst)
		{
			PlayerPrefs.SetInt("PlayMusic", isChecked ? 1 : 0);
		}
		isFirst = true;
	}
}

