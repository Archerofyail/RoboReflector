using System.Globalization;
using UnityEngine;

public class Power : MonoBehaviour
{
	public int charges = 6;
	private UILabel label;

	protected virtual void Start()
	{
		label = GetComponentInChildren<UILabel>();
		label.color = new Color(label.color.r, label.color.g, label.color.b, 0.4f);
		label.text = charges.ToString(CultureInfo.InvariantCulture);
	}

	protected virtual void OnPress(bool isDown)
	{

		charges--;
		label.text = charges.ToString(CultureInfo.InvariantCulture);
	}

	public void IncreaseCharges()
	{
		charges++;
		label.text = charges.ToString(CultureInfo.InvariantCulture);
	}
}

