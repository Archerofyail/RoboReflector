using System.Collections;
using UnityEngine;

public class CamShake : MonoBehaviour
{
	public Transform normalPos;

	public static float intensity;
	public float dropOff;
	public float minDropOff;
	public float normalDropOff;
	public float maxIntensity;

	void Start ()
	{
		StartCoroutine(Shake());
	}
	
	void Update ()
	{
		if (Input.GetKeyDown(KeyCode.K))
		{
			intensity += 1;
		}
		if (Input.GetKeyDown(KeyCode.L))
		{
			intensity += 1;
		}
		
	}

	IEnumerator Shake()
	{
		while (true)
		{
			intensity = Mathf.Clamp(intensity, 0, maxIntensity);
			dropOff = Mathf.Clamp(dropOff, minDropOff, normalDropOff);
			if (intensity > 0)
			{
				transform.position = normalPos.position + (Vector3) (Random.insideUnitCircle * intensity);
				intensity -= dropOff;
			}
			else
			{
				dropOff = normalDropOff;
			}
			yield return null;
		}
	}
}

