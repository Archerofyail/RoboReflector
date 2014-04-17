using System.Globalization;
using UnityEngine;

public class Power : MonoBehaviour
{
	public int charges = 0;
	public UILabel label;

	void Start()
	{
		label = GetComponentInChildren<UILabel>();
		label.color = new Color(label.color.r, label.color.g, label.color.b, 0.4f);
		label.text = charges.ToString(CultureInfo.InvariantCulture);
	}

	void OnPress(bool isDown)
	{
		if (isDown && charges > 0)
		{
			DebugLog.LogMessage("Speed Power button pressed");
			charges--;
			label.text = charges.ToString(CultureInfo.InvariantCulture);
		}
	}
}

