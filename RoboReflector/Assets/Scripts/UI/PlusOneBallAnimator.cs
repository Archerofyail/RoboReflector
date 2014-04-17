using System.Collections;
using UnityEngine;

public class PlusOneBallAnimator : MonoBehaviour
{
	public float maxVisibleTime = 1.2f;
	public float stationaryTime = 0.5f;
	public float risingSpeedIncrease = 0.2f;
	public float alphaDecreaseSpeed = 0.05f;
	public UILabel label;

	void Start ()
	{
		label = GetComponent<UILabel>();
		StartCoroutine(FloatUp());
	}

	IEnumerator FloatUp()
	{
		float timer = 0;
		float risingSpeed = 0;
		float alpha = 1;
		while (timer < maxVisibleTime)
		{
			if (timer > stationaryTime)
			{
				risingSpeed += risingSpeedIncrease;
				alpha -= alphaDecreaseSpeed;
				label.color = new Color(label.color.r, label.color.g, label.color.b, alpha);
			}
			transform.Translate(0, risingSpeed * Time.deltaTime, 0);
			timer += Time.deltaTime;
			yield return null;
		}
		Destroy(gameObject);

	}
}

