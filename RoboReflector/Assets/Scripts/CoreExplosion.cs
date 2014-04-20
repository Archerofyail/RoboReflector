using UnityEngine;

public class CoreExplosion : Destroyer
{
	public float activationTime = 0.8f;
	public GameObject nextExplosion;

	protected override void Start()
	{
		Invoke("ActivateExplosion", activationTime);
		base.Start();
	}

	void ActivateExplosion()
	{
		if (nextExplosion)
		{
			nextExplosion.SetActive(true);
		}
	}
}

