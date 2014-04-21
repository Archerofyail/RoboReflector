using UnityEngine;

public class HighScoreGrabber : MonoBehaviour 
{

	void Start ()
	{
		var label = GetComponent<UILabel>();
		label.text += PlayerPrefs.GetInt("HighScore");
	}
}

