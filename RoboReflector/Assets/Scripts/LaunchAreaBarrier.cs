using System.Collections;
using UnityEngine;

public class LaunchAreaBarrier : MonoBehaviour
{
	public Transform ball;
	private int rowsToRemove;
	private float maxScale;
	public float scaleRecharge = 0.001f;

	void Start()
	{
		BallManager.OnBallResetEventHandler += OnBallReset;
		maxScale = transform.localScale.y;
		StartCoroutine("CheckForBall");
		StartCoroutine("ShieldPowerChanger");
	}

	void OnDestroy()
	{
		BallManager.OnBallResetEventHandler -= OnBallReset;
	}

	void OnBallReset(int newCount)
	{
		gameObject.layer = LayerMask.NameToLayer("Shield_Launching");
	}

	IEnumerator CheckForBall()
	{
		while (true)
		{
			gameObject.layer = LayerMask.NameToLayer(ball.position.y < transform.position.y ? "Shield_Launching" : "Shield_Free");
			yield return null;
		}
	}
	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.transform.tag == "Laser")
		{
			other.gameObject.SetActive(false);
			rowsToRemove += 3;
		}
	}

	IEnumerator ShieldPowerChanger()
	{
		while (transform.localScale.y > 0.000001f)
		{
			transform.localScale += new Vector3(0, scaleRecharge, 0);
			if (rowsToRemove > 0)
			{
				transform.localScale -= new Vector3(0,0.005f,0);
				rowsToRemove--;
			}
			transform.localScale = new Vector3(transform.localScale.x, Mathf.Clamp(transform.localScale.y, 0, maxScale), transform.localScale.z);
			yield return null;
		}
		Destroy(gameObject);
	}
}

