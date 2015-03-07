using UnityEngine;
using System.Collections;

public class CorePulsate : MonoBehaviour
{
	public float frequency = 1f;
	public float positiveAmplitude = Mathf.Abs(0.4f);
	[Tooltip("It's absolute")]
	public float negativeAmplitude = Mathf.Abs(0.4f);
	private float timer = 0f;
	private Vector3 defaultScale;

	void Start()
	{
		defaultScale = transform.localScale;
	}

	void Update()
	{
		timer = Mathf.PingPong(Time.time, frequency);
		transform.localScale =
			new Vector3(
				Mathf.Lerp(defaultScale.x + positiveAmplitude, defaultScale.x - negativeAmplitude,
					Mathf.InverseLerp(0, frequency, timer)),
				Mathf.Lerp(defaultScale.y + positiveAmplitude, defaultScale.y - negativeAmplitude, Mathf.InverseLerp(0, frequency, timer)), defaultScale.z);
	}
}
